using System;
using System.Linq;
using Cr7Sund.Timeline.Extension;
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
        private VisualElement container;
        private IUniqueBehaviour _behaviour;
        private string _curRestID;
        private const string VisualTreeAssetGUID = "84a8ec30493bd7e4497a1eff081adb6f";
        private const string styleGUID = "cfde41d1fc9c9cc40bf3a8ed4778c5fc";

        private SerializedProperty _easePresetProp;
        private SerializedProperty _endPosProp;
        private SerializedProperty _startPosProp;

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

            var root = new VisualElement();
            container = new VisualElement();

            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(VisualTreeAssetGUID));
            VisualElement labelFromUXML = visualTreeAsset.Instantiate();
            var styleAsset = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(styleGUID));
            labelFromUXML.styleSheets.Add(styleAsset);
            container.Add(labelFromUXML);

            var behaviour = value as IUniqueBehaviour;
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
            float duration = (float)clipInfo.duration;
            float start = (float)clipInfo.start;
            // if ((float)Math.Round(duration, 3) != duration
            // || (float)Math.Round(start, 3) != start)
            // {
            //     HelpBox helpBox = new HelpBox($"Clip end is invalid seconds {duration} {start}.  Note: The time is rounded to 3 decimal places to avoid precision issues. \n If you change it , the warning remains. Try to switch inspector", HelpBoxMessageType.Warning);
            //     root.Add(helpBox);
            // }

            DrawRecordHistory(container.Query<VisualElement>("CommandContainers"));
            DrawTrackProperties(container.Query<VisualElement>("SettingsContainers"));
            DrawBtns();

            root.Add(container);
            return root;
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
            var templateProp = serializedObject.FindProperty("template");
            _easePresetProp = templateProp.FindPropertyRelative("_easePreset");
            _endPosProp = templateProp.FindPropertyRelative("_endPos");
            _startPosProp = templateProp.FindPropertyRelative("_startPos");

            // var easeProp = DrawEasePresetField();
            var easeProp = new PropertyField(_easePresetProp);
            var endPosProp = new PropertyField(_endPosProp);
            var startPosProp = new PropertyField(_startPosProp);

            container.Add(easeProp);
            container.Add(endPosProp);
            container.Add(startPosProp);
        }

        private VisualElement DrawEasePresetField()
        {
            var derivedEaseTokenTypes = TweenTimelineDefine.DerivedEaseTokenTypes;
            Type currentType = _easePresetProp.managedReferenceValue?.GetType();

            string[] typeNames = derivedEaseTokenTypes.Select(t => t.Name).ToArray();
            int currentIndex = Array.FindIndex(derivedEaseTokenTypes, type => type == currentType);
            var popupField = new PopupField<string>("Ease Preset Type", typeNames.ToList(), currentIndex);

            popupField.RegisterValueChangedCallback(evt =>
            {
                int selectedIndex = typeNames.ToList().IndexOf(evt.newValue);
                Type selectedType = derivedEaseTokenTypes[selectedIndex];

                if (selectedType != currentType)
                {
                    _easePresetProp.managedReferenceValue = Activator.CreateInstance(selectedType);
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }
            });

            var container = new VisualElement();
            container.Add(popupField);

            if (_easePresetProp.managedReferenceValue != null)
            {
                PropertyField easePresetField = new PropertyField(_easePresetProp, "Ease Preset");
                container.Add(easePresetField);
            }

            return container;
        }

        #region Buttons
        private void DrawBtns()
        {
            var preViewBtn = container.Q<Button>("preViewBtn");
            var recordBtn = container.Q<Button>("recordBtn");
            Button playBtn = container.Q<Button>("playBtn");
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
                if (!string.IsNullOrEmpty(_curRestID))
                {
                    EditorTweenCenter.UnRegisterEditorTimer(_curRestID);
                    TweenTimelineManager.TogglePlayClip(behaviour);
                }

                var stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
                var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
                TweenTimelineManager.TogglePlayClip(behaviour);

                // Yeah! it cost more gc compare to create sequence
                float dealyResetTime = TweenTimelinePreferencesProvider.GetFloat(ActionEditorSettings.DealyResetTime);
                _curRestID = EditorTweenCenter.RegisterDelayCallback(target,
                 (float)clipInfo.duration + dealyResetTime,
                  (t, elapsedTime) =>
                  {
                      TweenTimelineManager.TogglePlayClip(behaviour); _curRestID = string.Empty;
                  });
            });

            recordBtn.RegisterCallback<ClickEvent>(_ =>
            {
                var stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
                var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
                var trackTarget = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
                TweenTimelineManager.SelectBeforeToggle(behaviour);
                TweenTimelineManager.ToggleRecordClip(behaviour);
                if (stateInfo.BehaviourState == ClipBehaviourStateEnum.Recording)
                {
                    Selection.activeObject = trackTarget;
                }
            });
            RefreshBtns();
        }

        public void RefreshBtns()
        {
            var preViewBtn = container.Q<Button>("preViewBtn");
            var recordBtn = container.Q<Button>("recordBtn");
            Button playBtn = container.Q<Button>("playBtn");

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
            Button playBtn = container.Q<Button>("playBtn");
            var recordBtn = container.Q<Button>("recordBtn");

            playBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsPlaying ? TweenTimelineDefine.plaBackground : TweenTimelineDefine.stopBackground;
            recordBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsRecording ? TweenTimelineDefine.recordOnBackground : TweenTimelineDefine.recordOffBackground;
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

            var listView = container.Q<ListView>("historyList");
            listView.Rebuild();
        }
        #endregion

    }


}
