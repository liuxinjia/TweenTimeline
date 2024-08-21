using PrimeTween;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{

    [System.Serializable]
    public class RectTransformControlBehaviour : BaseControlBehaviour<RectTransform>
    {
        public Vector3 EndPos;


        protected override void ExecuteTween(float duration)
        {
            Tween.LocalPosition(_target, ease: Ease, endValue: EndPos, duration: duration);
        }
    }
}