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
        // public override Enum TokenType
        // {
        //     get => tokenKey;
        // }

        public MaterialEasingTokenPreset()
        {
            animationCurve = AnimationCurve.Constant(0, 1, 0);
        }

        public override void Init(string enumType, EasingWrapper easing)
        {
            tokenKey = Enum.Parse<MaterialEasingToken>(enumType);
            animationCurve = easing.Curve;
        }

        public override BaseEasingTokenPreset GetReverseEasing(EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            switch (tokenKey)
            {
                case MaterialEasingToken.StandardAccelerate:
                    return easingTokenPresetLibrary.GetEasePreset(MaterialEasingToken.StandardDecelerate);
                case MaterialEasingToken.StandardDecelerate:
                    return easingTokenPresetLibrary.GetEasePreset(MaterialEasingToken.StandardAccelerate);
                case MaterialEasingToken.EmphasizedAccelerate:
                    return easingTokenPresetLibrary.GetEasePreset(MaterialEasingToken.EmphasizedDecelerate);
                case MaterialEasingToken.EmphasizedDecelerate:
                    return easingTokenPresetLibrary.GetEasePreset(MaterialEasingToken.EmphasizedAccelerate);
                case MaterialEasingToken.Standard:
                    return easingTokenPresetLibrary.GetEasePreset(MaterialEasingToken.Emphasized);
                case MaterialEasingToken.Emphasized:
                    return easingTokenPresetLibrary.GetEasePreset(MaterialEasingToken.Standard);
                default:
                    return base.GetReverseEasing(easingTokenPresetLibrary);
            }
        }



        protected override double GetReverseDelta(double duration, int isIn)
        {
            if (tokenKey == MaterialEasingToken.StandardAccelerate
                || tokenKey == MaterialEasingToken.StandardDecelerate)
            {
                return 0.05d;
            }

            return base.GetReverseDelta(duration, isIn);
        }
    }

}
