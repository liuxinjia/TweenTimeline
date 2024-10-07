using System;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Cr7Sund.TweenTimeLine
{
    public class ClipInfo : IDisposable
    {
        public double delayTime;
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
            if (_sequence.isAlive)
            {
                _sequence.releaseTweens();
                _sequence.Stop();
            }

            var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
            {
                return;
            }

            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
            var startValue = behaviour.StartPos;

            if (target == null)
            {
                return;
            }
            var newTween = behaviour.CreateTween(target, duration, startValue);
            // newTween.isPaused = true;
            float tweenDelay = TweenTimeLineDataModel.IsPlaySingleTween ? 0 :
                 (float)delayTime;

            if (tweenDelay <= 0)
            {
                _sequence = Sequence.Create().Chain(newTween);
            }
            else
            {
                _sequence = Sequence.Create().ChainDelay(tweenDelay).Chain(newTween);
            }
            _sequence.isPaused = true;
            foreach (var valueMarker in clipInfo.valueMakers)
            {
                // another gc
                float markerTime = TweenTimeLineDataModel.IsPlaySingleTween ? (float)valueMarker.Time - (float)delayTime :
                         (float)valueMarker.Time;
                _sequence.InsertCallback(markerTime, valueMarker, (marker) => marker.Set(target, marker.UpdateValue));
            }
        }

        public void PlayTween(UnityEngine.Object target)
        {
            if (TweenTimelineManager.isPlay)
            {
                return;
            }

            EditorTweenCenter.RegisterSequence(Sequence, target, (float)Sequence.durationTotal);
        }
    }
}
