using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    public class ClickerAdapter : ComponentBinderAdapter, IPointerClickHandler
    {
        private Sequence _currentSequence;

        [SerializeField] private string _onPointerClickTween;


        public void OnPointerClick(PointerEventData eventData)
        {
            _currentSequence.Stop();
            _currentSequence = this.Play(_onPointerClickTween);
        }
    }
}