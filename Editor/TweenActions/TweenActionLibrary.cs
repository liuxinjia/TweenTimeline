using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    // [CreateAssetMenu(fileName = "Cr7Sund/TweenTimeLine/AnimationLibrary")]
    public class TweenActionLibrary : ScriptableObject, ITweenActionLibrary
    {
        public List<TweenCollection> animationContainers; // In, Out, Custom

        public void Reset()
        {
            OnReset();
        }

        protected virtual void OnReset()
        {
            var builder = new TweenActionContainerBuilder();
            animationContainers = new();
            builder.Build(this);
        }


        public void AddEffect(TweenActionEffect effect)
        {
            if (TryGetEffect(effect, out var animCollection))
            {
                animCollection.AddEffect(effect);
            }
        }

        private bool TryGetEffect(TweenActionEffect effect, out TweenCollection animCollection)
        {
            animCollection = default;
            if (string.IsNullOrEmpty(effect.collectionCategory))
            {
                Debug.LogError($" please specific the collectionCategory for {effect.label} first ");
                return false;
            }

            int index = animationContainers.FindIndex(item => effect.collectionCategory == item.category);
            if (index < 0)
            {
                animCollection = new TweenCollection("Custom");
                animCollection.animationCollections = new List<TweenActionEffect>();
                animationContainers.Add(animCollection);
            }
            else
            {
                animCollection = animationContainers[index];
            }

            return true;
        }


        public void RemoveEffect(TweenActionEffect tweenEffect)
        {
            if (TryGetEffect(tweenEffect, out var animCollection))
            {
                animCollection.RemoveEffect(tweenEffect);
            }
        }

        public void ApplyToSettings()
        {
            throw new NotImplementedException();
        }

    }
}
