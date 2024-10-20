﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cr7Sund.Timeline.Extension;
using PrimeTween;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    public class BindAdapterEditorHelper
    {

        public static void Reset(ComponentBinderAdapter binderAdapter)
        {
            binderAdapter.easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(
                TweenTimelineDefine.easingTokenPresetsPath);

        }

        public static ListView CreateList(SerializedProperty cacheListProp, ComponentBinderAdapter componentBinderAdapter,
             Action onValueChange)
        {
            Transform root = componentBinderAdapter.transform;

            var cacheListPropField = new ListView();
            cacheListPropField.allowAdd = true;
            cacheListPropField.allowRemove = true;
            cacheListPropField.showAddRemoveFooter = true;
            cacheListPropField.showBoundCollectionSize = false;
            cacheListPropField.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            cacheListPropField.BindProperty(cacheListProp);
            cacheListPropField.makeItem = () =>
            {
                var listItemContainer = new VisualElement();
                listItemContainer.style.flexDirection = FlexDirection.Row;
                ObjectField componentField = new ObjectField();
                Label label = new Label();
                label.name = "keyLabel";
                componentField.objectType = typeof(Component);
                componentField.style.flexGrow = 2;
                label.style.flexGrow = 1;

                listItemContainer.Add(label);
                listItemContainer.Add(componentField);

                return listItemContainer;
            };
            cacheListPropField.bindItem = (listItem, index) =>
            {
                SerializedProperty serializedProperty = cacheListProp.GetArrayElementAtIndex(index);
                string name = serializedProperty.FindPropertyRelative("key").stringValue;
                string[] strs = name.Split('_');
                Type componentType = GetComponentType(strs[1]);
                ObjectField objectField = listItem.Q<ObjectField>();

                objectField.RegisterValueChangedCallback((evt) =>
                {
                    if (evt.newValue == evt.previousValue)
                    {
                        return;
                    }
                    if (objectField.value is Component component)
                    {
                        if (!component.transform.IsChildOf(root))
                        {
                            Debug.LogError("Please assign the child of {root}");
                            return;
                        }
                    }
                    if (objectField.value == null)
                    {
                        objectField.parent.style.backgroundColor = Color.red;
                    }
                    else
                    {
                        objectField.parent.style.backgroundColor = Color.black;
                    }
                    onValueChange?.Invoke();
                });
                objectField.BindProperty(serializedProperty.FindPropertyRelative("component"));

                listItem.Q<Label>("keyLabel").text = strs[0];
                objectField.objectType = componentType;

            };
            return cacheListPropField;
        }

        public static List<Action> GetResetActions(ComponentBinderAdapter binder,
             ComponentBindTracks componentBindTracks)
        {
            var resultActions = new List<Action>();
            for (int i = 0; i < componentBindTracks.trackTypeNames.Count; i++)
            {
                string trackTypeName = componentBindTracks.trackTypeNames[i];
                if (trackTypeName == typeof(AudioTrack).FullName)
                {
                    continue;
                }

                var componentPairs = binder.cacheList.FirstOrDefault(
                    x => BindAdapterEditorHelper.GetTweenTarget(
                        componentBindTracks.bindTargets[i], componentBindTracks.bindTypes[i]) == x.key);
                var component = componentPairs.component;


                GetBehaviourInfo(trackTypeName,
                 out var getMethodInfo, out var setMethodInfo, out var behaviour);

                var initPos = getMethodInfo.Invoke(behaviour, new[]
                  { component});
                Action resetAction = () => setMethodInfo?.Invoke(behaviour, new[]
                {component, initPos });
                resultActions.Add(resetAction);
            }

            return resultActions;
        }

        private static void GetBehaviourInfo(string trackAssetName,
            out MethodInfo getMethodInfo, out MethodInfo setMethodInfo, out IUniqueBehaviour behaviour)
        {
            // / Graphic_ColorAControlTrack
            string tweenMethod = trackAssetName.Replace("ControlTrack", "ControlBehaviour");
            Assembly assembly = TweenActionStep.GetTweenTrackAssembly(trackAssetName);
            var tweenBehaviourType = assembly.GetType(tweenMethod);
            behaviour = Activator.CreateInstance(tweenBehaviourType) as IUniqueBehaviour;
            getMethodInfo = tweenBehaviourType.GetMethod("OnGet", BindingFlags.NonPublic | BindingFlags.Instance);
            setMethodInfo = tweenBehaviourType.GetMethod("OnSet", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static Type GetComponentType(string typeName)
        {
            System.Reflection.Assembly assembly = typeof(UnityEngine.UI.Button).Assembly;
            var resultType = assembly.GetType($"UnityEngine.UI.{typeName}");
            if (resultType == null)
            {
                assembly = typeof(UnityEngine.RectTransform).Assembly;
                resultType = assembly.GetType($"UnityEngine.{typeName}");
            }
            if (resultType == null)
            {
                assembly = typeof(TMPro.TextMeshProUGUI).Assembly;
                resultType = assembly.GetType($"TMPro.{typeName}");
            }

            return resultType;
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



        public static string GetCategory(GameObject go)
        {
            foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
            {
                var comp = go.GetComponent(item.Value);
                if (comp != null)
                {
                    return item.Key;
                }
            }

            return string.Empty;
        }

        public static string GetTweenName(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            return $"{name}Tween";
        }

        public static string GetTweenTarget(string name, string fullTypeName)
        {
            return $"{name}_{TypeConverter.GetSimplifyTypeName(fullTypeName)}";
        }

        public static List<ComponentBindTracks> GetTweenTypes(
            ComponentTweenCollection tweenActionCollection, string panelName)
        {
            string category = TweenTimelineDefine.PanelTag;
            if (!panelName.EndsWith(category))
            {
                category = TweenTimelineDefine.CompositeTag;
            }

            if (!panelName.EndsWith(category))
            {
                throw new Exception($"{panelName} should be endWith panel or Composite");
            }

            var tweenComponents = tweenActionCollection.GetTweenActions(category);
            var resultList = new List<ComponentBindTracks>();
            foreach (ComponentBindTracks item in tweenComponents)
            {
                if (item.tweenName.StartsWith(panelName))
                {
                    resultList.Add(item);
                }
            }

            return resultList;
        }

        public static void DrawLoopField(SerializedObject serializedObject, VisualElement container)
        {
            var loopCountProp = serializedObject.FindProperty(nameof(ComponentBinderAdapter.loopCount));
            IntegerField loopField = new IntegerField();
            loopField.label = "Loop Count";
            loopField.BindProperty(loopCountProp);
            container.Add(loopField);
        }
    }
}
