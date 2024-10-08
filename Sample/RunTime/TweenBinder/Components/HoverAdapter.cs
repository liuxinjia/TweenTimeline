using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    public class HoverAdapter : ComponentBinderAdapter, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string _onPointerEnterTween;
        [SerializeField] private string _onPointerExitTween;


        public void OnPointerEnter(PointerEventData eventData)
        {

            _currentSequence = this.PlayTween(_onPointerEnterTween);
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            _currentSequence = this.PlayTween(_onPointerExitTween);
        }
    }
}