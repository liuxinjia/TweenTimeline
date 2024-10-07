using UnityEngine.EventSystems;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    public class SelectAdapter : ComponentBinderAdapter, ISelectHandler, IDeselectHandler
    {
        private Sequence _currentSequence;

        public string selectTween;
        public string deSelectTween;

        public void OnDeselect(BaseEventData eventData)
        {
            _currentSequence.Stop();
            _currentSequence = this.Play(deSelectTween);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _currentSequence.Stop();
            _currentSequence = this.Play(selectTween);
        }
    }
}