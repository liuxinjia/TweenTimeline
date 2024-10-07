using UnityEditor;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    public class ControlTrackWindow : EditorWindow
    {
        private SerializedObject _serializedObject;
        private IUniqueBehaviour _behaviour;

        public static void Open(SerializedObject serializedObject, IUniqueBehaviour behaviour)
        {
            var window = EditorWindow.GetWindow<ControlTrackWindow>(desiredDockNextTo: new[]
            {
                System.Type.GetType("UnityEditor.GameView,UnityEditor.dll")
            });
            window._behaviour = behaviour;
            window._serializedObject = serializedObject;
            window.UpdateUI();
        }

        private void UpdateUI()
        {
            rootVisualElement.Clear();

            var trackUIBuilder = new ControlTrackUIBuilder();
            VisualElement container = trackUIBuilder.CreateContainer(_serializedObject, _behaviour);

            rootVisualElement.Add(container);
        }
    }
}
