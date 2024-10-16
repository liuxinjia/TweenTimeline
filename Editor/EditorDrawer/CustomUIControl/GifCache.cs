using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cr7Sund.TweenTimeLine;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund
{
    [CreateAssetMenu(menuName = "Cr7Sund/GifCache")]
    public class GifCache : ScriptableObject
    {
        // public Dictionary<string, GifData> _cache = new();
        public List<GifData> gifDataLists = new();
        public List<string> folders = new List<string>();

        public void Reset()
        {
            folders.Add(TweenTimelineDefine.BuiltInGIFPresetFolder);
            folders.Add(TweenTimelineDefine.CustomGIFPresetFolder);
        }

        [ContextMenu(nameof(ConvertFolders))]
        public void ConvertFolders()
        {
            var decoderDict = new Dictionary<string, MG.GIF.Decoder>();
            for (int i = 0; i < folders.Count; i++)
            {
                string folder = folders[i];
                string folderName = PathUtility.ConvertToAbsolutePath(folder);
                if (!Directory.Exists(folderName))
                {
                    continue;
                }
                var files = Directory.EnumerateFiles(folderName, "*.gif");
                foreach (var file in files)
                {
                    if (gifDataLists.FindIndex((t => t.FilePath == file)) >= 0)
                    {
                        continue;
                    }
                    var decoder = new MG.GIF.Decoder(File.ReadAllBytes(file));
                    decoderDict.Add(file, decoder);
                }
            }

            string assetPath = AssetDatabase.GetAssetPath(this);
            string textureFolderName = "GIFTextures";

            var images = new List<Tuple<string, byte[]>>();
            foreach (var item in decoderDict)
            {
                var filePath = item.Key;
                using (var decoder = item.Value)
                {
                    string textureSourcePath = Path.Combine(Path.GetDirectoryName(filePath), textureFolderName);
                    if (!Directory.Exists(textureSourcePath))
                    {
                        Directory.CreateDirectory(textureSourcePath);
                    }

                    var relativePath = PathUtility.ConvertToRelativePath(textureSourcePath);
                    var gifData = new GifData(filePath);
                    gifDataLists.Add(gifData);

                    var img = decoder.NextImage();
                    float time = 0;
                    int sequenceID = 0;
                    while (img != null)
                    {
                        Texture2D texture = img.CreateTexture();
                        var imagePath = Path.Combine(textureSourcePath,
                             $"{Path.GetFileNameWithoutExtension(filePath)}{sequenceID}.png");
                        byte[] encodedImage = texture.EncodeToPNG();
                        var relativeImagePath = PathUtility.ConvertToRelativePath(imagePath);
                        images.Add(new Tuple<string, byte[]>(relativeImagePath, encodedImage));

                        sequenceID++;
                        time += img.Delay / 1000.0f;
                        gifData.Frames.Add(new GIFFrameData(relativeImagePath, time));

                        img = decoder.NextImage();
                    }
                }
            }

            SaveTexturesFromList(images);
            AssetDatabase.SaveAssetIfDirty(this);
            AssetDatabase.Refresh();

            GC.Collect();
        }

        async void SaveTexturesFromList(List<Tuple<string, byte[]>> textures)
        {
            List<Task> tasks = new(textures.Count);

            for (int i = 0; i < textures.Count; i++)
            {
                byte[] bytes = textures[i].Item2;
                string texturePath = textures[i].Item1;

                if (bytes != null)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await File.WriteAllBytesAsync(texturePath, bytes);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Error saving texture to {textures[i].Item1}: {ex.Message}");
                        }
                    }));
                }
            }

            await Task.WhenAll(tasks);
            Debug.Log($"Batch texture saving complete. {tasks.Count}");
        }

    }
}
