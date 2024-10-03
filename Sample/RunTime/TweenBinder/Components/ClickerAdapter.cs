using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;
using UnityEngine.UI;

namespace Cr7Sund.TweenTimeLine
{
    [RequireComponent(typeof(Button))]
    public class ClickerAdapter : ComponentBinderAdapter, IPointerClickHandler
    {
        private Sequence _currentSequence;

        [SerializeField] private string _onClickTween;

        public void OnPointerClick(PointerEventData eventData)
        {
            _currentSequence.Stop();
            _currentSequence = this.Play(_onClickTween);
        }
    }
}