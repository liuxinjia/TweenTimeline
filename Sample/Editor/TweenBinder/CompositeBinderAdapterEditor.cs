using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    public class CompositeBinderEditor : ComponentBinderAdapterEditor
    {
        protected virtual List<string> _tweenFields { get; }

        protected override void OnRebind(ComponentBinderAdapter binderAdapter, string category)
        {
            foreach (var tweenField in _tweenFields)
            {
                var tweenTypeProperty = serializedObject.FindProperty(tweenField);
                GetTweenNamMenus(binderAdapter, tweenTypeProperty, category,
                 out var choices, out var initialIndex);
                RebindComponent(category, tweenTypeProperty.stringValue);
            }
        }

        protected override VisualElement CreateTopField(string category)
        {
            var topField = new VisualElement();
            topField.style.flexDirection = FlexDirection.Column;
            foreach (var tweeName in _tweenFields)
            {
                topField.Add(AddTweenField(serializedObject.FindProperty(tweeName), category));
            }
            return topField;
        }
    }
}
