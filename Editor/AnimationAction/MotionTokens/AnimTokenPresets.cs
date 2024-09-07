using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [CreateAssetMenu(menuName = "Cr7Sund/TweenTimeLine/AnimTokenPresets", fileName = "AnimTokenPresets")]
    public class AnimTokenPresets : ScriptableObject
    {
        [System.Serializable]
        public struct AnimationSettingData
        {
            [Tooltip("The type of animation setting.")]
            public TimeEasePairs settingType;

            [Tooltip("The duration of the animation.")]
            public DurationToken duration;

            [Tooltip("The easing function applied to the animation.")]
            public MaterialEasingToken easing;

            // Additional property for tooltip
            public string tooltip;

            public AnimationSettingData(TimeEasePairs setting, MaterialEasingToken easing, DurationToken duration, string tooltip = "")
            {
                this.settingType = setting;
                this.easing = easing;
                this.duration = duration;
                this.tooltip = tooltip;
            }
        }

        public AnimationSettingData[] settings;

        public AnimTokenPresets()
        {
            settings = new AnimationSettingData[]
                      {
                    new AnimationSettingData(TimeEasePairs.Emphasized, MaterialEasingToken.Emphasized, DurationToken.Long2, "Begin and end on screen, 500ms"),
                    new AnimationSettingData(TimeEasePairs.EmphasizedDecelerate, MaterialEasingToken.EmphasizedDecelerate, DurationToken.Long1, "Enter the screen, 400ms"),
                    new AnimationSettingData(TimeEasePairs.EmphasizedAccelerate, MaterialEasingToken.EmphasizedAccelerate, DurationToken.Short4, "Exit the screen, 200ms"),
                    new AnimationSettingData(TimeEasePairs.Standard, MaterialEasingToken.Standard, DurationToken.Medium2, "Begin and end on screen, 300ms"),
                    new AnimationSettingData(TimeEasePairs.StandardDecelerate, MaterialEasingToken.StandardDecelerate, DurationToken.Medium1, "Enter the screen, 250ms"),
                    new AnimationSettingData(TimeEasePairs.StandardAccelerate, MaterialEasingToken.StandardAccelerate, DurationToken.Short3, "Exit the screen, 200ms"),
                      };
        }

        public float ConvertDuration(DurationToken durationToken)
        {
            return (float)durationToken / 1000f; // assuming DurationToken represents milliseconds
        }

        public AnimationSettingData GetSettings(TimeEasePairs animType)
        {
            return settings.First((setting) => setting.settingType == animType);
        }

        public TimeEasePairs GetAnimationSettingType(DurationToken durationToken, MaterialEasingToken easingToken)
        {
            // Use FirstOrDefault to get the matching item
            AnimationSettingData item = settings.FirstOrDefault(setting => setting.duration == durationToken && setting.easing == easingToken);

            return item.settingType;
        }
    }

    public enum TimeEasePairs
    {
        Custom,
        Emphasized,
        EmphasizedDecelerate,
        EmphasizedAccelerate,
        Standard,
        StandardDecelerate,
        StandardAccelerate,
    }

}
