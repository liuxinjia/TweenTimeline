using Cr7Sund.TweenTimeLine;
using PrimeTween;
using UnityEngine;

public static class ITweenBindingExtenstion
{
   public static Sequence Play(this ITweenBinding tweenBinding, string tweenBehaviour, int cycles = 1, CycleMode cycleMode = CycleMode.Restart, Ease sequenceEase = Ease.Linear)
   {
      if (tweenBehaviour == nameof(GenerateTween.ScaleClickBtn_InTween))
      {
         return GenerateTween.ScaleClickBtn_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.FadeClickBtn_InTween))
      {
         return GenerateTween.FadeClickBtn_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.FillHoverBtn_InTween))
      {
         return GenerateTween.FillHoverBtn_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.FillHoverBtn_OutTween))
      {
         return GenerateTween.FillHoverBtn_OutTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.HomePanel_InTween))
      {
         return GenerateTween.HomePanel_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.SettingsPanel_InTween))
      {
         return GenerateTween.SettingsPanel_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.TansitionHolderPanel_InTween))
      {
         return GenerateTween.TansitionHolderPanel_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.SettingsPanel_OutTween))
      {
         return GenerateTween.SettingsPanel_OutTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.HomePanel_OutTween))
      {
         return GenerateTween.HomePanel_OutTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.TansitionHolderPanel_OutTween))
      {
         return GenerateTween.TansitionHolderPanel_OutTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.FadeSelectButton_InTween))
      {
         return GenerateTween.FadeSelectButton_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.ColorPointerEnterButton_InTween))
      {
         return GenerateTween.ColorPointerEnterButton_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.IndicatorComposite_InTween))
      {
         return GenerateTween.IndicatorComposite_InTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.FadeSelectButton_OutTween))
      {
         return GenerateTween.FadeSelectButton_OutTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      if (tweenBehaviour == nameof(GenerateTween.ColorPointerExitButton_OutTween))
      {
         return GenerateTween.ColorPointerExitButton_OutTween(tweenBinding, cycles, cycleMode, sequenceEase);
      }
      return Sequence.Create();
   }
}
