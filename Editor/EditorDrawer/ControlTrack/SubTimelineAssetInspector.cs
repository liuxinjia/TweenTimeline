using System;
using System.Collections.Generic;
using Cr7Sund.Timeline.Extension;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(SubTimelineControlAsset)), CanEditMultipleObjects]
    class SubTimelineAssetInspector : UnityEditor.Editor
    {
        SerializedProperty m_SourceObject;
        SerializedProperty m_tweenName;
        public override VisualElement CreateInspectorGUI()
        {
            if (target == null) // case 946080
                return new VisualElement();

            m_SourceObject = serializedObject.FindProperty("sourceGameObject");
            m_tweenName = serializedObject.FindProperty("subTweenName");

            var objectField = new PropertyField();
            objectField.BindProperty(m_SourceObject);

            var options = CreateOptions();
            int currentIndex = options.FindIndex((option) => (m_tweenName.stringValue == option));
            currentIndex = Math.Max(0, currentIndex);
            var menu = new PopupField<string>(m_tweenName.displayName, options, currentIndex);
            menu.RegisterValueChangedCallback(evt =>
            {
                OnOptionChanged(evt.newValue);
                m_tweenName.stringValue = evt.newValue;
                serializedObject.ApplyModifiedProperties();
            });
            var container = new VisualElement();
            container.Add(objectField);
            container.Add(menu);

            return container;
        }

        private void OnOptionChanged(string newValue)
        {
            var controlPlayableAsset = target as ControlPlayableAsset;
            var rootDirector = TimelineWindowExposer.GetCurDirector();
            var go = controlPlayableAsset.sourceGameObject.Resolve(rootDirector);
            if (go == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(newValue))
            {
                return;
            }
            var director = go.GetComponent<PlayableDirector>();
            var subTimeLineAsset = director.playableAsset as TimelineAsset;
            if (subTimeLineAsset != null)
            {
                string[] splits = newValue.Split('_');
                string tweenName = splits[0];
                string rootName = splits.Length < 2 ? string.Empty : splits[1];
                TimelineWindowExposer.IterateTrackAsset(subTimeLineAsset, (trackAsset) =>
                {
                    if (trackAsset is not GroupTrack)
                    {
                        return;
                    }
                    if (trackAsset.parent == null)
                    {
                        return;
                    }
                    if (trackAsset.parent.name != TweenTimelineDefine.InDefine
                    && trackAsset.parent.name != TweenTimelineDefine.OutDefine)
                    {
                        return;
                    }
                    trackAsset.muted = trackAsset.name != tweenName;

                    if (!string.IsNullOrEmpty(rootName)
                     && trackAsset.parent.name != rootName)
                    {
                        trackAsset.muted = true;
                    }
                });
            }

        }

        public List<string> CreateOptions()
        {
            var options = new List<string>();

            var controlPlayableAsset = target as ControlPlayableAsset;
            var rootDirector = TimelineWindowExposer.GetCurDirector();
            var go = controlPlayableAsset.sourceGameObject.Resolve(rootDirector);
            if (go == null)
            {
                return options;
            }
            var director = go.GetComponent<PlayableDirector>();
            var subTimeLineAsset = director.playableAsset as TimelineAsset;
            // clip.displayName = director.playableAsset.name;

            if (subTimeLineAsset != null)
            {
                TimelineWindowExposer.IterateTrackAsset(subTimeLineAsset, (trackAsset) =>
                {
                    if (trackAsset is not GroupTrack)
                    {
                        return;
                    }
                    if (trackAsset.parent == null)
                    {
                        return;
                    }
                    if (trackAsset.parent.name != TweenTimelineDefine.InDefine
                    && trackAsset.parent.name != TweenTimelineDefine.OutDefine)
                    {
                        return;
                    }

                    options.Add(TweenCodeGenerator.GetGenSequenceName(trackAsset));
                });
            }

            return options;
        }
    }
}