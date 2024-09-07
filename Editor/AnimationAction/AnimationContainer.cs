using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cr7Sund.TweenTimeLine;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [CreateAssetMenu(fileName = "Cr7Sund/TweenTimeLine/AnimationContainer")]
    public partial class AnimationContainer : ScriptableObject
    {
        public List<AnimationCollection> animationContainers; // In, Out, Custom

        public AnimationContainer()
        {
            var builder = new AnimationContainerBuilder();
            animationContainers = new();
            builder.Build(this);
        }

    }

}