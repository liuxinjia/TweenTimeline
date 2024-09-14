using UnityEngine;

namespace Cr7Sund
{
   [System.Serializable]
    public class GIFFrameData
    {
        public Texture Frame;
        public float Delay;


        public GIFFrameData(Texture2D frame, float delay)
        {
            Frame = frame;
            Delay = delay;
        }
    }
}
