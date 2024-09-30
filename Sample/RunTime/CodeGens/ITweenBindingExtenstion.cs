using Cr7Sund.TweenTimeLine;
using PrimeTween;
using UnityEngine;

public static class ITweenBindingExtenstion
{
        public static Sequence Play(this ITweenBinding tweenBinding, string tweenBehaviour)
{
    if (tweenBehaviour == nameof(GenerateTween. FadeBtn_InTween))
    {
       return GenerateTween.FadeBtn_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. GrowBtn_InTween))
    {
       return GenerateTween.GrowBtn_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. HomePanel_InTween))
    {
       return GenerateTween.HomePanel_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. SettingsPanel_InTween))
    {
       return GenerateTween.SettingsPanel_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. SettingsPanel_OutTween))
    {
       return GenerateTween.SettingsPanel_OutTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. HomePanel_OutTween))
    {
       return GenerateTween.HomePanel_OutTween(tweenBinding);
    }
            return Sequence.Create();
}
}
