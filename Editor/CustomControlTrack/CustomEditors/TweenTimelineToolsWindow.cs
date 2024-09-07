using System;
using Cr7Sund.Timeline.Extension;
using UnityEditor;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine.Editor
{
    public class TweenTimelineToolsWindow : EditorWindow
    {


        [MenuItem("Tools/FileId")]
        public static void ShowFileID()
        {
            var selectObj = Selection.activeObject;
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(selectObj.GetInstanceID(), out var guid, out var localID);
            EditorGUIUtility.systemCopyBuffer = $"{guid}";
        }

    }
}
