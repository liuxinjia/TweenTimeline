using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [MenuEntry("Custom Actions/TweenResetTrackNameTrackAction")]
    public class TweenResetTrackNameTrackAction : TrackAction
    {

        public override ActionValidity Validate(IEnumerable<TrackAsset> tracks)
        {
            return ActionValidity.Valid;
        }

        public override bool Execute(IEnumerable<TrackAsset> tracks)
        {
            TweenTimelineManager.InitTimeline();
            int isIn = TweenTimelineManager.GetPanelTracks(tracks, out var trackHierarchy, out var rootTrack);

            foreach (TrackAsset track in trackHierarchy)
            {
                resetResursively(track);
            }

            return true;

            void resetResursively(TrackAsset track)
            {
                resetName(track);
                foreach (var childTrack in track.GetChildTracks())
                {
                    resetResursively(childTrack);
                }
            }

            void resetName(TrackAsset trackAsset)
            {
                if (!TweenTimeLineDataModel.TrackBehaviourDict.ContainsKey(trackAsset)) return;
                if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset)) return;

                if (trackAsset is not GroupTrack)
                {
                    var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
                    var binComponent = target as Component;
                    var trackRoot = BindUtility.GetAttachRoot(binComponent.transform);
                    string trackName = TweenTimelineManager.GetTrackName(binComponent.transform, trackRoot,
                    binComponent.GetType(), trackAsset.GetType());
                    if (trackName != trackAsset.name)
                    {
                        trackAsset.name = trackName;
                    }
                }
            }
        }


        [TimelineShortcut("TweenResetTrackNameTrackAction", KeyCode.I, ShortcutModifiers.Shift)]
        public static void HandleShortCut(ShortcutArguments args)
        {
            Invoker.InvokeWithSelectedTracks<TweenResetTrackNameTrackAction>();
        }
    }
}
