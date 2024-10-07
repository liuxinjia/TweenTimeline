using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(CompositeBinder))]
    public class CompositeBinderEditor : PanelBinderEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            DrawUI();
            var loopCountProp = serializedObject.FindProperty(nameof(CompositeBinder.loopCount));
            IntegerField loopField = new IntegerField();
            loopField.label = "Loop Count";
            loopField.BindProperty(loopCountProp);
            _root.Add(loopField);
            return _root;
        }

        protected override int GetLoopCount()
        {
            var binder = target as CompositeBinder;
            return binder.loopCount;
        }
    }
}
