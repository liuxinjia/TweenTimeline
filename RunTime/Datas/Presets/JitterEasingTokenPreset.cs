using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;
namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class JitterEasingTokenPreset : BaseEasingTokenPreset
    {
        [FormerlySerializedAs("jitterTokenKey")]
        public JitterEasingToken tokenKey;
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
        // public override Enum TokenType
        // {
        //     get=>tokenKey;
        // }

        public JitterEasingTokenPreset()
        {
            animationCurve = AnimationCurve.Constant(0, 1, 0);
        }

        public override void Init(string enumType, EasingWrapper easing)
        {
            tokenKey = Enum.Parse<JitterEasingToken>(enumType);
            animationCurve = easing.Curve;
        }
    }
}
