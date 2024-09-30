using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(HoverBinder))]

    public class HoverBinderEditor : CompositeBinderEditor
    {
        protected override List<string> _tweenFields => new List<string>()
        { "_onPointerEnterTween", "_onPointerExitTween" };


        public override VisualElement CreateInspectorGUI()
        {
            return CreateInspector();
        }
    }
}
