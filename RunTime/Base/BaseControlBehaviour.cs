using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Playables;

namespace Cr7Sund.TweenTimeLine
{
    public abstract class BaseControlBehaviour<T> : PlayableBehaviour where T : class
    {
        public Ease Ease;
        protected T _target;
        private bool _init;
        private System.Reflection.MethodInfo _getDurationMethod =
                typeof(PlayableHandle).GetMethod("GetDuration", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Init(playable, info, playerData);
        }

        private void Init(Playable playable, FrameData info, object playerData)
        {
            // it's another reason don't recommend use timeline directly
            // since it iterate it every frame though it run if almost every time
            if (!IsValidTarget())
            {
                _init = true;
                _target = playerData as T;
                StartTween(playable, info);
            }
        }

        private bool IsValidTarget()
        {
            return _init;
        }

        private void StartTween(Playable playable, FrameData info)
        {
            var duration = Convert.ToSingle(_getDurationMethod.Invoke(playable.GetHandle(), null));
            ExecuteTween(duration);
            try
            {
                ExecuteTween(duration);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to run tween: {ex.Message}");
            }
        }

        protected abstract void ExecuteTween(float duration);

    }
}