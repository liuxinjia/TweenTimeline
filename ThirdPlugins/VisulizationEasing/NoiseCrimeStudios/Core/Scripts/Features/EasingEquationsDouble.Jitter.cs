namespace NoiseCrimeStudios.Core.Features.Easing
{
    public partial class EasingEquationsDouble
    {
        /// <summary>
        /// Enumeration of all easing equations for use with CreateDelegate.
        /// </summary>
        public enum JitterEquations
        {
            Smooth,
            Natural,
            SlowDown,
            Accelerate,
            Elastic,
            Bounce,
            Overshoot,
            Impulse,
            Swing,
            Linear,
        }

        /// <summary>
        /// Easing function for Smooth.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Smooth(double t, double b, double c, double d)
        {
            return QuartEaseInOut(t, b, c, d); // QuartEaseInOut provides a smooth, symmetrical easing
        }

        /// <summary>
        /// Easing function for Natural.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Natural(double t, double b, double c, double d)
        {
            return SineEaseInOut(t, b, c, d); // EaseInOutSine provides a natural, smooth transition
        }

        /// <summary>
        /// Easing function for Slow Down.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double SlowDown(double t, double b, double c, double d)
        {
            return QuadEaseOut(t, b, c, d); // EaseOutQuad slows down towards the end
        }

        /// <summary>
        /// Easing function for Accelerate.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Accelerate(double t, double b, double c, double d)
        {
            return QuadEaseIn(t, b, c, d); // EaseInQuad accelerates from the start
        }

        /// <summary>
        /// Easing function for Elastic.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Elastic(double t, double b, double c, double d)
        {
            return ElasticEaseInOut(t, b, c, d); // EaseInOutElastic provides an elastic bounce
        }

        /// <summary>
        /// Easing function for Bounce.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Bounce(double t, double b, double c, double d)
        {
            return BounceEaseOut(t, b, c, d); // EaseOutBounce simulates a bouncing effect
        }

        /// <summary>
        /// Easing function for Overshoot.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Overshoot(double t, double b, double c, double d)
        {
            return BackEaseInOut(t, b, c, d); // BackEaseInOut provides an overshooting effect
        }

        /// <summary>
        /// Easing function for Impulse.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Impulse(double t, double b, double c, double d)
        {
            return QuadEaseInOut(t, b, c, d); // EaseInQuart provides a quick start effect
        }

        /// <summary>
        /// Easing function for Swing.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Swing(double t, double b, double c, double d)
        {
            return BackEaseInOut(t, b, c, d); // BackEaseInOut can simulate a swinging effect
        }
    }
}
