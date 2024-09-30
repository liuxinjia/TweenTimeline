using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    public class HoverBinder : ComponentBinderAdapter
    {
        [SerializeField] private string _onPointerEnterTween;
        [SerializeField] private string _onPointerExitTween;

        public Sequence PlayOnPointerEnter()
        {
            return this.Play(_onPointerEnterTween);
        }

        public Sequence PlayOnPointerExit()
        {
            return this.Play(_onPointerExitTween);
        }
    }
}
