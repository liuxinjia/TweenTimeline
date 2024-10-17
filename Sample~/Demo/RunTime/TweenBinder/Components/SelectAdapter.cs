using UnityEngine.EventSystems;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    public class SelectAdapter : ComponentBinderAdapter, ISelectHandler, IDeselectHandler
    {
        public string selectTween;
        public string deSelectTween;

        public void OnDeselect(BaseEventData eventData)
        {

            _currentSequence = this.PlayTween(deSelectTween);
        }

        public void OnSelect(BaseEventData eventData)
        {

            _currentSequence = this.PlayTween(selectTween);
        }
    }
}