
using System;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class SequenceTimeScaleControlBehaviour : BaseControlBehaviour<Sequence, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Sequence target, double duration, float startValue)
        {
            return PrimeTween.Tween.TweenTimeScale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Sequence target)
        {
            return target.timeScale;
        }

        protected override void OnSet(Sequence target, float updateValue)
        {
            target.timeScale = updateValue;
        }
    }
}
