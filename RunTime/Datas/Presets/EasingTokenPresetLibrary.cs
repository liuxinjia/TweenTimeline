using System;
using System.Collections.Generic;
using System.Linq;
using PrimeTween;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    // Unique Instance
    // [CreateAssetMenu(menuName = "Cr7Sund/TweenTimeLine/EasingTokenPresets", fileName = "EasingTokenPresets")]
    [System.Serializable]
    public class EasingTokenPresetLibrary : ScriptableObject
    {
        [SerializeReference] public List<BaseEasingTokenPreset> easingTokenPresets;

        public bool TryGetEasePreset(string easeName, out Easing resultEase)
        {
            var findIndex = -1;
            findIndex = easingTokenPresets.FindIndex(token => token.Name.Equals(easeName));
            if (findIndex >= 0)
            {
                resultEase = easingTokenPresets[findIndex].Easing;
                return true;
            }
            else
            {
                resultEase = PrimeTween.Ease.Linear;
                return false;
            }
        }
        public AnimationCurve GetEaseAnimationCurve(string easeToken)
        {
            if (Enum.TryParse<JitterEasingToken>(easeToken.ToString(), out var jitterEasingToken))
            {
                var jitterEasingTokenPreset = GetEasePreset(jitterEasingToken);
                if (jitterEasingTokenPreset != null)
                    return jitterEasingTokenPreset.Curve;
            }
            else if (Enum.TryParse<MaterialEasingToken>(easeToken.ToString(), out var materialEasingToken))
            {
                var materialEasingTokenPreset = GetEasePreset(materialEasingToken);
                if (materialEasingTokenPreset != null)
                    return materialEasingTokenPreset.Curve;
            }

            return AnimationCurve.Constant(0, 1, 1);
        }
        public JitterEasingTokenPreset GetEasePreset(JitterEasingToken easeToken)
        {
            return easingTokenPresets
                .OfType<JitterEasingTokenPreset>()
                .FirstOrDefault(token => token.Name == easeToken.ToString() && token.tokenKey == easeToken);
        }

        public MaterialEasingTokenPreset GetEasePreset(MaterialEasingToken easeToken)
        {
            return easingTokenPresets
                .OfType<MaterialEasingTokenPreset>()
                .FirstOrDefault(token => token.Name == easeToken.ToString() && token.tokenKey == easeToken);
        }

        internal AnimationCurve GetAnimationCurve(string easeName)
        {
            var findIndex = easingTokenPresets.FindIndex(token => token.Name.Equals(easeName));
            if (findIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }
            return easingTokenPresets[findIndex].Curve;
        }
    }

}
