using System;
using System.Collections.Generic;
using PrimeTween;
namespace Cr7Sund.TweenTimeLine
{
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
}
