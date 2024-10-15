using System.Collections.Generic;
using Cr7Sund.TweenTimeLine;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeline
{
    [MenuEntry("Custom Actions/ResetCurveAction")]
    public class ResetCurveClipAction : ClipAction
    {

        public override bool Execute(IEnumerable<TimelineClip> clips)
        {
            foreach (var clip in clips)
            {
                ValidCurve(clip);
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

        private static void ValidCurve(TimelineClip timelineClip)
        {
            var uniqueBehaviour = TweenTimelineManager.GetBehaviourByTimelineClip(timelineClip);
            object endPos = uniqueBehaviour.EndPos;
            object startPos = uniqueBehaviour.StartPos;
            float duration = (float)(timelineClip.duration);
            var animClip = timelineClip.curves;

            EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(animClip);

            if (endPos is Vector3 endV3 && startPos is Vector3 startV3)
            {
                ValidClip(timelineClip, bindings[0], duration, startV3.x, endV3.x);
                // ValidClip(timelineClip, bindings[1], duration, startV3.y, endV3.y);
                // ValidClip(timelineClip, bindings[2], duration, startV3.z, endV3.z);
            }
            else
            {
                float endValue = (float)endPos;
                float startValue = (float)startPos;
                ValidClip(timelineClip, bindings[0], duration, startValue, endValue);
            }
        }

        private static void ValidClip(TimelineClip timelineClip, EditorCurveBinding binding, float duration,
        float startValue, float endValue)
        {
            var animClip = timelineClip.curves;
            AnimationCurve curve = AnimationUtility.GetEditorCurve(animClip, binding);
            Assert.IsNotNull(curve);

            Keyframe startFrame;
            Keyframe endFrame;
            if (curve.length > 0)
            {
                startFrame = curve[0];
                startFrame.time = 0;
                startFrame.value = startValue;
                curve.MoveKey(0, startFrame);
            }
            else
            {
                curve.AddKey(0, startValue);
            }
            if (curve.length > 1)
            {
                endFrame = curve[curve.length - 1];
                endFrame.time = duration;
                endFrame.value = endValue;
                curve.MoveKey(curve.length - 1, endFrame); // updateKeyFrame
            }
            else
            {
                curve.AddKey(duration, endValue);
            }

            AnimationUtility.SetEditorCurve(animClip, binding, curve);
            EditorUtility.SetDirty(animClip);

        }

    }
}
