using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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


        [ContextMenu(nameof(ConvertFolders))]
        public void ConvertFolders()
        {
            var decoderDict = new Dictionary<string, MG.GIF.Decoder>();
            Parallel.ForEach(folders, (folder, i) =>
            {
                var files = Directory.EnumerateFiles(folder, "*.gif");
                foreach (var file in files)
                {
                    if (gifDataLists.FindIndex((t => t.FilePath == file)) >= 0)
                    {
                        continue;
                    }
                    var decoder = new MG.GIF.Decoder(File.ReadAllBytes(file));
                    decoderDict.Add(file, decoder);
                }
            });

            string assetPath = AssetDatabase.GetAssetPath(this);
            string textureFolderName = "Textures";

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
                        var imagePath = $"{relativePath}/{Path.GetFileNameWithoutExtension(filePath)}{sequenceID}.asset";
                        AssetDatabase.CreateAsset(texture, imagePath);
                        // AssetDatabase.AddObjectToAsset(texture, assetPath);
                        sequenceID++;
                        time += img.Delay / 1000.0f;
                        gifData.Frames.Add(new GIFFrameData(texture, time));

                        img = decoder.NextImage();
                    }

                }
            }

            AssetDatabase.SaveAssetIfDirty(this);
            AssetDatabase.Refresh();

            GC.Collect();
        }
    }
}
