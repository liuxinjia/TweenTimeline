using PrimeTween;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [RequireComponent(typeof(CompositeBinder))]
    public class Indicator : MonoBehaviour
    {
        public void OnEnable()
        {
            var binder = GetComponent<CompositeBinder>();
            binder.PlayInTween();


        }

    }
}