using System.Collections.Generic;
using Cr7Sund.TweenTimeLine;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeline
{
    [MenuEntry("Custom Actions/ApplyCurveAction")]
    public class ApplyCurveClipAction : ClipAction
    {

        public override bool Execute(IEnumerable<TimelineClip> clips)
        {
            foreach (var clip in clips)
            {
                ApplyCurve(clip);
            }

            return true;
        }

        public override ActionValidity Validate(IEnumerable<TimelineClip> clips)
        {
            foreach (var clip in clips)
            {
                if (clip.curves != null)
                {
                    return ActionValidity.Valid;
                }
            }
            return ActionValidity.Invalid;
        }

        private static void ApplyCurve(TimelineClip timelineClip)
        {
            var uniqueBehaviour = TweenTimelineManager.GetBehaviourByTimelineClip(timelineClip);
            var animClip = timelineClip.curves;

            string newCurveName = "NewCurve";
            var customCurveEasingTokenPresets = AnimationClipConverter.CreateCurvePresets(animClip, newCurveName);
            var customCurveEasingTokenPreset = customCurveEasingTokenPresets[0];

            // var easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);
            // AnimationClipConverter.AddPresets(easingTokenPresetLibrary, easingTokenPresetsToAdd);
            EasingTokenPresetLibraryEditor.UpdatePresetLibrary(customCurveEasingTokenPreset);
            uniqueBehaviour.EasePreset = customCurveEasingTokenPreset;
        }



    }
}
