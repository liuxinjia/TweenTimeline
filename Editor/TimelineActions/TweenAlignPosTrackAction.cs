using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [MenuEntry("Custom Actions/TweenAlignPosTrackAction")]
    public class TweenAlignPosTrackAction : TrackAction
    {
        public override ActionValidity Validate(IEnumerable<TrackAsset> tracks)
        {
            return ActionValidity.Valid;
        }

        public override bool Execute(IEnumerable<TrackAsset> tracks)
        {
            TweenTimelineManager.InitTimeline();
            int isIn = TweenTimelineManager.GetPanelTracks(tracks, out var trackHierarchy, out var rootTrack);
            double startOffset = 0;

            Queue<TrackAsset> trackQueue = new Queue<TrackAsset>();
            foreach (TrackAsset track in trackHierarchy)
            {
                trackQueue.Enqueue(track);
            }

            while (trackQueue.Count > 0)
            {
                TrackAsset currentTrack = trackQueue.Dequeue();
                iteratePos(currentTrack);
                foreach (var childTrack in currentTrack.GetChildTracks())
                {
                    trackQueue.Enqueue(childTrack);
                }
            }

            void iteratePos(TrackAsset track)
            {
                if (track == null)
                {
                    return;
                }

                if (track.parent.name == TweenTimelineDefine.InDefine
                 || track.parent.name == TweenTimelineDefine.OutDefine)
                {
                    startOffset = -1;//reset
                }
                foreach (var clipAsset in track.GetClips())
                {
                    if (startOffset < 0)
                    {
                        startOffset = clipAsset.start;
                    }
                    if (startOffset > 0)
                        clipAsset.start -= startOffset;
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return true;
        }


        [TimelineShortcut("TweenAlignPosTrackAction", KeyCode.A, ShortcutModifiers.Shift)]
        public static void HandleShortCut(ShortcutArguments args)
        {
            Invoker.InvokeWithSelectedTracks<TweenAlignPosTrackAction>();
        }
    }
}
