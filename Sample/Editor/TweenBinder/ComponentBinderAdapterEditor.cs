using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;
using UnityEditor.UIElements;
using PrimeTween;
using System.Reflection;
using System;

namespace Cr7Sund.TweenTimeLine
{
    public class ComponentBinderAdapterEditor : UnityEditor.Editor
    {
        private ComponentTweenCollection _tweenActionCollection;
        private Sequence _curSequence;
        private string _updateSequenceID;

        protected VisualElement CreateInspector()
        {
            var root = new VisualElement();
            ComponentBinderAdapter binderAdapter = target as ComponentBinderAdapter;
            var cacheListProp = serializedObject.FindProperty("cacheList");
            var category = BindAdapterEditorHelper.GetCategory(binderAdapter.gameObject);

            _tweenActionCollection = AssetDatabase.LoadAssetAtPath<ComponentTweenCollection>(TweenTimelineDefine.componentTweenCollectionPath);
            if (_tweenActionCollection == null)
            {
                _tweenActionCollection = ScriptableObject.CreateInstance<ComponentTweenCollection>();
                AssetDatabase.CreateAsset(_tweenActionCollection, TweenTimelineDefine.componentTweenCollectionPath);
            }
            BindAdapterEditorHelper.Reset(binderAdapter);

            var topField = CreateTopField(category);

            var cacheListPropField = BindAdapterEditorHelper.CreateList(cacheListProp, binderAdapter,
            null);

            if (binderAdapter.cacheList == null
                || binderAdapter.cacheList.Count == 0)
            {
                OnRebind(binderAdapter, category);
            }

            var rebindBtn = new Button(() =>
            {
                OnRebind(binderAdapter, category);
            });
            rebindBtn.text = "Rebind";

            if (topField != null)
            {
                root.Add(topField);
            }
            root.Add(cacheListPropField);
            root.Add(rebindBtn);
            return root;
        }

        protected virtual VisualElement CreateTopField(string category)
        {
            return null;
        }

        protected virtual void OnRebind(ComponentBinderAdapter binderAdapter, string category)
        {

        }

        protected VisualElement AddTweenField(SerializedProperty nameProp, string category)
        {
            var binder = target as ComponentBinderAdapter;
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            var popup = CreateMenu(binder, nameProp, category);
            var previewBtn = CreatePreViewBtn(binder, nameProp, category);

            container.Add(popup);
            container.Add(previewBtn);
            return container;
        }

        private Button CreatePreViewBtn(ComponentBinderAdapter binder,
         SerializedProperty nameProp, string category)
        {
            var previewBtn = new Button(() =>
            {
                PreviewAction(binder, nameProp, category);
            });
            previewBtn.style.minHeight = 40;
            previewBtn.style.minWidth = 40;
            // var btnBackground = new Background();
            // previewBtn.iconImage = btnBackground;
            previewBtn.text = "preview";
            // btnBackground.texture = (Texture2D)EditorGUIUtility.IconContent("d_PlayButton On@2x").image;
            return previewBtn;
        }

        public void PreviewAction(ComponentBinderAdapter binder, SerializedProperty nameProp, string category)
        {
            List<ComponentBindTracks> tweenNames = _tweenActionCollection.GetTweenActions(category);
            var componentBindTracks = tweenNames.Find(t => BindAdapterEditorHelper.GetTweenName(t.tweenName) == nameProp.stringValue);

            TweenTimelineManager.InitPreTween();
            if (!string.IsNullOrEmpty(_updateSequenceID))
            {
                EditorTweenCenter.UnRegisterEditorTimer(_updateSequenceID);
                _curSequence.Stop();
            }
            var resetActions = BindAdapterEditorHelper.GetResetActions(binder, componentBindTracks);
            _curSequence = ((ITweenBinding)binder).Play(nameProp.stringValue);

            float dealyResetTime = TweenTimelinePreferencesProvider.GetFloat(ActionEditorSettings.DealyResetTime);
            _updateSequenceID = EditorTweenCenter.RegisterSequence(_curSequence,
             binder.transform, _curSequence.duration);
            EditorTweenCenter.RegisterDelayCallback(target,
              _curSequence.duration + dealyResetTime, (_, _) =>
              {
                  _updateSequenceID = string.Empty;
                  resetActions.ForEach(t => t?.Invoke());
              });
        }

        private VisualElement CreateMenu(ComponentBinderAdapter binderAdapter, SerializedProperty property, string category)
        {
            GetTweenNamMenus(binderAdapter, property, category, out var choices, out var initialIndex);

            string tweenName = choices[initialIndex];
            var popup = new PopupField<string>(label: property.displayName, choices: choices,
                defaultValue: tweenName);
            popup.RegisterValueChangedCallback(evt =>
            {
                property.stringValue = BindAdapterEditorHelper.GetTweenName(evt.newValue);
                property.serializedObject.ApplyModifiedProperties();
            });

            return popup;
        }

        protected void GetTweenNamMenus(ComponentBinderAdapter binderAdapter, SerializedProperty property,
            string category, out List<string> choices, out int initialIndex)
        {
            var tweenNames = _tweenActionCollection.GetTweenActions(category);
            Assert.IsFalse(tweenNames.Count <= 0);
            string tweenCategory = property.stringValue;
            if (string.IsNullOrEmpty(tweenCategory))
            {
                foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
                {
                    Component component = binderAdapter.gameObject.GetComponent(item.Value);
                    if (component)
                    {
                        tweenCategory = item.Key;
                        break;
                    }
                }
            }
            choices = tweenNames.Select((ts) => ts.tweenName).ToList();
            initialIndex = choices.FindIndex((ts) =>
            {
                return BindAdapterEditorHelper.GetTweenName(ts) == property.stringValue;
            });


            if (initialIndex < 0)
            {
                initialIndex = 0;
                property.stringValue = BindAdapterEditorHelper.GetTweenName(choices[0]);
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        protected void RebindComponent(string category, string tweenName)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(tweenName)) return;

            List<ComponentBindTracks> tweenNames = _tweenActionCollection.GetTweenActions(category);
            var componentBindTracks = tweenNames.Find((ts) => BindAdapterEditorHelper.GetTweenName(ts.tweenName) == tweenName);

            List<string> bindTargets = componentBindTracks.bindTargets;
            ComponentBinderAdapter binderAdapter = target as ComponentBinderAdapter;

            for (int i = 0; i < bindTargets.Count; i++)
            {
                reBind(binderAdapter, bindTargets[i],
                 componentBindTracks.bindTypes[i]);
            }

            void reBind(ComponentBinderAdapter binderAdapter, string name, string componentTypeFullName)
            {
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

    }
}
