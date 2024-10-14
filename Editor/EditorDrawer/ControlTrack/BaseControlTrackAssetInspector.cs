using System;
using System.Linq;
using Cr7Sund.Timeline.Extension;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(ControlAsset), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class BaseControlTrackAssetInspector : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            if (target is EmptyControlAsset)
            {
                return new Label("Empty Control Asset");
            }
            TweenTimelineManager.EnsureCanPreview();

            if (!TimelineWindowExposer.GetBehaviourValue(target, out var value))
            {
                return base.CreateInspectorGUI();
            }
            var _behaviour = value as IUniqueBehaviour;
            if (!TweenTimeLineDataModel.ClipStateDict.ContainsKey(_behaviour))
            {
                var t = value as ControlAsset;
                if (TimelineWindowExposer.GetCurDirector() == null)
                {
                    return new HelpBox("Please select a playable director first", HelpBoxMessageType.Warning);
                }
                return new HelpBox("Please assign the target component first", HelpBoxMessageType.Warning);
            }

            var root = new VisualElement();
            var trackUIBuilder = new ControlTrackUIBuilder();
            VisualElement container = trackUIBuilder.CreateContainer(serializedObject, _behaviour);
            root.Add(container);
            return root;
        }
    }

    public class ControlTrackUIBuilder
    {
        private IUniqueBehaviour _behaviour;
        private string _curRestID;
        private const string VisualTreeAssetGUID = "84a8ec30493bd7e4497a1eff081adb6f";
        private const string styleGUID = "cfde41d1fc9c9cc40bf3a8ed4778c5fc";

        private string recordBtnStartDefine = nameof(recordBtnStartDefine);
        private string recordBtnEndDefine = nameof(recordBtnEndDefine);


        public VisualElement CreateContainer(SerializedObject serializedObject, IUniqueBehaviour behaviour)
        {
            _behaviour = behaviour;

            var container = new VisualElement();

            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(VisualTreeAssetGUID));
            VisualElement labelFromUXML = visualTreeAsset.Instantiate();
            var styleAsset = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(styleGUID));
            labelFromUXML.styleSheets.Add(styleAsset);
            container.Add(labelFromUXML);

            DrawRecordHistory(container.Query<VisualElement>("CommandContainers"));
            DrawTrackProperties(container.Query<VisualElement>("SettingsContainers"), serializedObject);
            DrawBtns(container);
            return container;
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

        private void DrawTrackProperties(VisualElement container, SerializedObject serializedObject)
        {
            var templateProp = serializedObject.FindProperty("template");

            var easePresetProp = templateProp.FindPropertyRelative("_easePreset");
            var endPosProp = templateProp.FindPropertyRelative("_endPos");
            var startPosProp = templateProp.FindPropertyRelative("_startPos");
            var isDynamicProp = templateProp.FindPropertyRelative("_isDynamicPos");

            // var easeProp = DrawEasePresetField();
            var easePropField = new PropertyField(easePresetProp);
            var isDynamicPropField = new PropertyField(isDynamicProp);
            var endPosPropField = CreatePosField(endPosProp, false);
            var startPosPropField = CreatePosField(startPosProp, true);

            container.Add(easePropField);
            container.Add(startPosPropField);
            container.Add(endPosPropField);
            container.Add(isDynamicPropField);
        }


        private VisualElement CreatePosField(SerializedProperty prop, bool isStart)
        {
            var posContainer = new VisualElement();
            posContainer.style.flexDirection = FlexDirection.Row;
            var propField = SerializedPropertyValueExtension.CreateField(prop);
            // don't change the init pos 
            // since we should consider design one track which should not change the init pos
            Button resetBtn = new Button(() => ResetPos(isStart));
            Button recordBtn = new Button(() => Record(isStart, prop.name, propField));
            resetBtn.text = "Reset";
            recordBtn.text = "Record";
            recordBtn.name = isStart ? recordBtnStartDefine : recordBtnEndDefine;
            propField.style.flexGrow = 1;
            posContainer.Add(propField);
            posContainer.Add(recordBtn);
            posContainer.Add(resetBtn);
            return posContainer;
        }

        private void ResetPos(bool isStart)
        {
            var resetPos = TweenTimelineManager.GetStartValue(_behaviour, isStart);

            if (isStart)
            {
                _behaviour.StartPos = resetPos;
            }
            else
            {
                _behaviour.EndPos = resetPos;
            }
        }


        #region Buttons
        private void DrawBtns(VisualElement container)
        {
            var preViewBtn = container.Q<Button>("preViewBtn");
            var recordBtn = container.Q<Button>("recordBtn");
            Button playBtn = container.Q<Button>("playBtn");


            preViewBtn.SetEnabled(false);
            var stateInfo = TweenTimeLineDataModel.ClipStateDict[_behaviour];
            stateInfo.ViewAction = () => RefreshBtns(container);

            playBtn.RegisterCallback<ClickEvent>(_ =>
            {
                Play();
            });

            recordBtn.style.display = DisplayStyle.None;
            RefreshBtns(container);
        }

        private void Play()
        {
            if (!string.IsNullOrEmpty(_curRestID))
            {
                EditorTweenCenter.UnRegisterEditorTimer(_curRestID);
                TweenTimelineManager.TogglePlayClip(_behaviour);
                TweenTimeLineDataModel.IsPlaySingleTween = false;
            }

            var stateInfo = TweenTimeLineDataModel.ClipStateDict[_behaviour];
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[_behaviour];

            TweenTimeLineDataModel.IsPlaySingleTween = true;
            TweenTimelineManager.TogglePlayClip(_behaviour);

            // Yeah! it cost more gc compare to create sequence
            float delayResetTime = TweenTimelinePreferencesProvider.GetFloat(ActionEditorSettings.DelayResetTime);
            _curRestID = EditorTweenCenter.RegisterDelayCallback(_behaviour,
            (float)clipInfo.duration + delayResetTime,
             (t, elapsedTime) =>
             {
                 TweenTimelineManager.TogglePlayClip(_behaviour);
                 _curRestID = string.Empty;
                 TweenTimeLineDataModel.IsPlaySingleTween = false;
             });
        }

        private void Record(bool isStart, string propName, VisualElement propField)
        {
            var stateInfo = TweenTimeLineDataModel.ClipStateDict[_behaviour];
            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[_behaviour];
            var trackTarget = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            stateInfo.IsRecordStart = isStart;

            TweenTimelineManager.SelectBeforeToggle(_behaviour);
            TweenTimelineManager.ToggleRecordClip(_behaviour);

            if (stateInfo.BehaviourState == ClipBehaviourStateEnum.Recording)
            {
                if (TimelineEditor.selectedClip != null)
                {
                    TweenTimeLineDataModel.SelectTimelineClip = TimelineEditor.selectedClip;
                }
                var playableDirector = TimelineWindowExposer.GetCurDirector();
                if (playableDirector != null)
                {
                    TweenTimeLineDataModel.SelectDirector = playableDirector.gameObject;
                }

                var serializedObject = new SerializedObject(TweenTimeLineDataModel.SelectTimelineClip.asset);

                ControlTrackWindow.Open(serializedObject, _behaviour);
                SceneView.GetWindow<SceneView>();
                if (trackTarget is Component trackComponent)
                {
                    Selection.activeGameObject = trackComponent.gameObject;
                }
                else
                {
                    Selection.activeObject = trackTarget;
                }
                SceneView.lastActiveSceneView.FrameSelected();
            }
            else
            {
                var serializedObject = new SerializedObject(TweenTimeLineDataModel.SelectTimelineClip.asset);

                var templateProp = serializedObject.FindProperty("template");
                SerializedProperty serialProp = templateProp.FindPropertyRelative(propName);

                SerializedPropertyValueExtension.UpdateField(propField, serialProp,
                stateInfo.IsRecordStart ? _behaviour.StartPos : _behaviour.EndPos);
            }
            stateInfo.IsRecordStart = false;
        }

        public void RefreshBtns(VisualElement container)
        {
            var preViewBtn = container.Q<Button>("preViewBtn");
            Button playBtn = container.Q<Button>("playBtn");
            var recordEndBtn = container.Q<Button>(recordBtnEndDefine);
            var recordStartBtn = container.Q<Button>(recordBtnStartDefine);

            if (_behaviour == null)
            {
                OnUpdateRunBtn(container, recordEndBtn, () => false);
                OnUpdateRunBtn(container, recordStartBtn, () => false);
                OnUpdateRunBtn(container, preViewBtn, () => false);
                OnUpdateRunBtn(container, playBtn, () => false);
                return;
            }

            var stateInfo = TweenTimeLineDataModel.ClipStateDict[_behaviour];
            OnUpdateRunBtn(container, playBtn, () => stateInfo.IsPlaying);
            OnUpdateRunBtn(container, preViewBtn, () => stateInfo.IsPreview);

            if (stateInfo.IsRecording)
            {
                if (stateInfo.IsRecordStart)
                {
                    OnUpdateRunBtn(container, recordStartBtn, () => stateInfo.IsRecording);
                }
                else
                {
                    OnUpdateRunBtn(container, recordEndBtn, () => stateInfo.IsRecording);
                }
            }
            else
            {
                OnUpdateRunBtn(container, recordStartBtn, () => false);
                OnUpdateRunBtn(container, recordEndBtn, () => false);
            }
        }

        private void RefreshBtnImges(VisualElement container)
        {
            Button playBtn = container.Q<Button>("playBtn");
            var recordBtn = container.Q<Button>("recordBtn");

            playBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsPlaying ? TweenTimelineDefine.plaBackground : TweenTimelineDefine.stopBackground;
            recordBtn.iconImage = TweenTimeLineDataModel.StateInfo.IsRecording ? TweenTimelineDefine.recordOnBackground : TweenTimelineDefine.recordOffBackground;
        }

        private void OnUpdateRunBtn(VisualElement container, Button recordBtn, Func<bool> func)
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

            RefreshBtnImges(container);

            var listView = container.Q<ListView>("historyList");
            listView.Rebuild();
        }
        #endregion

    }
}
