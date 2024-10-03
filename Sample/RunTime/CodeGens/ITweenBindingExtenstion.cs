using Cr7Sund.TweenTimeLine;
using PrimeTween;
using UnityEngine;

public static class ITweenBindingExtenstion
{
        public static Sequence Play(this ITweenBinding tweenBinding, string tweenBehaviour)
{
    if (tweenBehaviour == nameof(GenerateTween. ScaleClickBtn_InTween))
    {
       return GenerateTween.ScaleClickBtn_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. FadeClickBtn_InTween))
    {
       return GenerateTween.FadeClickBtn_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. FillHoverBtn_InTween))
    {
       return GenerateTween.FillHoverBtn_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. FillHoverBtn_OutTween))
    {
       return GenerateTween.FillHoverBtn_OutTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. HomePanel_InTween))
    {
       return GenerateTween.HomePanel_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. SettingsPanel_InTween))
    {
       return GenerateTween.SettingsPanel_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. TansitionHolderPanel_InTween))
    {
       return GenerateTween.TansitionHolderPanel_InTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. SettingsPanel_OutTween))
    {
       return GenerateTween.SettingsPanel_OutTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. HomePanel_OutTween))
    {
       return GenerateTween.HomePanel_OutTween(tweenBinding);
    }
    if (tweenBehaviour == nameof(GenerateTween. TansitionHolderPanel_OutTween))
    {
       return GenerateTween.TansitionHolderPanel_OutTween(tweenBinding);
    }
            return Sequence.Create();
}
}
