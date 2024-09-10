using Cr7Sund.TweenTimeLine;
using PrimeTween;
using UnityEngine;

public static class GenerateTween
{
    public static void Canvas_1InTween(ITweenBinding binding)
    {
        Sequence.Create()
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Graphic>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0.7333333f, ease: binding.GetEasing("NewAnimationImageAlpha")))
           .Group(Tween.ScaleX(binding.GetBindObj<UnityEngine.Transform>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0.7333333f, ease: binding.GetEasing("NewAnimationRectTransformScaleX")))
           .Group(Tween.ScaleY(binding.GetBindObj<UnityEngine.Transform>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0.7333333f, ease: binding.GetEasing("NewAnimationRectTransformScaleY")))
           .Group(Tween.ScaleZ(binding.GetBindObj<UnityEngine.Transform>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0.7333333f, ease: binding.GetEasing("NewAnimationRectTransformScaleZ")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Graphic>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0f, ease: binding.GetEasing("NewAnimationImageAlpha")))
           .Group(Tween.ScaleX(binding.GetBindObj<UnityEngine.Transform>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0f, ease: binding.GetEasing("NewAnimationRectTransformScaleX")))
           .Group(Tween.ScaleY(binding.GetBindObj<UnityEngine.Transform>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0f, ease: binding.GetEasing("NewAnimationRectTransformScaleY")))
           .Group(Tween.ScaleZ(binding.GetBindObj<UnityEngine.Transform>("image"), startValue: 0f, endValue: 1f, duration: 0.5166667f, startDelay: 0f, ease: binding.GetEasing("NewAnimationRectTransformScaleZ")))
           .Group(Sequence.Create()
                .InsertCallback(1.75f, binding.GetBindObj<UnityEngine.AudioSource>("Canvas_1"), (target) => target.Stop())
                .InsertCallback(0.1666667f, binding.GetBindObj<UnityEngine.AudioSource>("Canvas_1"), (target) => { binding.PlayAudioClip(target, "door", 1.2f);})
                .InsertCallback(0.9666666f, binding.GetBindObj<UnityEngine.AudioSource>("Canvas_1"), (target) => { binding.PlayAudioClip(target, "swordHits", 0f);})
           )
            ;
    }

}
