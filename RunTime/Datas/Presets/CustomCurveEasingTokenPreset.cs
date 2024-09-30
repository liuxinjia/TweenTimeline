using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;
namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class CustomCurveEasingTokenPreset : BaseEasingTokenPreset
    {
        public string tokenKey;
        public AnimationCurve animationCurve;

        public override string Name
        {
            get
            {
                return tokenKey;
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


        public CustomCurveEasingTokenPreset()
        {
            animationCurve = AnimationCurve.Constant(0, 1, 0);
        }

        public override void Init(string enumType, EasingWrapper easing)
        {
            tokenKey = enumType;
            animationCurve = easing.Curve;
        }
    }
}
