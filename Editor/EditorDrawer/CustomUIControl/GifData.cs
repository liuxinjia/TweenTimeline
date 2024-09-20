using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cr7Sund
{
    [System.Serializable]
    public class GifData
    {
        public List<GIFFrameData> Frames;
        public string FilePath;

        public GifData(string path)
        {
            FilePath = path;
            Frames = new List<GIFFrameData>();
        }

        public void CreateGIFWithouCache()
        {
            // string absolutePath = PathUtility.ConvertToAbsolutePath(FilePath);
            using var decoder = new MG.GIF.Decoder(File.ReadAllBytes(FilePath));
            var img = decoder.NextImage();

            this.Frames.Clear();
            float time = 0;
            while (img != null)
            {
                Texture2D texture = img.CreateTexture();
                time += img.Delay / 1000.0f;
                this.Frames.Add(new GIFFrameData(texture, time));
                img = decoder.NextImage();
            }
        }
    }
}
