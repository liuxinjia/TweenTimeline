using Cr7Sund.TweenTimeLine;
using PrimeTween;
using UnityEngine;

public static class GenerateTween
{
    public static Sequence FadeBtn_InTween(ITweenBinding binding)
    {
        return Sequence.Create()
           .Group(Tween.Alpha(binding.GetBindObj<TMPro.TextMeshProUGUI>("UpgradeText_TextMeshProUGUI"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("UpgradeBtn_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard")))
            ;
    }

    public static Sequence GrowBtn_InTween(ITweenBinding binding)
    {
        return Sequence.Create()
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("UpgradeBtn_RectTransform"), startValue: new Vector3(1.00f, 1.00f, 1.00f), endValue: new Vector3(1.20f, 1.20f, 1.20f), duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("InQuad")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("UpgradeBtn_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("InExpo")))
            ;
    }

    public static Sequence HomePanel_InTween(ITweenBinding binding)
    {
        return Sequence.Create()
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("Start_RectTransform"), startValue: -303f, endValue: 1269f, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("StandardAccelerate")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("Start_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("OutQuad")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("UpgradeBtn_RectTransform"), startValue: -303f, endValue: 1269f, duration: 0.3f, startDelay: 0.05f, ease: binding.GetEasing("Elastic")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("UpgradeBtn_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0.05f, ease: binding.GetEasing("Standard")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("Settings_RectTransform"), startValue: -303f, endValue: 1269f, duration: 0.3f, startDelay: 0.1f, ease: binding.GetEasing("Overshoot")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("Settings_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0.1f, ease: binding.GetEasing("Standard")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("Exit_RectTransform"), startValue: -303f, endValue: 1269f, duration: 0.3f, startDelay: 0.15f, ease: binding.GetEasing("Standard")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("Exit_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0.15f, ease: binding.GetEasing("Standard")))
           .Group(Tween.Alpha(binding.GetBindObj<TMPro.TextMeshProUGUI>("Title_TextMeshProUGUI"), startValue: 0f, endValue: 1f, duration: 0.5f, startDelay: 0.1f, ease: binding.GetEasing("EmphasizedDecelerate")))
           .Group(Tween.UIAnchoredPositionY(binding.GetBindObj<UnityEngine.RectTransform>("Title_RectTransform"), startValue: 969.6f, endValue: 263f, duration: 0.5f, startDelay: 0.1f, ease: binding.GetEasing("Standard")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("leftMask_RectTransform"), startValue: -1791f, endValue: -787f, duration: 0.3f, startDelay: 0.1f, ease: binding.GetEasing("Standard")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("leftMask_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0.1f, ease: binding.GetEasing("Standard")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("rightMask_RectTransform"), startValue: 1842f, endValue: 810f, duration: 0.3f, startDelay: 0.1f, ease: binding.GetEasing("Standard")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("rightMask_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0.1f, ease: binding.GetEasing("Standard")))
            ;
    }

    public static Sequence SettingsPanel_InTween(ITweenBinding binding)
    {
        return Sequence.Create()
           .Group(Tween.UIAnchoredPosition(binding.GetBindObj<UnityEngine.RectTransform>("NextBtn_RectTransform"), startValue: new Vector2(-1631.00f, 588.25f), endValue: new Vector2(-1020.00f, 586.40f), duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("StandardDecelerate")))
           .Group(Tween.UIAnchoredPosition(binding.GetBindObj<UnityEngine.RectTransform>("Title_RectTransform"), startValue: new Vector2(0.13f, 825.00f), endValue: new Vector2(0.00f, 263.00f), duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard")))
           .Group(Tween.UIAnchoredPosition(binding.GetBindObj<UnityEngine.RectTransform>("SettingsBackground_RectTransform"), startValue: new Vector2(-2171.00f, 0.00f), endValue: new Vector2(0.00f, 0.00f), duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("SettingsBackground_Image"), startValue: 0f, endValue: 1f, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("SoundSlider_RectTransform"), startValue: new Vector3(0.00f, 0.00f, 0.00f), endValue: new Vector3(0.00f, 0.00f, 0.00f), duration: 0.2f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedAccelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("MusicVolumeSlider_RectTransform"), startValue: new Vector3(0.00f, 0.00f, 0.00f), endValue: new Vector3(1.00f, 1.00f, 1.00f), duration: 0.2f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedAccelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("MasterVolumeSlider_RectTransform"), startValue: new Vector3(0.00f, 0.00f, 0.00f), endValue: new Vector3(1.00f, 1.00f, 1.00f), duration: 0.2f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedAccelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("MusicSwtich_RectTransform"), startValue: new Vector3(0.00f, 0.00f, 0.00f), endValue: new Vector3(1.00f, 1.00f, 1.00f), duration: 0.2f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedAccelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("DialogueSwtich_RectTransform"), startValue: new Vector3(0.00f, 0.00f, 0.00f), endValue: new Vector3(1.00f, 1.00f, 1.00f), duration: 0.2f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedAccelerate")))
            ;
    }

    public static Sequence SettingsPanel_OutTween(ITweenBinding binding)
    {
        return Sequence.Create()
           .Group(Tween.UIAnchoredPosition(binding.GetBindObj<UnityEngine.RectTransform>("NextBtn_RectTransform"), startValue: new Vector2(-1020.00f, 586.40f), endValue: new Vector2(-1631.00f, 588.25f), duration: 0.35f, startDelay: 0f, ease: binding.GetEasing("StandardAccelerate")))
           .Group(Tween.UIAnchoredPosition(binding.GetBindObj<UnityEngine.RectTransform>("Title_RectTransform"), startValue: new Vector2(0.00f, 263.00f), endValue: new Vector2(0.13f, 825.00f), duration: 0.4f, startDelay: 0f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.UIAnchoredPosition(binding.GetBindObj<UnityEngine.RectTransform>("SettingsBackground_RectTransform"), startValue: new Vector2(0.00f, 0.00f), endValue: new Vector2(-2171.00f, 0.00f), duration: 0.4f, startDelay: 0f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("SettingsBackground_Image"), startValue: 1f, endValue: 0f, duration: 0.4f, startDelay: 0f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("SoundSlider_RectTransform"), startValue: new Vector3(0.00f, 0.00f, 0.00f), endValue: new Vector3(0.00f, 0.00f, 0.00f), duration: 0.3f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedDecelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("MusicVolumeSlider_RectTransform"), startValue: new Vector3(1.00f, 1.00f, 1.00f), endValue: new Vector3(0.00f, 0.00f, 0.00f), duration: 0.3f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedDecelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("MasterVolumeSlider_RectTransform"), startValue: new Vector3(1.00f, 1.00f, 1.00f), endValue: new Vector3(0.00f, 0.00f, 0.00f), duration: 0.3f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedDecelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("MusicSwtich_RectTransform"), startValue: new Vector3(1.00f, 1.00f, 1.00f), endValue: new Vector3(0.00f, 0.00f, 0.00f), duration: 0.3f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedDecelerate")))
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("DialogueSwtich_RectTransform"), startValue: new Vector3(1.00f, 1.00f, 1.00f), endValue: new Vector3(0.00f, 0.00f, 0.00f), duration: 0.3f, startDelay: 0.3f, ease: binding.GetEasing("EmphasizedDecelerate")))
            ;
    }

    public static Sequence HomePanel_OutTween(ITweenBinding binding)
    {
        return Sequence.Create()
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("Start_RectTransform"), startValue: 1269f, endValue: -303f, duration: 0.35f, startDelay: 0f, ease: binding.GetEasing("StandardDecelerate")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("Start_Image"), startValue: 1f, endValue: 0f, duration: 0.4f, startDelay: 0f, ease: binding.GetEasing("InQuad")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("UpgradeBtn_RectTransform"), startValue: 1269f, endValue: -303f, duration: 0.4f, startDelay: 0.05f, ease: binding.GetEasing("Elastic")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("UpgradeBtn_Image"), startValue: 1f, endValue: 0f, duration: 0.4f, startDelay: 0.05f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("Settings_RectTransform"), startValue: 1269f, endValue: -303f, duration: 0.4f, startDelay: 0.1f, ease: binding.GetEasing("Overshoot")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("Settings_Image"), startValue: 1f, endValue: 0f, duration: 0.4f, startDelay: 0.1f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("Exit_RectTransform"), startValue: 1269f, endValue: -303f, duration: 0.4f, startDelay: 0.15f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("Exit_Image"), startValue: 1f, endValue: 0f, duration: 0.4f, startDelay: 0.15f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.Alpha(binding.GetBindObj<TMPro.TextMeshProUGUI>("Title_TextMeshProUGUI"), startValue: 1f, endValue: 0f, duration: 0.6f, startDelay: 0.1f, ease: binding.GetEasing("EmphasizedAccelerate")))
           .Group(Tween.UIAnchoredPositionY(binding.GetBindObj<UnityEngine.RectTransform>("Title_RectTransform"), startValue: 263f, endValue: 969.6f, duration: 0.6f, startDelay: 0.1f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("leftMask_RectTransform"), startValue: -787f, endValue: -1791f, duration: 0.4f, startDelay: 0.1f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("leftMask_Image"), startValue: 1f, endValue: 0f, duration: 0.4f, startDelay: 0.1f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.UIAnchoredPositionX(binding.GetBindObj<UnityEngine.RectTransform>("rightMask_RectTransform"), startValue: 810f, endValue: 1842f, duration: 0.4f, startDelay: 0.1f, ease: binding.GetEasing("Emphasized")))
           .Group(Tween.Alpha(binding.GetBindObj<UnityEngine.UI.Image>("rightMask_Image"), startValue: 1f, endValue: 0f, duration: 0.4f, startDelay: 0.1f, ease: binding.GetEasing("Emphasized")))
            ;
    }

}
