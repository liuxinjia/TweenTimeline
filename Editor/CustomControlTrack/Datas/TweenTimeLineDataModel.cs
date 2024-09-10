using System;
using System.Collections.Generic;
using Cr7Sund.TweenTimeLine.Editor;
using PrimeTween;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
namespace Cr7Sund.TweenTimeLine
{
    public enum ClipBehaviourStateEnum
    {
        Default,
        Preview,
        Playing,
        Recording
    }
    public class ClipBehaviourState
    {
        public ClipBehaviourStateEnum BehaviourState { get; private set; }
        public bool IsSelect;
        public object initPos;
        public Dictionary<int,object> markerInitPosDict;

        public ActionTrackHistory actionTrackHistory = new();
        public Action<IUniqueBehaviour, ClipBehaviourStateEnum> RecordAction { get; internal set; }
        public Action<IUniqueBehaviour, ClipBehaviourStateEnum> PreViewAction { get; internal set; }
        public Action<IUniqueBehaviour, ClipBehaviourStateEnum> PlayAction { get; internal set; }
        public Action ViewAction = () => { };
        public bool IsPreview => BehaviourState == ClipBehaviourStateEnum.Preview;
        public bool IsPlaying => BehaviourState == ClipBehaviourStateEnum.Playing;
        public bool IsRecording => BehaviourState == ClipBehaviourStateEnum.Recording;


        public void ToggleState(IUniqueBehaviour behaviour, ClipBehaviourStateEnum targetState)
        {
            if (BehaviourState == targetState)
            {
                targetState = ClipBehaviourStateEnum.Default;
            }

            ChangeState(behaviour, targetState);
        }

        public void ChangeState(IUniqueBehaviour behaviour, ClipBehaviourStateEnum targetState)
        {
            if (BehaviourState == targetState)
            {
                return;
            }
            if (targetState == ClipBehaviourStateEnum.Recording && !IsSelect)
            {
                // Debug.LogError("Please select the clip first, which means you should drag the timeline to the clip pos ");
                return;
            }

            // stop Other Action First
            DoAction(behaviour, targetState);

            // Debug.Log(targetState + " " + BehaviourState);

            this.BehaviourState = targetState;

            // play Target Action 
            DoAction(behaviour, targetState);
        }

        private void DoAction(IUniqueBehaviour behaviour, ClipBehaviourStateEnum targetState)
        {
            if (behaviour == null) return;

            switch (this.BehaviourState)
            {
                case ClipBehaviourStateEnum.Default:
                    ViewAction?.Invoke();
                    break;
                case ClipBehaviourStateEnum.Playing:
                    PlayAction?.Invoke(behaviour, targetState);
                    ViewAction?.Invoke();
                    break;
                case ClipBehaviourStateEnum.Preview:
                    PreViewAction?.Invoke(behaviour, targetState);
                    ViewAction?.Invoke();
                    break;
                case ClipBehaviourStateEnum.Recording:
                    RecordAction?.Invoke(behaviour, targetState);
                    ViewAction?.Invoke();
                    break;
                default: break;
            }
        }
    }

    public class MarkInfo
    {
        public object UpdateValue;
        public string FieldName;
        public double Time;
        public int InstanceID;

        public MarkInfo(IValueMaker field)
        {
            UpdateValue = field.Value;
            FieldName = field.FieldName;
            Time = field.time;
            InstanceID = field.instanceID;
        }

        public void Set(object target, object updateValue)
        {
            var fieldInfo = target.GetType().GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo == null)
            {
                var propInfo = target.GetType().GetProperty(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                propInfo.SetMethod.Invoke(target, new object[] { updateValue });
            }
            else
            {
                fieldInfo.SetValue(target, updateValue);
            }
        }

        public object Get(object target)
        {
            var fieldInfo = target.GetType().GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo == null)
            {
                var propInfo = target.GetType().GetProperty(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                return propInfo.GetMethod.Invoke(target, null);
            }
            else
            {
                return fieldInfo.GetValue(target);
            }
        }

    }

    public class ClipInfo : IDisposable
    {
        public double start;
        public double duration;
        // public AnimationCurve curve;
        public List<MarkInfo> valueMakers;
        private PrimeTween.Sequence _sequence;


        public PrimeTween.Sequence Sequence { get => _sequence; }

        public void Dispose()
        {
            if (Sequence.isAlive)
            {
                Sequence.Stop();
            }
        }

        public void CreateTween(IUniqueBehaviour behaviour)
        {
            if (Sequence.isAlive)
            {
                Sequence.Stop();
            }

            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
            {
                return;
            }

            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
            var startValue = behaviour.StartPos;
            var newTween = behaviour.CreateTween(target, duration, startValue);
            // newTween.isPaused = true;
            _sequence = Sequence.Create().Chain(newTween);
            _sequence.isPaused = true;
            foreach (var valueMarker in clipInfo.valueMakers)
            {
                // another gc
                float time = (float)valueMarker.Time;
                _sequence.InsertCallback(time, valueMarker, (marker) => marker.Set(target, marker.UpdateValue));
            }
        }

        public void PlayTween(UnityEngine.Object target)
        {
            if (TweenTimelineManager.isPlay)
            {
                return;
            }
            EditorTweenCenter.RegisterSequence(Sequence, target, (float)duration);
        }
    }

    public static class TweenTimeLineDataModel
    {
        public const double MinFrameThreshold = 0.001d;

        public static int ID;
        public static ClipBehaviourState StateInfo = new();

        public static Dictionary<IUniqueBehaviour, ClipInfo> ClipInfoDicts = new();
        public static Dictionary<IUniqueBehaviour, ClipBehaviourState> ClipStateDict = new();
        public static Dictionary<IUniqueBehaviour, TrackAsset> PlayBehaviourTrackDict = new();
        public static Dictionary<IUniqueBehaviour, INotificationReceiver> NotificationReceiverDict = new();
        public static Dictionary<TrackAsset, UnityEngine.Object> TrackObjectDict = new();
        public static Dictionary<TrackAsset, List<IUniqueBehaviour>> TrackBehaviourDict = new();
        public static Dictionary<UnityEngine.Object, IUniqueBehaviour> ClipAssetBehaviourDict = new();
        public static Dictionary<PlayableAsset, TrackAsset> PlayableAssetTrackDict = new();

        public static List<GroupTrack> groupTracks = new(); // in, out track, Canvas1, Canvas 2
    }
}
