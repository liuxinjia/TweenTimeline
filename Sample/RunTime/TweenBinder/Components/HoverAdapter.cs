using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    public class HoverAdapter : ComponentBinderAdapter, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string _onPointerEnterTween;
        [SerializeField] private string _onPointerExitTween;
        private Sequence _currentSequence;


        public void OnPointerEnter(PointerEventData eventData)
        {
            _currentSequence.Stop();
            _currentSequence = this.Play(_onPointerEnterTween);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _currentSequence.Stop();
            _currentSequence = this.Play(_onPointerExitTween);
        }
    }
}