using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cr7Sund.TweenTimeLine
{
    public partial class TweenActionContainerBuilder
    {
        public List<TweenCollection> animationContainers = new();
        public void Build(TweenActionLibrary container)
        {
            TweenCollection inCollection = CreateInAnimationCollection();
            inCollection.animationCollections.ForEach(animation => animation.collectionCategory = inCollection.category);
            animationContainers.Add(inCollection);

            TweenCollection outCollection = CreateOutAnimationCollection();
            outCollection.animationCollections.ForEach(animation => animation.collectionCategory = outCollection.category);
            animationContainers.Add(outCollection);

            TweenCollection baseCollection = CreateBaseAnimationCollection();
            baseCollection.animationCollections.ForEach(animation => animation.collectionCategory = baseCollection.category);
            animationContainers.Add(baseCollection);

            container.animationContainers.AddRange(animationContainers);
        }

        public static string GetTweenMethodName<T>()
        {
            var typeName = typeof(T).Name;

            // Default return value
            return typeName;
        }


    }
}
