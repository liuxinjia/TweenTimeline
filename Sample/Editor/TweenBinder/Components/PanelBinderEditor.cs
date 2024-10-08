using System;
using System.Collections.Generic;
using System.Linq;
using PrimeTween;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using ObjectField = UnityEditor.UIElements.ObjectField;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(PanelBinder))]
    public class PanelBinderEditor : UnityEditor.Editor
    {
        protected VisualElement _root;
        private Sequence _curSequence;
        private string _updateSequenceID;
        private string _resetTweenID;

        protected virtual List<string> tweenPropNames => new List<string>() { "inTweenName", "outTweenName" };
        protected string timelineFieldName = "timelineAsset";

        public override VisualElement CreateInspectorGUI()
        {
            DrawUI();
            return _root;
        }

        protected void DrawUI()
        {
            _root = new VisualElement();
            var tweenContainer = new VisualElement();
            var binderAdapter = target as ComponentBinderAdapter;
            var cacheListProp = serializedObject.FindProperty("cacheList");
            var timelineAssetProp = serializedObject.FindProperty(timelineFieldName);
            BindAdapterEditorHelper.Reset(binderAdapter);

            CreateTweenContains(tweenContainer);

            var cacheListPropField = BindAdapterEditorHelper.CreateList(cacheListProp, binderAdapter, null);
            cacheListPropField.name = "cacheList";
            var rebindBtn = new Button(RebindComponents);
            rebindBtn.text = "Rebind";

            ObjectField timeLineField = CreateTimeLineField(timelineAssetProp);
            BindAdapterEditorHelper.DrawLoopField(serializedObject, _root);

            _root.Add(timeLineField);
            _root.Add(tweenContainer);
            _root.Add(cacheListPropField);
            _root.Add(rebindBtn);
        }


        private ObjectField CreateTimeLineField(SerializedProperty timelineAssetProp)
        {
            var timeLineField = new ObjectField();
            timeLineField.objectType = typeof(TimelineAsset);
            timeLineField.value = GetTimelineAsset();
            timeLineField.RegisterValueChangedCallback(evt =>
            {
                if (evt.previousValue == evt.newValue)
                {
                    return;
                }
                OnUpdateTimeLineField();
                // RebindComponents();
            });
            timeLineField.BindProperty(timelineAssetProp); // if will trigger ValueChanged when switch each time
            return timeLineField;
        }

        protected virtual TimelineAsset GetTimelineAsset()
        {
            var binderAdapter = target as PanelBinder;
            return binderAdapter.timelineAsset;
        }

        protected virtual void OnUpdateTimeLineField()
        {
            var binderAdapter = target as PanelBinder;
            var inTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.inTweenName));
            var outTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.outTweenName));
            UpdateMenu(inTweenNameProp, true);
            UpdateMenu(outTweenNameProp);
        }

        protected virtual void CreateTweenContains(VisualElement tweenContainer)
        {
            var binderAdapter = target as PanelBinder;
            var inTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.inTweenName));
            var outTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.outTweenName));
            var inTweenMenu = AddTweenField(inTweenNameProp, true);
            var outTweenMenu = AddTweenField(outTweenNameProp);

            tweenContainer.Add(inTweenMenu);
            tweenContainer.Add(outTweenMenu);
        }

        private void UpdateMenu(SerializedProperty property, PopupField<string> popup, bool isIn = false)
        {
            GetTweenNamMenus(property, isIn, out var choices, out var initialIndex);
            popup.choices = choices;
            popup.index = initialIndex;
        }

        protected void UpdateMenu(SerializedProperty property, bool isIn = false)
        {
            PopupField<string> popup = _root.Q<PopupField<string>>(property.displayName);
            UpdateMenu(property, popup, isIn);
        }

        private PopupField<string> CreateMenu(SerializedProperty property, bool isIn = false)
        {
            var popupMenu = new PopupField<string>(label: property.displayName);
            popupMenu.name = property.displayName;
            UpdateMenu(property, popupMenu, isIn);
            popupMenu.RegisterValueChangedCallback(evt =>
            {
                property.stringValue = BindAdapterEditorHelper.GetTweenName(evt.newValue);
                serializedObject.ApplyModifiedProperties();
                RebindComponents();
            });

            return popupMenu;
        }

        protected VisualElement AddTweenField(SerializedProperty nameProp, bool isIn = false)
        {
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            var popup = CreateMenu(nameProp, isIn);
            var previewBtn = CreatePreViewBtn(nameProp);

            container.Add(popup);
            container.Add(previewBtn);
            return container;
        }

        private void RebindComponents()
        {
            var binderAdapter = target as ComponentBinderAdapter;
            var timelineAsset = GetTimelineAsset();

            binderAdapter.cacheList.Clear();
            if (timelineAsset == null)
            {
                return;
            }

            var propList = new List<SerializedProperty>();
            foreach (var tweenName in tweenPropNames)
            {
                propList.Add(serializedObject.FindProperty(tweenName));
            }

            List<ComponentTween> tweenComponents = BindAdapterEditorHelper.GetTweenTypes(timelineAsset);
            foreach (ComponentTween item in tweenComponents)
            {
                foreach (var prop in propList)
                {
                    RebindComponent(item, prop.stringValue);
                }
            }
        }

        private bool RebindComponent(ComponentTween componentTween, string tweenName)
        {
            List<ComponentBindTracks> tweenNames = componentTween.tweenNames;
            var componentBindTracks = tweenNames.Find((ts) =>
                BindAdapterEditorHelper.GetTweenName(ts.tweenName) == tweenName);

            if (componentBindTracks == null)
            {
                return false;
            }
            List<string> bindTargets = componentBindTracks.bindTargets;
            ComponentBinderAdapter binderAdapter = target as ComponentBinderAdapter;

            for (int i = 0; i < bindTargets.Count; i++)
            {
                reBind(bindTargets[i],
                 componentBindTracks.bindTypes[i]);
            }

            return true;
            void reBind(string name, string componentTypeFullName)
            {
                ComponentBinderAdapter binderAdapter = target as ComponentBinderAdapter;

                int findIndex = binderAdapter.cacheList.FindIndex(ts =>
                     ts.key == BindAdapterEditorHelper.GetTweenTarget(name, componentTypeFullName));
                if (findIndex >= 0) return;

                Transform transform = binderAdapter.transform.FindChildByName(name);
                Component component = null;
                if (transform != null)
                {
                    component = transform.GetNotNullComponent(componentTypeFullName);
                }

                binderAdapter.cacheList.Add(new ComponentPairs()
                {
                    key = BindAdapterEditorHelper.GetTweenTarget(name, componentTypeFullName),
                    component =
                               component
                });
            }
        }

        private void GetTweenNamMenus(SerializedProperty property, bool isIn,
         out List<string> choices, out int initialIndex)
        {
            var binderAdapter = target as ComponentBinderAdapter;
            choices = new List<string>();
            initialIndex = 0;

            var timelineAsset = GetTimelineAsset();
            if (timelineAsset == null)
            {
                return;
            }

            var tweenComponents = BindAdapterEditorHelper.GetTweenTypes(timelineAsset);
            foreach (ComponentTween componentTween in tweenComponents)
            {
                List<ComponentBindTracks> tweenNames = componentTween.tweenNames;
                choices.AddRange(tweenNames.Select((ts) => ts.tweenName).ToList());
            }

            initialIndex = choices.FindIndex((ts) =>
            {
                return BindAdapterEditorHelper.GetTweenName(ts) == property.stringValue;
            });

            if (initialIndex < 0 && choices.Count > 0)
            {
                string postFix = isIn ? TweenTimelineDefine.InDefine : TweenTimelineDefine.OutDefine;
                string tweenName = $"{binderAdapter.transform.name}_{postFix}";
                initialIndex = choices.FindIndex((ts) =>
                {
                    return ts == tweenName;
                });
            }

            initialIndex = Math.Max(0, initialIndex);
            property.stringValue = BindAdapterEditorHelper.GetTweenName(choices[0]);
        }

        private Button CreatePreViewBtn(
            SerializedProperty nameProp)
        {
            var previewBtn = new Button(() =>
            {
                PreviewAction(nameProp);
            });
            previewBtn.style.minHeight = 40;
            previewBtn.style.minWidth = 40;
            // var btnBackground = new Background();
            // previewBtn.iconImage = btnBackground;
            previewBtn.text = "preview";
            // btnBackground.texture = (Texture2D)EditorGUIUtility.IconContent("d_PlayButton On@2x").image;
            return previewBtn;
        }

        public void PreviewAction(SerializedProperty nameProp)
        {
            var binder = target as ComponentBinderAdapter;
            ComponentBindTracks componentBindTracks = GetBindTrack(nameProp);
            Assert.IsNotNull(componentBindTracks, $"{nameProp.displayName} don't have match ComponentBindTracks");
            TweenTimelineManager.InitPreTween();
            CancelTween();

            var resetActions = BindAdapterEditorHelper.GetResetActions(binder, componentBindTracks);
            _curSequence = ((ITweenBinding)binder).Play(nameProp.stringValue, cycles: GetLoopCount());
            float delayResetTime = TweenTimelinePreferencesProvider.GetFloat(ActionEditorSettings.DelayResetTime);
            // _curSequence.ChainDelay(delayResetTime).OnComplete(() =>
            // {
            // resetActions.ForEach(t => t?.Invoke());
            // });
            
            // encounter editor flot precision
            // https://github.com/KyryloKuzyk/PrimeTween/discussions/116
            _resetTweenID = EditorTweenCenter.RegisterDelayCallback(binder, _curSequence.durationTotal + delayResetTime,
            (_, _) =>
            {
                resetActions.ForEach(t => t?.Invoke());
            });

            _updateSequenceID = EditorTweenCenter.RegisterSequence(_curSequence,
                binder.transform, _curSequence.durationTotal);
        }

        private void CancelTween()
        {
            if (_curSequence.isAlive)
            {
                _curSequence.Complete();
                _curSequence.Stop();
            }
            EditorTweenCenter.UnRegisterEditorTimer(_resetTweenID);
            EditorTweenCenter.UnRegisterEditorTimer(_updateSequenceID);
        }

        private ComponentBindTracks GetBindTrack(SerializedProperty nameProp)
        {
            Assert.IsFalse(string.IsNullOrEmpty(nameProp.stringValue), $"{nameProp.displayName} is empty");
            var binderAdapter = target as ComponentBinderAdapter;
            var timelineAsset = GetTimelineAsset();
            string category = binderAdapter.transform.name;
            if (timelineAsset == null)
            {
                return null;
            }

            var tweenComponents = BindAdapterEditorHelper.GetTweenTypes(timelineAsset);
            foreach (var item in tweenComponents)
            {
                var tweenNames = item.tweenNames;
                var componentBindTracks = tweenNames.Find(t => BindAdapterEditorHelper.GetTweenName(t.tweenName) == nameProp.stringValue);
                if (componentBindTracks != null)
                {
                    return componentBindTracks;
                }
            }

            return null;
        }

        private int GetLoopCount()
        {
            var binder = target as CompositeBinder;
            if (binder.loopCount < 0)
            {
                return 5;
            }
            return binder.loopCount;
        }
    }
}
