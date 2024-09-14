using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using PrimeTween;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Cr7Sund.TweenTimeLine
{
    /// <summary>
    /// you can create your adapter
    /// below binder only for editor sample use
    /// </summary>
    public abstract class BasePlayableBinderAdapter : MonoBehaviour, ITweenBinding
    {
        [FormerlySerializedAs("_cacheDict")]
        public List<UnityEngine.GameObject> _cacheList;
        [FormerlySerializedAs("easingTokenPresets")]
        public EasingTokenPresetLibrary easingTokenPresetLibrary;
        public CurveWrapperLibrary curveLibrary;
        public List<AudioClip> audioClips;

        public void Awake()
        {
            if (curveLibrary != null)
            {
                curveLibrary.GenCurveInfoDict();
            }
        }

        public void Reset()
        {
            BasePlayableBinderAdapterEditor.Reset(this);
        }


        public T GetBindObj<T>(string name)
        {
            GameObject go = _cacheList.Find(x => x.name == name);
            if (gameObject == null)
            {
                throw new Exception($"Don't bind {name}");
            }
            return go.GetComponent<T>();
        }

        public Easing GetEasing(string easeName)
        {
            if (!easingTokenPresetLibrary.TryGetEasePreset(easeName, out var resultEase))
            {
                if (curveLibrary.curveDictionary.TryGetValue(easeName, out var curveWrapper))
                {
                    resultEase = curveWrapper.Curve;
                }
                else
                {
                    throw new IndexOutOfRangeException($"can not found {easeName}");
                }
            }
            return resultEase;
        }

        public void PlayAudioClip(AudioSource audioSource, string clipName, float clipTime)
        {
            var clip = audioClips.Find(clip => clip.name == clipName);
            audioSource.clip = clip;
            audioSource.time = clipTime;
            audioSource.Play();
        }
    }
}
