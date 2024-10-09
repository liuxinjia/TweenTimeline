using System.Collections.Generic;
using Cr7Sund.Timeline.Extension;
using PrimeTween;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [MenuEntry("Custom Actions/TweenRebindTargetTrackAction")]
    public class TweenRebindTargetTrackAction : TrackAction
    {
        private EasingTokenPresetLibrary _easingTokenPresetLibrary;

        public override ActionValidity Validate(IEnumerable<TrackAsset> tracks)
        {
            return ActionValidity.Valid;
        }

        public override bool Execute(IEnumerable<TrackAsset> tracks)
        {
            TweenTimelineManager.InitTimeline();


            var newTrackList = new List<TrackAsset>();
            var newTrackDict = new Dictionary<TrackAsset, TrackAsset>();
            int isIn = TweenTimelineManager.GetPanelTracks(tracks, out var trackHierarchy, out var rootTrack);
            var rootBindTarget = GameObject.Find("RootCanvas");

            foreach (var childTrack in trackHierarchy)
            {
                RebindTrack(childTrack);
            }

            void RebindTrack(TrackAsset trackAsset)
            {
                if (trackAsset is not GroupTrack)
                {
                    var rootTrack = TweenTimelineManager.GetTrackSecondRoot(trackAsset);
                    var bindTarget = GameObject.Find(rootTrack.name);
                    if (bindTarget == null)
                    {
                        bindTarget = rootBindTarget;
                    }
                    var director = TimelineWindowExposer.GetCurDirector();
                    if (!TweenTimeLineDataModel.TrackObjectDict.TryGetValue(trackAsset, out var component)
                    || component == null)
                    {
                        var target = TweenTimelineManager.FindTrackBindTarget(bindTarget.transform, trackAsset);
                        TweenTimelineManager.GetTrackBindInfos(trackAsset, out var trackBindTarget, out var bindType);
                        component = target.transform.GetNotNullComponent(bindType);

                        Assert.IsNotNull(component, $"bind Component is null ,Target： {target} Type: {bindType}");
                    }
                    Assert.IsNotNull(component, $"bind Component is null {trackAsset.name}");
                    TimelineWindowExposer.Bind(director, trackAsset, component);
                }
                foreach (var childTrack in trackAsset.GetChildTracks())
                {
                    RebindTrack(childTrack);
                }
            }


            TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
            return true;
        }


    }
}
