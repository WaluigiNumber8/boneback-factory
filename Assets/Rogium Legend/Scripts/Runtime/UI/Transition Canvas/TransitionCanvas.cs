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

        private void Awake()
        {
            mainCam = Camera.main;
        }

        /// <summary>
        /// Fade the screen out to black.
        /// </summary>
        /// <param name="duration">The length of the fade.</param>
        /// <param name="waitForCompletion">The time to wait until the next action.</param>
        public IEnumerator FadeOut(float duration, bool waitForCompletion)
        {
            yield return Fade(1f, duration, waitForCompletion);
            mainCam.enabled = false;
            transitionCam.enabled = true;
        }
        
        /// <summary>
        /// Fades the screen back to normal.
        /// </summary>
        /// <param name="duration">The length of the fade.</param>
        /// <param name="waitForCompletion">The time to wait until the next action.</param>
        public IEnumerator FadeIn(float duration, bool waitForCompletion)
        {
            yield return Fade(0f, duration, waitForCompletion);
            mainCam.enabled = true;
            transitionCam.enabled = false;
        }

        /// <summary>
        /// Fade the canvas group.
        /// </summary>
        /// <param name="endValue">The value to reach with the tween.</param>
        /// <param name="duration">The duration of the fade.</param>
        /// <param name="waitForCompletion">The time to wait until the next action.</param>
        private IEnumerator Fade(float endValue, float duration, bool waitForCompletion)
        {
            fadeTween?.Kill();
            fadeTween = canvasGroup.DOFade(endValue, duration);

            if (!waitForCompletion) yield break;
            while (fadeTween.active && !fadeTween.IsComplete())
            {
                yield return null;
            }
            
        }
        
        
    }
}