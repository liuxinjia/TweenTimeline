using PrimeTween;
using UnityEngine;
using UnityEngine.Playables;
using Object = System.Object;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class EmptyBehaviour : BaseControlBehaviour<UnityEngine.Component, System.Object>
    {
        protected override Tween OnCreateTween(Component target, double duration, Object startValue)
        {
            return Tween.Custom(0, 1, 0.1f, _ =>
            {

            });
        }

        protected override object OnGet(Component target)
        {
            return null;
        }

        protected override void OnSet(Component target, Object intValue)
        {
        }
    }
}