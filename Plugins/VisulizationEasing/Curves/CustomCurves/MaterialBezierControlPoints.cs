using System.Collections.Generic;
using burningmime.curves;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{
    public class MaterialBezierControlPoints
    {
        public static readonly Dictionary<MaterialEasingToken, Vector2[]> ControlPoints = new Dictionary<MaterialEasingToken, Vector2[]>
    {
        { MaterialEasingToken.Emphasized, new Vector2[]
            {
                new Vector2(0, 0),       // P0
                new Vector2(0.2f, 0.0f), // P1
                new Vector2(0.0f, 1.0f), // P2
                new Vector2(1, 1)        // P3
            }
        },
        { MaterialEasingToken.EmphasizedDecelerate, new Vector2[]
            {
                new Vector2(0, 0),       // P0
                new Vector2(0.05f, 0.7f), // P1
                new Vector2(0.1f, 1.0f), // P2
                new Vector2(1, 1)        // P3
            }
        },
        { MaterialEasingToken.EmphasizedAccelerate, new Vector2[]
            {
                new Vector2(0, 0),       // P0
                new Vector2(0.3f, 0.0f), // P1
                new Vector2(0.8f, 0.15f), // P2
                new Vector2(1, 1)        // P3
            }
        },
        { MaterialEasingToken.Standard, new Vector2[]
            {
                new Vector2(0, 0),       // P0
                new Vector2(0.2f, 0.0f), // P1
                new Vector2(0.0f, 1.0f), // P2
                new Vector2(1, 1)        // P3
            }
        },
        { MaterialEasingToken.StandardDecelerate, new Vector2[]
            {
                new Vector2(0, 0),       // P0
                new Vector2(0, 0),       // P1
                new Vector2(0, 1),       // P2
                new Vector2(1, 1)        // P3
            }
        },
        { MaterialEasingToken.StandardAccelerate, new Vector2[]
            {
                new Vector2(0, 0),       // P0
                new Vector2(0.3f, 0),    // P1
                new Vector2(1, 1),       // P2
                new Vector2(1, 1)        // P3
            }
        },

    };

        // Dictionary to hold CubicBezier instances
        public static readonly Dictionary<MaterialEasingToken, CubicBezier> ControlCubics = new Dictionary<MaterialEasingToken, CubicBezier>();

        static MaterialBezierControlPoints()
        {
            // Initialize ControlCubics dictionary with CubicBezier instances
            foreach (var entry in ControlPoints)
            {
                ControlCubics[entry.Key] = new CubicBezier(entry.Value[0], entry.Value[1], entry.Value[2], entry.Value[3]);
            }
        }
    }
}