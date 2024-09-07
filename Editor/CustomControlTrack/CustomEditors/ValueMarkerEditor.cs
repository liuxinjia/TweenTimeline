using System;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    // Editor used by the Timeline window to customize the appearance of a NotesMarker
    [CustomTimelineEditor(typeof(ValueMaker))]
    public class ValueMarkerEditor : MarkerEditor
    {

        public override void OnCreate(IMarker marker, IMarker clonedFrom)
        {
            base.OnCreate(marker, clonedFrom);
        }

        public override MarkerDrawOptions GetMarkerOptions(IMarker marker)
        {
            // Check if marker is not NotesMarker and assign it to notes
            if (marker is not ValueMaker notes)
            {
                return base.GetMarkerOptions(marker); // If not, return with no tooltip override
            }

            if (!TweenTimeLineDataModel.TrackBehaviourDict.ContainsKey(marker.parent))
            {
                return base.GetMarkerOptions(marker);
            }

            var behaviors = TweenTimeLineDataModel.TrackBehaviourDict[marker.parent];
            string errorMsg = string.Empty;
            foreach (var behaviour in behaviors)
            {
                if (behaviour.EndPos.GetType() != notes.Value.GetType())
                {
                    errorMsg = "UnMatch Type";
                }
            }
            return new MarkerDrawOptions { errorText = errorMsg }; // If NotesMarker, replace tooltip with contents of notes.title
        }

    }
}