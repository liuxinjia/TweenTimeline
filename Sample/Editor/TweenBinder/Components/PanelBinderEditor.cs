using System;
using System.Collections.Generic;
using System.Linq;
using PlasticPipe.Client;
using PrimeTween;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using ObjectField = UnityEditor.UIElements.ObjectField;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(PanelBinder))]
    public class PanelBinderEditor : UnityEditor.Editor
    {
        private VisualElement _root;
        private Sequence _curSequence;
        private string _updateSequenceID;

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();
            var binderAdapter = target as PanelBinder;
            var cacheListProp = serializedObject.FindProperty("cacheList");
            var inTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.inTweenName));
            var outTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.outTweenName));
            var timelineAssetProp = serializedObject.FindProperty(nameof(binderAdapter.timelineAsset));
            BindAdapterEditorHelper.Reset(binderAdapter);

            var cacheListPropField = BindAdapterEditorHelper.CreateList(cacheListProp, binderAdapter,
            null);
            cacheListPropField.name = "cacheList";
            var rebindBtn = new Button(RebindComponents);
            rebindBtn.text = "Rebind";
            var inTweenMenu = AddTweenField(inTweenNameProp, true);
            var outTweenMenu = AddTweenField(outTweenNameProp);


            var timeLineField = new ObjectField();
            timeLineField.objectType = typeof(TimelineAsset);
            timeLineField.value = binderAdapter.timelineAsset;
            timeLineField.RegisterValueChangedCallback(evt =>
            {
                if (evt.previousValue == evt.newValue)
                {
                    return;
                }

                UpdateMenu(inTweenNameProp, true);
                UpdateMenu(outTweenNameProp);
                // RebindComponents();
            });
            timeLineField.BindProperty(timelineAssetProp); // if will trigger ValueChanged when switch each time

            _root.Add(timeLineField);
            _root.Add(inTweenMenu);
            _root.Add(outTweenMenu);
            _root.Add(cacheListPropField);
            _root.Add(rebindBtn);
            return _root;
        }

        private void UpdateMenu(SerializedProperty property, PopupField<string> popup, bool isIn = false)
        {
            GetTweenNamMenus(property, isIn, out var choices, out var initialIndex);
            popup.choices = choices;
            popup.index = initialIndex;
        }

        private void UpdateMenu(SerializedProperty property, bool isIn = false)
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
            var binderAdapter = target as PanelBinder;
            var timelineAsset = binderAdapter.timelineAsset;

            binderAdapter.cacheList.Clear();
            if (timelineAsset == null)
            {
                return;
            }

            var inTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.inTweenName));
            var outTweenNameProp = serializedObject.FindProperty(nameof(binderAdapter.outTweenName));


            List<ComponentTween> tweenComponents = BindAdapterEditorHelper.GetTweenTypes(timelineAsset);
            foreach (ComponentTween item in tweenComponents)
            {
                if (RebindComponent(item, inTweenNameProp.stringValue))
                {

                }
                if (RebindComponent(item, outTweenNameProp.stringValue))
                {

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
                    component = BindAdapterEditorHelper.GetComponent(componentTypeFullName, transform);
                }

                binderAdapter.cacheList.Add(new ComponentPairs()
                {
                    key = BindAdapterEditorHelper.GetTweenTarget(name, componentTypeFullName),
                    component =
                               component
                });
            }
        }

        protected void GetTweenNamMenus(SerializedProperty property, bool isIn,
         out List<string> choices, out int initialIndex)
        {
            var binderAdapter = target as PanelBinder;
            choices = new List<string>();
            initialIndex = 0;

            var timelineAsset = binderAdapter.timelineAsset;
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
            var binder = target as PanelBinder;
            ComponentBindTracks componentBindTracks = GetBindTrack(nameProp);

            TweenTimelineManager.InitPreTween();
            if (!string.IsNullOrEmpty(_updateSequenceID))
            {
                EditorTweenCenter.UnRegisterEditorTimer(_updateSequenceID);
                _curSequence.Stop();
            }
            var resetActions = BindAdapterEditorHelper.GetResetActions(binder, componentBindTracks);
            _curSequence = ((ITweenBinding)binder).Play(nameProp.stringValue);

            float delayResetTime = TweenTimelinePreferencesProvider.GetFloat(ActionEditorSettings.DelayResetTime);
            _updateSequenceID = EditorTweenCenter.RegisterSequence(_curSequence,
             binder.transform, _curSequence.duration);
            EditorTweenCenter.RegisterDelayCallback(target,
              _curSequence.duration + delayResetTime, (_, _) =>
              {
                  _updateSequenceID = string.Empty;
                  resetActions.ForEach(t => t?.Invoke());
              });
        }

        private ComponentBindTracks GetBindTrack(SerializedProperty nameProp)
        {
            var binderAdapter = target as PanelBinder;
            var timelineAsset = binderAdapter.timelineAsset;
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
    }
}
