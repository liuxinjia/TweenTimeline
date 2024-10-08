using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;
using UnityEngine.UI;

namespace Cr7Sund.TweenTimeLine
{
    [RequireComponent(typeof(Button))]
    public class ClickerAdapter : ComponentBinderAdapter, IPointerClickHandler
    {

        [SerializeField] private string _onClickTween;

        public void OnPointerClick(PointerEventData eventData)
        {

            _currentSequence = this.PlayTween(_onClickTween);
        }
    }
}