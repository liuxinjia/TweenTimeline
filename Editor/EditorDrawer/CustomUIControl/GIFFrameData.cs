using UnityEditor;
using UnityEngine;

namespace Cr7Sund
{
    [System.Serializable]
    public class GIFFrameData
    {
        public string texturePath;
        public float Delay;

        private Texture cachedTexture;
        public Texture Texture => cachedTexture ?? (cachedTexture = AssetDatabase.LoadAssetAtPath<Texture>(texturePath));


        public GIFFrameData(string frameTexturePath, float delay)
        {
            texturePath = frameTexturePath;
            Delay = delay;
        }

        public GIFFrameData(Texture texture, float delay)
        {
            cachedTexture = texture;
            Delay = delay;
        }
    }
}
