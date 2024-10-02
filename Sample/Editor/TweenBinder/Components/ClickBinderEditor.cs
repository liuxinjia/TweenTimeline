using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(ClickerAdapter))]

    public class ClickBinderEditor : ComponentBinderAdapterEditor
    {
        protected override List<string> _tweenFields => new List<string>()
        { "_onPointerClickTween",  };


        public override VisualElement CreateInspectorGUI()
        {
            return CreateInspector();
        }
    }
}
