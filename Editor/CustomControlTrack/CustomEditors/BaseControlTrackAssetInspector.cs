﻿using System;
using Cr7Sund.Timeline.Extension;
using Cr7Sund.TweenTimeLine.Editor;
using PrimeTween;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(ControlAsset), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class BaseControlTrackAssetInspector : UnityEditor.Editor
    {
        private VisualElement _root;
        private IUniqueBehaviour _behaviour;
        private const string VisualTreeAssetGUID = "84a8ec30493bd7e4497a1eff081adb6f";
        private const string styleGUID = "cfde41d1fc9c9cc40bf3a8ed4778c5fc";

        public override VisualElement CreateInspectorGUI()
        {
            TweenTimelineManager.EnsureCanPreview();
            if (!TimelineWindowExposer.GetBehaviourValue(target, out var value))
            {
                return base.CreateInspectorGUI();
            }
            _behaviour = value as IUniqueBehaviour;
            if (!TweenTimeLineDataModel.ClipStateDict.ContainsKey(_behaviour))
            {
                return base.CreateInspectorGUI();
            }

            _root = new VisualElement();

            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(VisualTreeAssetGUID));
            VisualElement labelFromUXML = visualTreeAsset.Instantiate();
            var styleAsset = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(styleGUID));
            labelFromUXML.styleSheets.Add(styleAsset);
            _root.Add(labelFromUXML);

            DrawRecordHistory(_root.Query<VisualElement>("CommandContainers"));
            DrawTrackProperties(_root.Query<VisualElement>("SettingsContainers"));

            DrawBtns();
            return _root;
        }

        private void DrawRecordHistory(VisualElement container)
        {
            var clipBehaviourState = TweenTimeLineDataModel.ClipStateDict[_behaviour];
            var trackHistory = clipBehaviourState.actionTrackHistory;
            var items = trackHistory.HistoryList;

            // The "makeItem" function is called when the
            // ListView needs more items to render.
            Func<VisualElement> makeItem = () => new Button();

            // As the user scrolls through the list, the ListView object
            // recycles elements created by the "makeItem" function,
            // and invoke the "bindItem" callback to associate
            // the element with the matching data item (specified as an index in the list).
            Action<VisualElement, int> bindItem = (e, i) => (e as Button).text = items[i].ToString();

            // Provide the list view with an explict height for every row
            // so it can calculate how many items to actually display
            const int itemHeight = 16;

            var listView = container.Q<ListView>("historyList");
            listView.fixedItemHeight = itemHeight;
            listView.makeItem = makeItem;
            listView.bindItem = bindItem;
            listView.selectionType = SelectionType.Multiple;
            listView.itemsSource = items;

            listView.itemsChosen += objects => Debug.Log(objects);
            listView.selectionChanged += objects => Debug.Log(objects);
        }

        protected virtual void DrawTrackProperties(VisualElement container)
        {
            var property = serializedObject.GetIterator();
            var expanded = true;
            while (property.NextVisible(expanded))
            {
                if ("m_Script" == property.propertyPath)
                {
                    continue;
                }

                if ("name" == property.propertyPath)
                {
                    continue;
                }

                container.Add(new PropertyField(property));
                expanded = false;
            }
        }


        #region  Buttons
        private void DrawBtns()
        {
            var preViewBtn = _root.Q<Button>("preViewBtn");
            var recordBtn = _root.Q<Button>("recordBtn");
            Button playBtn = _root.Q<Button>("playBtn");
            if (!TimelineWindowExposer.GetBehaviourValue(target, out var value))
            {
                return;
            }
            var behaviour = value as IUniqueBehaviour;

            preViewBtn.SetEnabled(false);
            var stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
            stateInfo.ViewAction = RefreshBtns;

            playBtn.RegisterCallback<ClickEvent>(_ =>
            {
                var stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
                var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
                TweenTimelineManager.TogglePlayClip(behaviour);

                // Yeah! it cost more gc compare to create sequence
                EditorTweenCenter.RegisterDelayCallback(target, (float)clipInfo.duration + 0.8f, b => TweenTimelineManager.TogglePlayClip(behaviour));
            });

            recordBtn.RegisterCallback<ClickEvent>(_ =>
            {
                var stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
                TweenTimelineManager.SelectBeforeToggle(behaviour);
                TweenTimelineManager.ToggleRecordClip(behaviour);
            });
            RefreshBtns();
        }

        public void RefreshBtns()
        {
            var preViewBtn = _root.Q<Button>("preViewBtn");
            var recordBtn = _root.Q<Button>("recordBtn");
            Button playBtn = _root.Q<Button>("playBtn");

            if (!TimelineWindowExposer.GetBehaviourValue(target, out var value))
            {
                OnUpdateRunBtn(recordBtn, () => false);
                OnUpdateRunBtn(preViewBtn, () => false);
                OnUpdateRunBtn(playBtn, () => false);
                return;
            }

            var behaviour = value as IUniqueBehaviour;
            var stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
            OnUpdateRunBtn(recordBtn, () => stateInfo.IsRecording);
            OnUpdateRunBtn(playBtn, () => stateInfo.IsPlaying);
            OnUpdateRunBtn(preViewBtn, () => stateInfo.IsPreview);
        }

        private void RefreshBtnImges()
        {
            Button playBtn = _root.Q<Button>("playBtn");
            var recordBtn = _root.Q<Button>("recordBtn");

            playBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsPlaying ? AnimationEditorWindow.plaBackground : AnimationEditorWindow.stopBackground;
            recordBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsRecording ? AnimationEditorWindow.recordOnBackground : AnimationEditorWindow.recordOffBackground;
        }

        private void OnUpdateRunBtn(Button recordBtn, Func<bool> func)
        {
            bool result = func.Invoke();
            if (result)
            {
                recordBtn.EnableInClassList("DisActiveBtn", false);
                recordBtn.EnableInClassList("ActiveBtn", true);
            }
            else
            {
                recordBtn.EnableInClassList("DisActiveBtn", true);
                recordBtn.EnableInClassList("ActiveBtn", false);
            }

            RefreshBtnImges();

            var listView = _root.Q<ListView>("historyList");
            listView.Rebuild();
        }

        #endregion

    }


}
