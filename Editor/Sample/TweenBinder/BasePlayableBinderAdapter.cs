using System;
using System.Collections.Generic;
using System.Linq;
using Cr7Sund.TweenTimeLine.Editor;
using PrimeTween;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Cr7Sund.TweenTimeLine
{
    public abstract class BasePlayableBinderAdapter : MonoBehaviour, ITweenBinding
    {
        private const string AudioSourceFilePath = "Assets/Plugins/TweenTimeline/Editor/Sample/Datas/Audios";
        [SerializeField] private List<UnityEngine.GameObject> _cacheDict;
        [SerializeField] private EasingTokenPresets easingTokenPresets;
        [SerializeField] private CurveWrapperLibrary curveLibrary;
        [SerializeField] private List<AudioClip> audioClips;


        private void Reset()
        {
            TweenTimelineManager.InitPlayableBindings();
            PlayableDirector playableDirector = GameObject.FindFirstObjectByType<PlayableDirector>();
            if (playableDirector == null)
            {
                return;
            };

            _cacheDict = new();
            IterateRecursive(this.transform, (child) =>
            {
                foreach (var item in TweenTimeLineDataModel.TrackObjectDict)
                {
                    var component = item.Value as Component;
                    if (component.transform == child)
                    {
                        if (!_cacheDict.Contains(child.gameObject))
                            _cacheDict.Add(child.gameObject);
                    }
                }
            });

            easingTokenPresets = AssetDatabase.LoadAssetAtPath<EasingTokenPresets>(
                AssetDatabase.GUIDToAssetPath(AnimationEditorWindow.easingTokenPresetGuid));

            curveLibrary = AssetDatabase.LoadAssetAtPath<CurveWrapperLibrary>(CurveLibraryCenter.CurWrapLibraryPath);

            var audioAssetGUIDs = AssetDatabase.FindAssets("t:AudioClip", new string[] { AudioSourceFilePath });
            audioClips = new List<AudioClip>(audioAssetGUIDs.Length);
            foreach (var guid in audioAssetGUIDs)
            {
                audioClips.Add(AssetDatabase.LoadAssetAtPath<AudioClip>(AssetDatabase.GUIDToAssetPath(guid)));
            }
        }

        public void Awake()
        {
            curveLibrary.GenCurveInfoDict();
        }

        private static void IterateRecursive(Transform transform, Action<Transform> iterateAction)
        {
            iterateAction?.Invoke(transform);
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                IterateRecursive(child, iterateAction);
            }
        }

        public T GetBindObj<T>(string name)
        {
            GameObject go = _cacheDict.Find(x => x.name == name);
            if (gameObject == null)
            {
                throw new Exception($"Don't bind {name}");
            }
            return go.GetComponent<T>();
        }

        public Easing GetEasing(string easeName)
        {
            if (!easingTokenPresets.TryGetEasePreset(easeName, out var resultEase))
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
