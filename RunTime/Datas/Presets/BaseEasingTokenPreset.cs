using System;
using PrimeTween;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{
    public struct EasingWrapper
    {
        public AnimationCurve Curve;
        public Ease Ease;
        public EasingWrapper(AnimationCurve curve, Ease ease = Ease.Custom)
        {
            Curve = curve;
            Ease = ease;
        }
    }

    [System.Serializable]
    public abstract class BaseEasingTokenPreset
    {
        public abstract string Name { get; }
        // public abstract Enum TokenType { get; }
        /// <summary>
        /// (Ease easeType \ AnimationCurve animCurve \ EaseFunction customEase)
        /// </summary>
        public abstract PrimeTween.Easing Easing { get; }
        public abstract AnimationCurve Curve { get; }

        public abstract void Init(string enumType, EasingWrapper easing);

        public virtual BaseEasingTokenPreset GetReverseEasing(EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            return this;
        }

        public double GetReverseDuration(double duration, int isIn)
        {
            double delta = GetReverseDelta(duration, isIn);
            delta *= Mathf.Sign(isIn) * -1;

            double result = duration + delta;
            return result > 0 ? result : duration;
        }

        protected virtual double GetReverseDelta(double duration, int isIn)
        {
            return 0.1f;
        }
    }
}
