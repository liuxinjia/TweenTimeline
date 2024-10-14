using System;
using System.Collections.Generic;
using System.Linq;
using PrimeTween;
using UnityEngine;
using UnityEngine.Assertions;

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
            if (Enum.TryParse<PrimeTween.Ease>(easeName, out var builtInEase))
            {
                resultEase = builtInEase;
                return true;
            }
            else
            {
                Assert.IsNotNull(easingTokenPresets);

                var findIndex = -1;
                findIndex = easingTokenPresets.FindIndex(token => token.Name.Equals(easeName));
                if (findIndex >= 0)
                {
                    resultEase = easingTokenPresets[findIndex].Easing;
                    return true;
                }
                else
                {
                    throw new Exception($"Invalid Ease Name {easeName}");
                }
            }
        }


        public void AddPresets(IEnumerable<BaseEasingTokenPreset> easingTokenPresetsToAdd)
        {
            foreach (var preset in easingTokenPresetsToAdd)
            {
                AddPreset(preset); // Reuse AddPreset to handle duplicates
            }
        }

        public void AddPreset(BaseEasingTokenPreset easingTokenPreset)
        {
            var findIndex = easingTokenPresets
               .FindIndex(token =>
                token.Name == easingTokenPreset.Name
                && token.GetType() == easingTokenPreset.GetType()
                );

            if (findIndex >= 0)
            {
                // Debug.LogWarning($"A ease preset with the name '{easingTokenPreset.Name}' already exists.");
                easingTokenPresets[findIndex] = easingTokenPreset;
            }
            else
            {
                easingTokenPresets.Add(easingTokenPreset);
            }
        }

        public void RemovePreset(BaseEasingTokenPreset easingTokenPreset)
        {
            var findIndex = easingTokenPresets
               .FindIndex(token =>
                token.Name == easingTokenPreset.Name
                && token.GetType() == easingTokenPreset.GetType()
                );


            if (findIndex >= 0)
            {
                // Debug.LogWarning($"A ease preset with the name '{easingTokenPreset.Name}' already exists.");
                easingTokenPresets.RemoveAt(findIndex);
            }

        }

#if UNITY_EDITOR

        public JitterEasingTokenPreset GetEasePreset(JitterEasingToken easeToken)
        {
            var targetEase = easingTokenPresets
                .OfType<JitterEasingTokenPreset>()
                .First(token => token.tokenKey == easeToken);


            var easeTokenPreset = Activator.CreateInstance<JitterEasingTokenPreset>();
            easeTokenPreset.tokenKey = easeToken;
            easeTokenPreset.animationCurve = targetEase.animationCurve;
            return easeTokenPreset;
        }

        public MaterialEasingTokenPreset GetEasePreset(MaterialEasingToken easeToken)
        {
            var targetEase = easingTokenPresets
                .OfType<MaterialEasingTokenPreset>()
                .First(token => token.tokenKey == easeToken);

            var easeTokenPreset = Activator.CreateInstance<MaterialEasingTokenPreset>();
            easeTokenPreset.tokenKey = easeToken;
            easeTokenPreset.animationCurve = targetEase.animationCurve;
            return easeTokenPreset;
        }

        public EaseTokenPreset GetEasePreset(PrimeTween.Ease ease)
        {
            EaseTokenPreset easeTokenPreset = Activator.CreateInstance<EaseTokenPreset>();
            easeTokenPreset.tokenKey = ease;
            return easeTokenPreset;
        }

        public CustomCurveEasingTokenPreset GetEasePreset(string curveName)
        {
            var targetEase = easingTokenPresets
                .OfType<CustomCurveEasingTokenPreset>()
                .First(token => token.Name == curveName);

            var easeTokenPreset = Activator.CreateInstance<CustomCurveEasingTokenPreset>();
            easeTokenPreset.animationCurve = targetEase.animationCurve;
            easeTokenPreset.tokenKey = curveName;
            return easeTokenPreset;
        }

        public int FindEasePreset(string curveName)
        {
            return easingTokenPresets
                .OfType<CustomCurveEasingTokenPreset>()
                .ToList()
                .FindIndex(token => token.Name == curveName);
        }
#endif


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
