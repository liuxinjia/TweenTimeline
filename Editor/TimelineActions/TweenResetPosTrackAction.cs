using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [MenuEntry("Custom Actions/TweenResetPosTrackAction")]
    public class TweenResetPosTrackAction : TrackAction
    {
        private EasingTokenPresetLibrary _easingTokenPresetLibrary;

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
                resetPos(track);
                foreach (var childTrack in track.GetChildTracks())
                {
                    resetResursively(childTrack);
                }
            }

            void resetPos(TrackAsset track)
            {
                if (!TweenTimeLineDataModel.TrackBehaviourDict.ContainsKey(track)) return;
                if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(track)) return;

                var behaviours = TweenTimeLineDataModel.TrackBehaviourDict[track];
                var target = TweenTimeLineDataModel.TrackObjectDict[track];
                if (behaviours.Count > 0)
                {
                    var behaviour = behaviours[behaviours.Count - 1];
                    behaviour.Set(target,
                        behaviour.StartPos);

                    TweenTimelineManager.ResetInitPos(behaviour);
                }
            }
        }


        [TimelineShortcut("TweenResetPosTrackAction", KeyCode.R)]
        public static void HandleShortCut(ShortcutArguments args)
        {
            Invoker.InvokeWithSelectedTracks<TweenResetPosTrackAction>();
        }
    }
}
