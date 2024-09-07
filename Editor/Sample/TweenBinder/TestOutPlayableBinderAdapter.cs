using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{
    public class TestOutPlayableBinderAdapter : BasePlayableBinderAdapter
    {
        public void OnGUI()
        {

            if (GUI.Button(new Rect(210, 10, 150, 100), "Canvas_2OutTween"))
            {
                // GenerateTween.Canvas_2OutTween(this);
            }
        }
    }
}
