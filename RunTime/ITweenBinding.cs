using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Cr7Sund.TweenTimeLine
{
    public interface ITweenBinding
    {
        /// <summary>
        /// Gets the tween bound object.
        /// </summary>
        /// <typeparam name="T">The type of the bound object.</typeparam>
        /// <param name="name">The name of the bound object.</param>
        /// <returns>Returns the bound object of the specified type.</returns>
        T GetBindObj<T>(string name) where T : class;

        /// <summary>
        /// Gets the easing effect.
        /// </summary>
        /// <param name="easeName">The name of the easing effect.</param>
        /// <returns>Returns the corresponding easing effect.</returns>
        PrimeTween.Easing GetEasing(string easeName);

        /// <summary>
        /// Plays an audio clip.
        /// </summary>
        /// <param name="audioSource">The audio source.</param>
        /// <param name="clipName">The name of the audio clip.</param>
        /// <param name="clipTime">The starting time to play the clip.</param>
        void PlayAudioClip(AudioSource audioSource, string clipName, float clipTime);

        /// <summary>
        /// Sets the sprite image.
        /// </summary>
        /// <param name="image">The image component to set.</param>
        /// <param name="sprite">The name of the sprite to set.</param>
        void SetSprite(Image image, string sprite);
    }
}
