using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;
namespace Cr7Sund.TweenTimeLine
{
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
            get=>tokenKey;
        }
        public override Easing Easing => tokenKey;
        public override AnimationCurve Curve => null;
        
        public override void Init(Enum enumType, EasingWrapper easing)
        {
            tokenKey = (Ease)enumType;
        }
    }
}
