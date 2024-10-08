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

            return _root;
        }


    }
}
