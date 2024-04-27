using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Rogium.UserInterface.TransitionsCanvas
{
    /// <summary>
    /// Controls visual transitions.
    /// </summary>
    public class TransitionCanvas : MonoBehaviour
    {
        [SerializeField] private Camera transitionCam;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private Camera mainCam;
        private Tween fadeTween;

        private void Awake() => mainCam = Camera.main;

        /// <summary>
        /// Fade the screen out to black.
        /// </summary>
        /// <param name="duration">The length of the fade.</param>
        /// <param name="waitForCompletion">The time to wait until the next action.</param>
        public IEnumerator FadeOut(float duration, bool waitForCompletion)
        {
            yield return FadeTo(1f, duration, waitForCompletion, true);
        }
        
        /// <summary>
        /// Fades the screen back to normal.
        /// </summary>
        /// <param name="duration">The length of the fade.</param>
        /// <param name="waitForCompletion">The time to wait until the next action.</param>
        public IEnumerator FadeIn(float duration, bool waitForCompletion)
        {
            yield return FadeTo(0f, duration, waitForCompletion, false);
        }

        /// <summary>
        /// Fades the screen back to normal.
        /// </summary>
        /// <param name="value">The target alpha value of the canvas.</param>
        /// <param name="duration">The length of the fade.</param>
        /// <param name="waitForCompletion">The time to wait until the next action.</param>
        /// <param name="enableTransitionCam">Should the transition cam be enabled at the end.</param>
        public IEnumerator FadeTo(float value, float duration, bool waitForCompletion, bool enableTransitionCam)
        {
            yield return ProcessFade(value, duration, waitForCompletion);
            mainCam.enabled = !enableTransitionCam;
            transitionCam.enabled = enableTransitionCam;
        }

        /// <summary>
        /// Fade the canvas group.
        /// </summary>
        /// <param name="endValue">The value to reach with the tween.</param>
        /// <param name="duration">The duration of the fade.</param>
        /// <param name="waitForCompletion">The time to wait until the next action.</param>
        private IEnumerator ProcessFade(float endValue, float duration, bool waitForCompletion)
        {
            fadeTween?.Kill();
            fadeTween = canvasGroup.DOFade(endValue, duration).SetUpdate(true);

            if (!waitForCompletion) yield break;
            while (fadeTween.active && !fadeTween.IsComplete())
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}