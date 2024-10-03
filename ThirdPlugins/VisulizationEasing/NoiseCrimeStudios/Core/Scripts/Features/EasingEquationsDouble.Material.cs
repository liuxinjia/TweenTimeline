/* Unity Easing - Credits
 * Modified version of PennerDoubleAnimation tweaked for Unity.
 * Converted Doubles to doubles
 * 
 * 
 * PennerDoubleAnimation
 * Animates the value of a Double property between two target values using 
 * Robert Penner's easing equations for interpolation over a specified Duration.
 *
 * @author		Darren David darren-code@lookorfeel.com
 * @version		1.0
 *
 * Credit/Thanks:
 * Robert Penner - The easing equations we all know and love 
 *   (http://robertpenner.com/easing/) [See License.txt for license info]
 * 
 *  TERMS OF USE - EASING EQUATIONS
 *  
 *  Open source under the BSD License. 
 *  
 *  Copyright � 2001 Robert Penner
 *  All rights reserved.

 *  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

 *  Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 *  Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
 *  Neither the name of the author nor the names of contributors may be used to endorse or promote products derived from this software without specific prior written permission.
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
 *  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
 *  (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 *  STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *  
 *  
 * Lee Brimelow - initial port of Penner's equations to WPF 
 *   (http://thewpfblog.com/?p=12)
 * 
 * Zeh Fernando - additional equations (out/in) from 
 *   caurina.transitions.Tweener (http://code.google.com/p/tweener/)
 *   [See License.txt for license info]
 * 
 *
 * References
 * Sources:			http://gsgd.co.uk/sandbox/jquery/easing/
 * 					http://gsgd.co.uk/sandbox/jquery/easing/jquery.easing.1.3.js
 * 					https://api.jquery.com/animate/
 * 
 *  HTML Demo:		http://easings.net/
 * 					https://github.com/ai/easings.net
 * 
 *  Robert Penner	http://robertpenner.com/easing/
 * 					http://www.timotheegroleau.com/Flash/experiments/easing_function_generator.htm
 */

namespace NoiseCrimeStudios.Core.Features.Easing
{
    public partial class EasingEquationsDouble
    {
        /// <summary>
        /// Enumeration of all easing equations for use with CreateDelegate.
        /// </summary>
        public enum MaterialEquations
        {
            Emphasized,
            EmphasizedDecelerate,
            EmphasizedAccelerate,
            Standard,
            StandardDecelerate,
            StandardAccelerate,
            Linear
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Change in value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static double Emphasized(double t, double b, double c, double d)
        {
            // Get the Bezier control points for the 'Emphasized' easing function
            Cr7Sund.TweenTimeLine.MaterialBezierControlPoints.ControlCubics.TryGetValue(
                Cr7Sund.TweenTimeLine.MaterialEasingToken.Emphasized,
                out var cubic);

            // Scale the Bezier result to the actual animation range [b, b + c]
            return cubic.Evaluate(t, b, c, d);
        }
        // Similar methods for other easing functions can be implemented here
        public static double EmphasizedDecelerate(double t, double b, double c, double d)
        {
            Cr7Sund.TweenTimeLine.MaterialBezierControlPoints.ControlCubics.TryGetValue(
                Cr7Sund.TweenTimeLine.MaterialEasingToken.EmphasizedDecelerate,
                out var cubic);

            return cubic.Evaluate(t, b, c, d);
        }

        public static double EmphasizedAccelerate(double t, double b, double c, double d)
        {
            Cr7Sund.TweenTimeLine.MaterialBezierControlPoints.ControlCubics.TryGetValue(
                Cr7Sund.TweenTimeLine.MaterialEasingToken.EmphasizedAccelerate,
                out var cubic);

            return cubic.Evaluate(t, b, c, d);
        }

        public static double Standard(double t, double b, double c, double d)
        {
            Cr7Sund.TweenTimeLine.MaterialBezierControlPoints.ControlCubics.TryGetValue(
                Cr7Sund.TweenTimeLine.MaterialEasingToken.Standard,
                out var cubic);

            return cubic.Evaluate(t, b, c, d);
        }

        public static double StandardDecelerate(double t, double b, double c, double d)
        {
            Cr7Sund.TweenTimeLine.MaterialBezierControlPoints.ControlCubics.TryGetValue(
                Cr7Sund.TweenTimeLine.MaterialEasingToken.StandardDecelerate,
                out var cubic);

            return cubic.Evaluate(t, b, c, d);
        }

        public static double StandardAccelerate(double t, double b, double c, double d)
        {
            Cr7Sund.TweenTimeLine.MaterialBezierControlPoints.ControlCubics.TryGetValue(
                Cr7Sund.TweenTimeLine.MaterialEasingToken.StandardAccelerate,
                out var cubic);

            return cubic.Evaluate(t, b, c, d);
        }
    }
}
