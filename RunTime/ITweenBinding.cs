using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Cr7Sund.TweenTimeLine
{
    public interface ITweenBinding
    {
        public T GetBindObj<T>(string name) where T : class;
        public PrimeTween.Easing GetEasing(string easeName);
        public void PlayAudioClip(AudioSource audioSource, string clipName, float clipTime);
        public void SetSprite(Image image, string sprite);

    }
}
