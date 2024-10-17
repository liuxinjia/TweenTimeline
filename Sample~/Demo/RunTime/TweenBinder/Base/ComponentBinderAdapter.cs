using System;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using UnityEngine.UI;
using Assert = UnityEngine.Assertions.Assert;

namespace Cr7Sund.TweenTimeLine
{
    public class ComponentBinderAdapter : MonoBehaviour, ITweenBinding
    {
        public List<ComponentPairs> cacheList;
        public EasingTokenPresetLibrary easingTokenPresetLibrary;
        public int loopCount = 1;

        protected Sequence _currentSequence;

        public void OnDisable()
        {
            StopTween();
        }

        protected Sequence PlayTween(string tweenName)
        {
            StopTween();
            return this.Play(tweenName, loopCount);
        }

        public void StopTween()
        {
            if (_currentSequence.isAlive)
            {
                _currentSequence.Stop();
            }
        }

        public T GetBindObj<T>(string name) where T : class
        {
            ComponentPairs componentPairs = cacheList.Find(x => x.key == name);
            Assert.IsNotNull(componentPairs, $"{this.gameObject.name} had no cache {name} \n or you don't update the generate tween after change gameobject name");

            Component component = componentPairs.component;
            T result = component as T;
            Assert.IsNotNull(result, $"{name} is null or don't have component {typeof(T)}");

            return result;
        }

        public Easing GetEasing(string easeName)
        {
            if (!easingTokenPresetLibrary.TryGetEasePreset(easeName, out var resultEase))
            {
                throw new IndexOutOfRangeException($"can not found {easeName}");
            }
            return resultEase;
        }

        public void PlayAudioClip(AudioSource audioSource, string clipName, float clipTime)
        {
            var clip = AddressableLoader.Load<AudioClip>(clipName);
            audioSource.clip = clip;
            audioSource.time = clipTime;
            audioSource.Play();
        }

        public void SetSprite(Image image, string sprite)
        {
            image.sprite = AddressableLoader.Load<Sprite>(sprite);
        }

    }

}
