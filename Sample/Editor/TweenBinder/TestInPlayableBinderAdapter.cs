using UnityEditor;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{

    public class TestInPlayableBinderAdapter : BasePlayableBinderAdapter
    {
        public void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "Canvas_1InTween"))
            {
                GenerateTween.Canvas_1OutTween(this);
            }
        }
    }
}
