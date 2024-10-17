namespace Cr7Sund.TweenTimeLine
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(LineRenderer))]
    public class LinePath : MonoBehaviour
    {

        private LineRenderer line;

        public Transform[] points = new Transform[3];

        private int numPoints = 50;
        public Vector3[] positions;

        public float distance;
        [Range(0, 1)]
        public float distanceAmount = 1;

        void Start()
        {

            line = GetComponent<LineRenderer>();
            line.positionCount = numPoints;

            Transform nextPoint;

            if (transform.GetSiblingIndex() == transform.parent.childCount - 1)
            {
                return;
            }

            int nextIndex = transform.GetSiblingIndex() + 1;
            if (nextIndex < transform.parent.childCount)
            {
                positions = new Vector3[50];
                nextPoint = transform.parent.GetChild(nextIndex);
                SetMidPoint(transform, nextPoint);
                DrawQuadraticCurve();
            }
        }

        void SetMidPoint(Transform startPoint, Transform nextPoint)
        {
            distance = Vector3.Distance(startPoint.GetChild(0).position, nextPoint.GetChild(0).position);

            GameObject pivot = new GameObject("pivot");
            GameObject point = new GameObject("point");

            pivot.transform.parent = startPoint.parent.parent;

            point.transform.parent = pivot.transform;
            point.transform.position += (Vector3.forward / 2) + Vector3.forward * distance * distanceAmount;

            pivot.transform.localEulerAngles = new Vector3((startPoint.localEulerAngles.x + nextPoint.localEulerAngles.x) / 2,
                (startPoint.localEulerAngles.y + nextPoint.localEulerAngles.y) / 2, 0);

            points[0] = startPoint.GetChild(0);
            points[1] = point.transform;
            points[2] = nextPoint.GetChild(0);
        }


        private void DrawQuadraticCurve()
        {
            for (int i = 0; i < numPoints; i++)
            {
                float t = i / (float)numPoints;
                positions[i] = CalculateQuadraticLinePathPoint(t, points[0].position, points[1].position, points[2].position);
            }

            line.SetPositions(positions);
        }

        private Vector3 CalculateQuadraticLinePathPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }


    }

}