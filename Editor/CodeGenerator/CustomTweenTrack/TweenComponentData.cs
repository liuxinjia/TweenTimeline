using System.Collections.Generic;
using UnityEngine;
using static Cr7Sund.TweenTimeLine.TweenActionStep;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class TweenComponentData
    {
        public string ComponentType;
        public string ValueType;
        public string GetPropertyMethod;
        [SerializeField] private string SetPropertyMethod;
        [SerializeField] public string PreTweenMethod;


        public static HashSet<string> NotAdditiveSet = new()
        {
            nameof(TransformTween.Transform_LocalScaleControlBehaviour),
            nameof(GraphicTween.Graphic_ColorControlBehaviour),
            nameof(GraphicTween.Graphic_ColorAControlBehaviour),
            nameof(ImageTween.Image_FillAmountControlBehaviour),
        };

        public TweenComponentData()
        {
        }

        public TweenComponentData(string componentType, string valueType, string getPropertyMethod, string setPropertyMethod, string preTweenMethod)
        {
            ComponentType = componentType;
            ValueType = valueType;
            GetPropertyMethod = getPropertyMethod;
            SetPropertyMethod = setPropertyMethod;
            PreTweenMethod = preTweenMethod;
        }

        public string GetTweenMethod()
        {
            return PreTweenMethod;
        }

        public string GetSetMethod()
        {
            return SetPropertyMethod;
        }

        public string GetCustomTweenMethod()
        {
            // var result = SetPropertyMethod.Replace("target.", "");
            var result = SetPropertyMethod.TrimEnd(';');
            return result;
        }

        public TweenOperationType GetTweenOperationType(string tweenMethod){
            TweenOperationType tweenOperationType = TweenOperationType.Additive;
            if (NotAdditiveSet.Contains(tweenMethod))
            {
                tweenOperationType = TweenOperationType.Relative;
            }
            return tweenOperationType;
        }
    }
}
