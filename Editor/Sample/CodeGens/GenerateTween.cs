using Cr7Sund.TweenTimeLine;
using PrimeTween;
using UnityEngine;

public static class GenerateTween
{
    public static void Canvas_1InTween(ITweenBinding binding)
    {
        Sequence.Create()
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Graphic>("GraphicColorAControlAsset"), startValue: 0f, endValue: 1f, duration: 0.25f, startDelay: 0f, ease: binding.GetEasing("Emphasized"))
            )
            ;
    }

    public static void Canvas_2OutTween(ITweenBinding binding)
    {
        Sequence.Create()
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Graphic>("GraphicColorAControlAsset"), startValue: 1f, endValue: 0f, duration: 0.8166667f, startDelay: 0.25f, ease: binding.GetEasing("Standard"))
            )
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Graphic>("GraphicColorAControlAsset"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 1.066667f, ease: binding.GetEasing("Standard"))
            )
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.Transform>("TransformLocalScaleControlAsset"), startValue: new Vector3(0.00f, 0.00f, 0.00f), endValue: new Vector3(1.00f, 1.00f, 1.00f), duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard"))
            )
            ;
    }

}
