using System;
using Cr7Sund.Timeline.Extension;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    public class ControlTrackWindow : EditorWindow
    {
        private SerializedObject _serializedObject;
        private IUniqueBehaviour _behaviour;

        public static void Open(SerializedObject serializedObject, IUniqueBehaviour behaviour)
        {
            Type windowType = Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor.dll");
            EditorWindow hierarchyWindow = EditorWindow.GetWindow(windowType);

            var window = EditorWindow.GetWindow<ControlTrackWindow>(
            //     desiredDockNextTo: new[]
            // {
            //     System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor.dll")
            // }
            );
            window._behaviour = behaviour;
            window._serializedObject = serializedObject;
            window.minSize = new Vector2(338, 328);
            window.maxSize = new Vector2(368, 328);

            if (hierarchyWindow != null)
                window.position = hierarchyWindow.position;
            window.UpdateUI();
        }

        private void UpdateUI()
        {
            rootVisualElement.Clear();

            var trackUIBuilder = new ControlTrackUIBuilder();
            VisualElement container = trackUIBuilder.CreateContainer(_serializedObject, _behaviour);

            var btn = new Button(() =>
            {
                this.Close();
            });
            btn.text = "Back To TimeLine";
            rootVisualElement.Add(container);
            rootVisualElement.Add(btn);
        }


        private void OnDestroy()
        {
            Selection.activeObject = TweenTimeLineDataModel.SelectDirector;
            ForceEndRecord();
        }

        private void ForceEndRecord()
        {
            var stateInfo = TweenTimeLineDataModel.ClipStateDict[_behaviour];
            stateInfo.IsRecordStart = true;
            TweenTimelineManager.ResetDefaultClip(_behaviour);
            stateInfo.IsRecordStart = false;
            TweenTimelineManager.ResetDefaultClip(_behaviour);
        }
    }
}
