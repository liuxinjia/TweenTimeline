using System;
using System.Collections.Generic;
namespace Cr7Sund.TweenTimeLine
{
    public class ClipBehaviourState
    {
        public ClipBehaviourStateEnum BehaviourState { get; private set; }
        public bool IsSelect;
        public object initPos;
        public bool IsRecordStart;
        public Dictionary<int, object> markerInitPosDict;

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

            // UnityEngine.Debug.Log($"Target:{targetState} , Prev: {BehaviourState}");

            this.BehaviourState = targetState;

            // play Target Action 
            DoAction(behaviour, targetState);
        }

        private void DoAction(IUniqueBehaviour behaviour, ClipBehaviourStateEnum targetState)
        {
            if (behaviour == null) return;

            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            if (trackAsset.mutedInHierarchy)
            {
                return;
            }
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
}
