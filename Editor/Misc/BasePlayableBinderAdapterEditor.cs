using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;
namespace Cr7Sund.TweenTimeLine
{
    public class BasePlayableBinderAdapterEditor
    {
        public static void Reset(BasePlayableBinderAdapter binderAdapter)
        {
            TweenTimelineManager.InitPlayableBindings();

            binderAdapter._cacheList = new();
            IterateRecursive(binderAdapter.transform, (child) =>
            {
                // if (binderAdapter._cacheList.FindIndex
                //     (trans=> trans.GetInstanceID()==child.GetInstanceID()) == -1    )
                //        binderAdapter._cacheList.Add(child.gameObject);

                foreach (var item in TweenTimeLineDataModel.TrackObjectDict)
                {
                    var component = item.Value as Component;
                    if (component.transform == child)
                    {
                        if (!binderAdapter._cacheList.Contains(child.gameObject))
                            binderAdapter._cacheList.Add(child.gameObject);
                    }
                }
            });

            binderAdapter.easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(
                TweenTimelineDefine.easingTokenPresetsPath);

            binderAdapter.curveLibrary = AssetDatabase.LoadAssetAtPath<CurveWrapperLibrary>(TweenTimelineDefine.CurveWrapLibraryPath);

            string AudioSourceFilePath = TweenTimelineDefine.RuntimDataSourePath;
            var audioAssetGUIDs = AssetDatabase.FindAssets("t:AudioClip", new string[] { AudioSourceFilePath });
            binderAdapter.audioClips = new List<AudioClip>(audioAssetGUIDs.Length);
            foreach (var guid in audioAssetGUIDs)
            {
                binderAdapter.audioClips.Add(AssetDatabase.LoadAssetAtPath<AudioClip>(AssetDatabase.GUIDToAssetPath(guid)));
            }
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

    }
}
