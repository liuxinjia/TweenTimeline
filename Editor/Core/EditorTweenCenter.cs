using System;
using System.Collections.Generic;
using PrimeTween;
using UnityEditor;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{
    public struct EditorTimerAction
    {
        public float duration;
        public float deltaTime;
        public float startTime;
        public Action<float> action;
        public UnityEngine.Object target; // to refresh
        public string id;

        public EditorTimerAction(UnityEngine.Object tweenTarget, float Duration, Action<float> timeAction)
        {
            target = tweenTarget;
            startTime = (float)Time.realtimeSinceStartup;
            duration = Duration;
            action = timeAction;
            id = Guid.NewGuid().ToString();
            deltaTime = 0;
        }
    }


    public static class EditorTweenCenter
    {

        public static List<EditorTimerAction> _editorUpdateActionList = new();
        public static List<string> _deleteUpdateActionList = new();
        private static double lastUpdateTime = 0.0;
        private static double targetFrameTime = 1.0 / 60.0;


        [InitializeOnLoadMethod]
        static void OnInitEditorTweenCenter()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            double currentTime = EditorApplication.timeSinceStartup;

            if (currentTime - lastUpdateTime >= targetFrameTime)
            {
                UpdateEditorTimer();
                lastUpdateTime = currentTime;
            }
        }

        public static string RegisterSequence(Sequence sequence, UnityEngine.Object target, float duration)
        {
            UnityEngine.Assertions.Assert.IsTrue(duration > 0);
            // preview action will update and create newOne
            // CreateTween(behaviour); 
            Action<float> timeAction = (elapsedTime) =>
            {
                if (!sequence.isAlive)
                {
                    return;
                }
                if (elapsedTime > 0)
                    sequence.elapsedTime = elapsedTime;
            };

            return RegisterEditorTimer(target, (float)duration, timeAction);
        }

        public static string RegisterTween(Tween tween, UnityEngine.Object target, float duration)
        {
            UnityEngine.Assertions.Assert.IsTrue(duration > 0);
            // preview action will update and create newOne
            // CreateTween(behaviour); 
            Action<float> timeAction = (elapsedTime) =>
            {
                if (!tween.isAlive)
                {
                    return;
                }
                if (elapsedTime > 0)
                    tween.elapsedTime = elapsedTime;
            };

            return RegisterEditorTimer(target, (float)duration, timeAction);
        }

        public static string RegisterDelayCallback(UnityEngine.Object target, float duration, Action<UnityEngine.Object> action)
        {
            var sequence = Sequence.Create().InsertCallback(duration, target, t => action?.Invoke(t));
            return RegisterSequence(sequence, target, duration);
        }
        private static string RegisterEditorTimer(UnityEngine.Object target, float duration, Action<float> action)
        {
            var timer = new EditorTimerAction(target, duration, action);
            _editorUpdateActionList.Add(timer);

            return timer.id;

        }

        public static void UnRegisterEditorTimer(string id)
        {
            _deleteUpdateActionList.Add(id);
        }

        private static void UpdateEditorTimer()
        {
            _editorUpdateActionList.RemoveAll(t => _deleteUpdateActionList.Exists(d => d == t.id));

            _deleteUpdateActionList.Clear();

            for (int i = _editorUpdateActionList.Count - 1; i >= 0; i--)
            {
                UnityEngine.Object target = _editorUpdateActionList[i].target;
                if (target == null)
                {
                    _deleteUpdateActionList.Add(_editorUpdateActionList[i].id);
                    continue;
                }

                float elapsedTime = Time.realtimeSinceStartup - _editorUpdateActionList[i].startTime;
                _editorUpdateActionList[i].action?.Invoke(elapsedTime);
                // Force visual refresh of UI objects
                // (a simple SceneView.RepaintAll won't work with UI elements)
                EditorUtility.SetDirty(target);

                if (_editorUpdateActionList[i].duration > 0 && elapsedTime > _editorUpdateActionList[i].duration)
                {
                    _deleteUpdateActionList.Add(_editorUpdateActionList[i].id);
                }
            }
        }

    }
}
