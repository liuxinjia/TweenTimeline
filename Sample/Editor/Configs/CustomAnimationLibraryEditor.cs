using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(CustomTweenActionLibrary))]
    public class CustomAnimationLibraryEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            // var root = base.CreateInspectorGUI();
            var root = new VisualElement();
            var listView = new ListView();
            var listProperty = serializedObject.FindProperty("animationCollections");

            listView.selectionType = SelectionType.Multiple;
            listView.allowAdd = true;
            listView.allowRemove = true;
            listView.itemsRemoved += OnRemove;
            listView.showAddRemoveFooter = true;
            listView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            listView.BindProperty(listProperty);

            root.Add(listView);
            root.Bind(serializedObject);
            listView.Rebuild();

            return root;
        }

        private void OnRemove(IEnumerable<int> indexes)
        {
            var library = target as CustomTweenActionLibrary;
            library.RemoveEffects(indexes.ToList());
        }
    }
}
