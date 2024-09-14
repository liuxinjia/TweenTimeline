using Cr7Sund.TweenTimeLine;
using PrimeTween;
using UnityEngine;

public static class GenerateTween
{
    public static Sequence Canvas_1OutTween(ITweenBinding binding)
    {
        return Sequence.Create()
           .Group(Tween.Scale(binding.GetBindObj<UnityEngine.RectTransform>("Image"), startValue: new Vector3(1.00f, 1.00f, 1.00f), endValue: new Vector3(0.00f, 0.00f, 0.00f), duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard")))
                .InsertCallback(0.15f, binding.GetBindObj<UnityEngine.RectTransform>("Image"), (target) => { target.hasChanged= false;})
           .Group(Tween.Custom(binding.GetBindObj<TMPro.TextMeshProUGUI>("Text (TMP)"), startValue: 0f, endValue: 23f, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard"), onValueChange: (t, updateValue) => t. characterSpacing = updateValue))
                .InsertCallback(0.2f, binding.GetBindObj<TMPro.TextMeshProUGUI>("Text (TMP)"), (target) => { target.text= "";})
           .Group(Tween.TextMaxVisibleCharacters(binding.GetBindObj<TMPro.TextMeshProUGUI>("Text (TMP)"), startValue: 99999, endValue: 99999, duration: 0.3f, startDelay: 0f, ease: binding.GetEasing("Standard")))
            ;

    }

}
