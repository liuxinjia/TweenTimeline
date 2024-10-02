using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


namespace Cr7Sund.TweenTimeLine
{
    [CreateAssetMenu(menuName = "Cr7Sund/TweenTimeLine/CustomAnimationLibrary")]
    public class CustomTweenActionLibrary : ScriptableObject, ITweenActionLibrary
    {
        [SerializeField] public List<TweenActionEffect> animationCollections;

        public CustomTweenActionLibrary()
        {
            animationCollections = new();
        }

        [ContextMenu(nameof(AddCustomTracks))]
        public void AddCustomTracks()
        {
            var newAnimations = CustomTweenActionContainerBuilder.CreateCustomAnimationCollection();
            newAnimations.ForEach((effect)=>AddEffect(effect));
        }

        public void AddEffect(TweenActionEffect effect, string category = "Custom")
        {
            var existingEffectIndex = animationCollections.FindIndex(e => e.label == effect.label);
            if (existingEffectIndex >= 0)
            {
                // Update existing effect
                animationCollections[existingEffectIndex].CopyFrom(effect);
            }
            else
            {
                // Add new effect
                animationCollections.Add(effect);
            }
        }

        public void RemoveEffects(List<int> indices)
        {
            var setting = AssetDatabase.LoadAssetAtPath<TweenActionLibrary>(TweenTimelineDefine.tweenLibraryPath);

            indices.Sort((a, b) => b.CompareTo(a)); // Sort in descending order
            for (int i = 0; i < indices.Count; i++)
            {
                var item = animationCollections[indices[i]];
                animationCollections.RemoveAt(indices[i]);
                setting.RemoveEffect(item);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(setting);
            AssetDatabase.SaveAssetIfDirty(this);
            AssetDatabase.Refresh();
        }


        [ContextMenu(nameof(ApplyToSettings))]
        public void ApplyToSettings()
        {
            var setting = AssetDatabase.LoadAssetAtPath<TweenActionLibrary>(TweenTimelineDefine.tweenLibraryPath);
            foreach (var item in animationCollections)
            {
                setting.AddEffect(item);
            }
        }

        [MenuItem("Tools/AssemblyName")]
        public static void ShowFileID()
        {
            string fullName =
            AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));
            //  typeof(CustomTweenActionLibrary).Assembly.FullName;
            EditorGUIUtility.systemCopyBuffer = fullName;

            Debug.Log(fullName);
        }

    }



}
