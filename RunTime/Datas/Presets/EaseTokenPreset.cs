using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;
namespace Cr7Sund.TweenTimeLine
{
    // why we don't store it at easeLibrary
    // since we still use it as Serializable field in TrackAset
    [System.Serializable]
    public class EaseTokenPreset : BaseEasingTokenPreset
    {
        [FormerlySerializedAs("ease")]
        public PrimeTween.Ease tokenKey;

        public override string Name
        {
            get
            {
                return tokenKey.ToString();
            }
        }
        public override Enum TokenType
        {
            get => tokenKey;
        }
        public override Easing Easing => tokenKey;
        public override AnimationCurve Curve => null;

        public override void Init(Enum enumType, EasingWrapper easing)
        {
            tokenKey = (Ease)enumType;
        }

#if UNITY_EDITOR
        public override BaseEasingTokenPreset GetReverseEasing(EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            switch (tokenKey)
            {
                case Ease.InSine:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutSine);
                case Ease.OutSine:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InSine);
                case Ease.InOutSine:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutSine);
                case Ease.InQuad:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutQuad);
                case Ease.OutQuad:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InQuad);
                case Ease.InOutQuad:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutQuad);
                case Ease.InCubic:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutCubic);
                case Ease.OutCubic:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InCubic);
                case Ease.InOutCubic:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutCubic);
                case Ease.InQuart:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutQuart);
                case Ease.OutQuart:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InQuart);
                case Ease.InOutQuart:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutQuart);
                case Ease.InQuint:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutQuint);
                case Ease.OutQuint:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InQuint);
                case Ease.InOutQuint:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutQuint);
                case Ease.InExpo:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutExpo);
                case Ease.OutExpo:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InExpo);
                case Ease.InCirc:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutCirc);
                case Ease.OutCirc:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InCirc);
                case Ease.InElastic:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutElastic);
                case Ease.OutElastic:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InElastic);
                case Ease.InBack:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutBack);
                case Ease.OutBack:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InBack);
                case Ease.InBounce:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.OutBounce);
                case Ease.OutBounce:
                    return easingTokenPresetLibrary.GetEasePreset(Ease.InBounce);
                // Add more cases as needed for other easing types
                default:
                    break;
            }
            return base.GetReverseEasing(easingTokenPresetLibrary); // Corrected method call
        }
#endif


    }
}
