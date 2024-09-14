using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    public class ActionTrackHistory
    {
        public List<object> HistoryList = new();

        public int Count => HistoryList.Count;

        public void Add(object value)
        {
            HistoryList.Add(value);
        }

        public object PopTop()
        {
            var topValue = HistoryList.Count <= 0 ? null : HistoryList[HistoryList.Count - 1];
            HistoryList.RemoveAt(HistoryList.Count - 1);
            return topValue;
        }
    }

    public interface IRecordAction
    {
        object Get(System.Object target);
        void Set(System.Object target, object updateValue);
        object EndPos { get; set; }
        object StartPos { get; set; }
    }

    public static class ActionCenters
    {
        public static void EndRecord(IUniqueBehaviour key)
        {
            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[key];
            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            var clipState = TweenTimeLineDataModel.ClipStateDict[key];
            object curValue = key.Get(target);
            if (curValue == null) return;
            if (!key.EndPos.Equals(curValue))
            {
                clipState.actionTrackHistory.Add(key.EndPos);
            }
            key.EndPos = curValue;
        }

        public static void StartPlay(IUniqueBehaviour key)
        {
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[key];
            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[key];
            if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
            {
                return;
            }
            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];

            if (!clipInfo.Sequence.isAlive)
            {
                clipInfo.CreateTween(key);
            }

            clipInfo.PlayTween(target);
        }

        internal static void StopPlay(IUniqueBehaviour key)
        {
            if (!TweenTimeLineDataModel.ClipInfoDicts.ContainsKey(key))
            {
                return;
            }
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[key];
            if (clipInfo.Sequence.isAlive)
            {
                clipInfo.Sequence.Stop();
            }
        }

        internal static void StartPreview(IUniqueBehaviour key)
        {
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[key];
            if (!clipInfo.Sequence.isAlive)
            {
                clipInfo.CreateTween(key);
            }
        }

        internal static void MoveToEndPos(IUniqueBehaviour key)
        {
            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[key];
            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            key.Set(target, key.EndPos);
        }

        internal static void Reset(IUniqueBehaviour key)
        {
            //  try to reset deleted asset
            if (!TweenTimeLineDataModel.PlayBehaviourTrackDict.ContainsKey(key))
            {
                return;
            }
            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[key];
            if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
            {
                return;
            }
            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            var stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[key];
            key.Set(target, stateInfo.initPos);

            foreach (var item in stateInfo.markerInitPosDict)
            {
                var valueMarker = clipInfo.valueMakers.Find(m => m.InstanceID == item.Key);
                valueMarker.Set(target, item.Value);
            }
        }
    }
}
