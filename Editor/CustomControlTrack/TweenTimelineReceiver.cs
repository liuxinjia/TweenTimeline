using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace Cr7Sund.TweenTimeLine
{
    public class TweenTimelineReceiver : INotificationReceiver
    {
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            if (notification is IValueMaker valueMarker)
            {
                IUniqueBehaviour behaviour = TweenTimeLineDataModel.NotificationReceiverDict.FirstOrDefault(t => t.Value == (this)).Key;
                if (behaviour == null) return;
                var trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
                var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];

                if (behaviour.EndPos.GetType() != valueMarker.Value.GetType())
                {
                    return;
                }
                // behaviour.Set(target, valueMarker.Value);
            }
        }

    }
}
