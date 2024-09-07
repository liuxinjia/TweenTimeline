using System.Collections.Generic;
using burningmime.curves;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public static class JitterBezierControlPoints
    {
        public static readonly Dictionary<JitterEasingToken, Vector2[]> ControlPoints = new Dictionary<JitterEasingToken, Vector2[]>
        {
            { JitterEasingToken.Smooth, new Vector2[]
                {
                    new Vector2(0.42f, 0.0f), // P1
                    new Vector2(0.58f, 1.0f), // P2
                    new Vector2(0.42f, 0.0f), // P3
                    new Vector2(0.58f, 1.0f)  // P4
                }
            },
            { JitterEasingToken.Natural, new Vector2[]
                {
                    new Vector2(0.5f, 0.0f),  // P1
                    new Vector2(0.5f, 1.0f),  // P2
                    new Vector2(0.5f, 0.0f),  // P3
                    new Vector2(0.5f, 1.0f)   // P4
                }
            },
            { JitterEasingToken.SlowDown, new Vector2[]
                {
                    new Vector2(0.0f, 0.0f),  // P1
                    new Vector2(0.3f, 0.7f),  // P2
                    new Vector2(0.7f, 1.0f),  // P3
                    new Vector2(1.0f, 1.0f)   // P4
                }
            },
            { JitterEasingToken.Accelerate, new Vector2[]
                {
                    new Vector2(0.0f, 0.0f),  // P1
                    new Vector2(0.2f, 0.0f),  // P2
                    new Vector2(0.8f, 1.0f),  // P3
                    new Vector2(1.0f, 1.0f)   // P4
                }
            },
            { JitterEasingToken.Elastic, new Vector2[]
                {
                    new Vector2(0.5f, 0.0f),  // P1
                    new Vector2(0.5f, 1.0f),  // P2
                    new Vector2(0.8f, 0.6f),  // P3
                    new Vector2(1.0f, 1.0f)   // P4
                }
            },
            { JitterEasingToken.Bounce, new Vector2[]
                {
                new Vector2(0.0f, 0.0f), // P1: Start point
                new Vector2(0.2f, 0.8f), // P2: First bounce peak
                new Vector2(0.5f, 0.4f), // P3: Second bounce peak
                new Vector2(1.0f, 1.0f)  // P4: End point
                }
            },
            { JitterEasingToken.Overshoot, new Vector2[]
                {
                new Vector2(0.0f, 0.0f), // P1: Start point
                new Vector2(0.2f, 1.2f), // P2: Overshoot peak
                new Vector2(0.8f, 0.8f), // P3: Settling back
                new Vector2(1.0f, 1.0f)  // P4: End point
                }
            },
            { JitterEasingToken.Impulse, new Vector2[]
                {
                    new Vector2(0.0f, 0.0f),  // P1
                    new Vector2(0.5f, 0.1f),  // P2
                    new Vector2(0.5f, 0.9f),  // P3
                    new Vector2(1.0f, 1.0f)   // P4
                }
            },
            { JitterEasingToken.Swing, new Vector2[]
                {
                    new Vector2(0.0f, 0.0f),  // P1
                    new Vector2(0.5f, 0.5f),  // P2
                    new Vector2(0.5f, 0.5f),  // P3
                    new Vector2(1.0f, 1.0f)   // P4
                }
            }
        };

        // Dictionary to hold CubicBezier instances
        public static readonly Dictionary<JitterEasingToken, CubicBezier> ControlCubics = new Dictionary<JitterEasingToken, CubicBezier>();

        static JitterBezierControlPoints()
        {
            // Initialize ControlCubics dictionary with CubicBezier instances
            foreach (var entry in ControlPoints)
            {
                ControlCubics[entry.Key] = new CubicBezier(entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[3]);
            }
        }
    }
}
