using System.Collections;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all LF camera effects.
    /// </summary>
    public abstract class LFEffectCameraBase : LFEffectTweenBase
    {
        protected override void Initialize()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                DelayedInitialize();
                base.Initialize();
            }
        }

        protected override void SetBeginState()
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                DelayedSetBeginState();
            }
        }

        protected override void ResetTargetState()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                DelayedResetTargetState();
            }
        }

        protected override void UpdateStartingValues()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                DelayedUpdateStartingValues();
            }
        }

        /// <summary>
        /// Initialize all values needed for the effect at the end of the frame.
        /// </summary>
        protected abstract void DelayedInitialize();
        /// <summary>
        /// Set parameters to beginning states right before tweening but at the end of the frame.
        /// </summary>
        protected abstract void DelayedSetBeginState();
        /// <summary>
        /// Reset the target to its state before the tween happened at the end of the frame.
        /// </summary>
        protected abstract void DelayedResetTargetState();
        /// <summary>
        /// Updates starting values to current values of the current target at the end of the frame.
        /// </summary>
        protected abstract void DelayedUpdateStartingValues();
    }
}