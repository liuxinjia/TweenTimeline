using Cr7Sund.TweenTimeLine;
using UnityEditor.ShortcutManagement;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Cr7Sund.TweenTimeline
{
    [MenuEntry("Custom Actions/Sample Timeline Action")]
    public class TweenTimelineAction : TimelineAction
    {
        public override ActionValidity Validate(ActionContext context)
        {
            return ActionValidity.Valid;
        }

        public override bool Execute(ActionContext context)
        {
            TweenActionEditorWindow.ShowWindow();
            return true;
        }

        [TimelineShortcut("TweenTimelineAction", KeyCode.T, ShortcutModifiers.Control)]
        public static void HandleShortCut(ShortcutArguments args)
        {
            Invoker.InvokeWithSelected<TweenTimelineAction>();
        }
    }
}
