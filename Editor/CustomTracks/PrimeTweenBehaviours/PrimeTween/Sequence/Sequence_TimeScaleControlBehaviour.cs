
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.SequenceTween
{
    [Serializable]
    public  class Sequence_TimeScaleControlBehaviour : BaseControlBehaviour<PrimeTween.Sequence, float>
    {
        protected override PrimeTween.Tween OnCreateTween(PrimeTween.Sequence target, double duration, float startValue)
        {
            return PrimeTween.Tween.TweenTimeScale(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(PrimeTween.Sequence target)
        {
            return target.timeScale;
        }

        protected override void OnSet(PrimeTween.Sequence target, float updateValue)
        {
           target.timeScale = updateValue;
        }
    }
}
