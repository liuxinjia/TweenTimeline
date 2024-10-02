using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Cr7Sund.TweenTimeLine
{
    public enum ClipBehaviourStateEnum
    {
        Default,
        Preview,
        Playing,
        Recording
    }

    public static class TweenTimeLineDataModel
    {
        public const double MinFrameThreshold = 0.001d;

        public static int ID;
        public static ClipBehaviourState StateInfo = new();
        public static GroupTrack selectGroupTrack;
 
        public static Dictionary<IUniqueBehaviour, ClipInfo> ClipInfoDicts = new();
        public static Dictionary<IUniqueBehaviour, ClipBehaviourState> ClipStateDict = new();
        public static Dictionary<IUniqueBehaviour, TrackAsset> PlayBehaviourTrackDict = new();
        public static Dictionary<IUniqueBehaviour, INotificationReceiver> NotificationReceiverDict = new();
        public static Dictionary<TrackAsset, UnityEngine.Object> TrackObjectDict = new(); //type is  Component
        public static Dictionary<TrackAsset, List<IUniqueBehaviour>> TrackBehaviourDict = new();
        public static Dictionary<UnityEngine.Object, IUniqueBehaviour> ClipAssetBehaviourDict = new();
        public static Dictionary<PlayableAsset, TrackAsset> PlayableAssetTrackDict = new();
        public static List<GroupTrack> groupTracks = new(); // in, out track, Canvas1, Canvas 2
    }
}
