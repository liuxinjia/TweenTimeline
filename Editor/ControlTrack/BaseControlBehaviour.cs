using System;
using Cr7Sund.Timeline.Extension;
using PrimeTween;
using UnityEngine;
using UnityEngine.Playables;
using Object = System.Object;
using Assert = UnityEngine.Assertions.Assert;

namespace Cr7Sund.TweenTimeLine
{
    public interface IUniqueBehaviour : IRecordAction
    {
        int ID { get; }
        string BindTarget { get; set; }
        string BindType { get; set; }
        Easing PrimEase { get; }
        BaseEasingTokenPreset EasePreset { get; set; }
        PrimeTween.Tween CreateTween(object target, double duration, object startValue);
    }

    public abstract class BaseControlBehaviour<TTarget, TValue> : UnityEngine.Playables.PlayableBehaviour, IUniqueBehaviour
    {
        [SerializeReference][SerializeField] private BaseEasingTokenPreset _easePreset;
        [SerializeField] private TValue _endPos;
        [SerializeField] protected TValue _startPos;

        [HideInInspector]
        [SerializeField] private string _bindTargetName;
        [HideInInspector]
        [SerializeField] private string _bindType;
        [SerializeField] private bool _isDynamicPos;

        private readonly int _id = TweenTimeLineDataModel.ID++;

        public int ID => _id;
        public object EndPos
        {
            get => _endPos;
            set
            {
                _endPos = (TValue)value;
            }
        }
        public Easing PrimEase
        {
            get
            {
                if (_easePreset == null)
                {
                    return Ease.Linear;
                }
                return _easePreset.Easing;
            }
        }

        public object StartPos
        {
            get => _startPos;
            set => _startPos = (TValue)value;
        }
        public BaseEasingTokenPreset EasePreset
        {
            get => _easePreset;
            set => _easePreset = value;
        }
        public string BindTarget
        {
            get => _bindTargetName;
            set => _bindTargetName = value; // {{ edit_1 }}
        }
        public string BindType
        {
            get => _bindType;
            set
            {
                _bindType = value;
            } // {{ edit_1 }}
        }

        public bool IsDynamicPos => _isDynamicPos;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            // when drag don't publish event
            // so we will handle the marker event by sequence
            // if (TweenTimeLineDataModel.NotificationReceiverDict.ContainsKey(this))
            // {
            //     info.output.RemoveNotificationReceiver(TweenTimeLineDataModel.NotificationReceiverDict[this]);
            //     info.output.AddNotificationReceiver(TweenTimeLineDataModel.NotificationReceiverDict[this]);
            // }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TweenTimelineManager.EnsureCanPreview();
            if (!TweenTimeLineDataModel.ClipStateDict.ContainsKey(this)) { return; }

            var stateInfo = TweenTimeLineDataModel.ClipStateDict[this];
            if (!stateInfo.IsPreview && !stateInfo.IsPlaying)
            {
                return;
            }
            Assert.IsFalse(stateInfo.IsRecording);

            ExecuteTween(playable, info, playerData);
        }

        public object Get(System.Object target)
        {
            return OnGet((TTarget)target);
        }

        public void Set(System.Object target, Object value)
        {
            OnSet((TTarget)target, (TValue)value);
        }

        public PrimeTween.Tween CreateTween(object target, double duration, object startValue)
        {
            Tween tween = OnCreateTween((TTarget)target, duration, (TValue)startValue);
            return tween;
        }

        private void ExecuteTween(Playable playable, FrameData info, object playerData)
        {
            try
            {
                var curTime = TimelineWindowExposer.GetSequenceTime();
                var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[this];
                // var elapsedTime = curTime - clipInfo.delayTime;
                var elapsedTime = curTime;
                if (elapsedTime <= 0)
                {
                    return;
                }
                var tween = clipInfo.Sequence;
                if (!tween.isAlive)
                {
                    return;
                }
                if (_startPos != null &&
                 _startPos.Equals(EndPos))
                {
                    return;
                }
                tween.elapsedTime = (float)elapsedTime;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to run tween: {ex.Message} \n {ex.StackTrace}");
            }
        }

        protected abstract object OnGet(TTarget target);
        protected abstract void OnSet(TTarget target, TValue intValue);
        protected abstract PrimeTween.Tween OnCreateTween(TTarget target, double duration, TValue startValue);
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is IUniqueBehaviour otherBehaviour)
            {
                return ID.Equals(otherBehaviour.ID);
            }

            return base.Equals(obj);
        }
    }
}
