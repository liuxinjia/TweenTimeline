using UnityEditor.Timeline;
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
            return base.GetMarkerOptions(marker);
        }

    }

}