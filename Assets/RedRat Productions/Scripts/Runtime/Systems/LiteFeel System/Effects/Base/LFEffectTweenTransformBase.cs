using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all effects that tween transforms.
    /// </summary>
    public abstract class LFEffectTweenTransformBase : LFEffectTweenSingleBase<Vector3>
    {
        [Header("Target")]
        [SerializeField] protected Transform target;
        
        [Header("Transform")] 
        [SerializeField] protected TransitionType mode = TransitionType.AtoB;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] protected Vector3 beginValue;
        [SerializeField] protected Vector3 endValue;
        [SerializeField, EnumToggleButtons] protected MovementType movement = MovementType.Absolute;
        
        protected Vector3 startValue;
        
        protected override void Tween(Vector3 valueToReach, float duration, bool forceAbsolute = false)
        {
            Vector3 targetValue = (!forceAbsolute && movement == MovementType.Relative) ? GetCurrentValue() + valueToReach : valueToReach;
            tween = GetTween(targetValue, duration);
        }
        
        protected override void UpdateStartingValues()
        {
            startValue = (mode == TransitionType.AtoB) ? ((movement == MovementType.Relative) ? GetTransformValue() + beginValue : beginValue) : GetTransformValue();
        }
        
        /// <summary>
        /// Returns the tween used for the tweening action.
        /// </summary>
        /// <param name="targetValue">The target value to animate to.</param>
        /// <param name="duration">How long will the tween last.</param>
        /// <returns>The tween itself.</returns>
        protected virtual Tween GetTween(Vector3 targetValue, float duration) => GetTweenForWorldSpace(targetValue, duration);
        protected override Vector3 GetStartingValue() => startValue;
        protected override Vector3 GetTargetValue() => endValue;
        /// <summary>
        /// Resets target world values to their original state.
        /// </summary>
        protected abstract override void ResetTargetState();
        
        /// <summary>
        /// Returns the current value of the target, depending on world type.
        /// </summary>
        protected virtual Vector3 GetCurrentValue() => GetTransformValue();

        /// <summary>
        /// Returns the value which will be tweened in world space.
        /// </summary>
        protected abstract Vector3 GetTransformValue();
        /// <summary>
        /// Returns a tween that animates the target in world space.
        /// </summary>
        /// <param name="targetValue">The target value to animate to.</param>
        /// <param name="duration">How long will the tween last.</param>
        /// <returns>The tween itself.</returns>
        protected abstract Tween GetTweenForWorldSpace(Vector3 targetValue, float duration);

        protected override void Initialize()
        {
            //Nothing to do here.
        }
    }
}