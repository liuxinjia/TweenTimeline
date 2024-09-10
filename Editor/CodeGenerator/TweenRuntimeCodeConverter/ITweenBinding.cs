using PrimeTween;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public interface ITweenBinding
    {
        public T GetBindObj<T>(string name);
        public Easing GetEasing(string curName);
        public void PlayAudioClip(AudioSource audioSource, string clipName, float clipTime);
    }
}
