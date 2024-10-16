using System;
namespace Cr7Sund.TweenTimeLine
{
    public class EaseEnumConverter
    {
        #if DOTWEEN
        // Convert from DG.Tweening.Ease to PrimeTween.Ease
        public static PrimeTween.Ease ConvertToPrimeEase(DG.Tweening.Ease sourceEnum)
        {
            // Get the string representation of the DG.Tweening.Ease value
            string sourceString = Enum.GetName(typeof(DG.Tweening.Ease), sourceEnum);

            // Convert the string to PrimeTween.Ease
            if (Enum.TryParse(typeof(PrimeTween.Ease), sourceString, out var targetEnum))
            {
                return (PrimeTween.Ease)targetEnum;
            }
            if (sourceEnum == DG.Tweening.Ease.Unset)
            {
                return PrimeTween.Ease.Default;
            }

            throw new ArgumentException("Invalid DG.Tweening.Ease value", nameof(sourceEnum));
        }
        #endif

        // Convert from PrimeTween.Ease to DG.Tweening.Ease
        public static DG.Tweening.Ease ConvertToDGEase(PrimeTween.Ease targetEnum)
        {
            // Get the string representation of the PrimeTween.Ease value
            string targetString = Enum.GetName(typeof(PrimeTween.Ease), targetEnum);

            // Convert the string to DG.Tweening.Ease
            if (Enum.TryParse(typeof(DG.Tweening.Ease), targetString, out var sourceEnum))
            {
                return (DG.Tweening.Ease)sourceEnum;
            }
            if (targetEnum == PrimeTween.Ease.Default)
            {
                return DG.Tweening.Ease.Unset;
            }

            throw new ArgumentException("Invalid PrimeTween.Ease value", nameof(targetEnum));
        }
    }
}
