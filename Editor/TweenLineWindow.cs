using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine.Editor
{
    public class TweenLineWindow : EditorWindow
    {
        [MenuItem("Tools/TweenLine")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow<TweenLineWindow>();
            window.Show();
        }

        
    }
}