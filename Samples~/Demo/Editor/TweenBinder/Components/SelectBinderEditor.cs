using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(SelectAdapter))]

    public class SelectBinderEditor : ComponentBinderAdapterEditor
    {
        protected override List<string> _tweenFields => new List<string>()
        { nameof(SelectAdapter.selectTween), nameof(SelectAdapter.deSelectTween)};


        public override VisualElement CreateInspectorGUI()
        {
            return CreateInspector();
        }
    }
}
