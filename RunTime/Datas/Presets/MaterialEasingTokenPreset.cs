using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;
namespace Cr7Sund.TweenTimeLine
{

    [System.Serializable]
    public class MaterialEasingTokenPreset : BaseEasingTokenPreset
    {
        [FormerlySerializedAs("materialTokenKey")]
        public MaterialEasingToken tokenKey;
        public AnimationCurve animationCurve;

        public override string Name
        {
            get
            {
                return tokenKey.ToString();
            }
        }
        public override Easing Easing
        {
            get
            {
                return animationCurve;
            }
        }
        public override AnimationCurve Curve => animationCurve;
        public override Enum TokenType
        {
            get => tokenKey;
        }


        public override void Init(Enum enumType, EasingWrapper easing)
        {
            tokenKey = (MaterialEasingToken)enumType;
            animationCurve = easing.Curve;
        }

        public MaterialEasingTokenPreset()
        {
            animationCurve = AnimationCurve.Constant(0, 1, 0);
        }
    }

}
