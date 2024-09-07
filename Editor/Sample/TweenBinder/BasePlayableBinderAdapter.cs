using System;
using System.Collections.Generic;
using Cr7Sund.TweenTimeLine.Editor;
using PrimeTween;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
namespace Cr7Sund.TweenTimeLine
{
    public abstract class BasePlayableBinderAdapter : MonoBehaviour, ITweenBinding
    {
        [SerializeField] private List<UnityEngine.GameObject> _cacheDict;
        [SerializeField] private EasingTokenPresets easingTokenPresets;



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
                foreach (var item in TweenTimeLineDataModel.BindingTackDict)
                {
                    var component = item.Key as Component;
                    if (component.transform == child)
                    {
                        if (!_cacheDict.Contains(child.gameObject))
                            _cacheDict.Add(child.gameObject);
                    }
                }
            });

            easingTokenPresets = AssetDatabase.LoadAssetAtPath<EasingTokenPresets>(
                AssetDatabase.GUIDToAssetPath(AnimationEditorWindow.easingTokenPresetGuid));

        }

        private static void IterateRecursive(Transform transform, Action<Transform> iterateAction)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                iterateAction?.Invoke(child);
                IterateRecursive(child, iterateAction);
            }
        }

        public T GetBindObj<T>(string name)
        {
            return _cacheDict.Find(x => x.name == name).GetComponent<T>();
        }

        public Easing GetEasing(string easeName)
        {
            return easingTokenPresets.GetEasePreset(easeName);
        }
    }
}
