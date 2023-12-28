using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all effects that tween transforms.
    /// </summary>
    public abstract class LFEffectTweenTransformWithLocalBase : LFEffectTweenTransformBase
    {
        [SerializeField, EnumToggleButtons] protected WorldType worldType = WorldType.World;
        protected Vector3 startLocalValue;
        
        protected override void UpdateStartingValues()
        {
            base.UpdateStartingValues();
            startLocalValue = (mode == TransitionType.AtoB) ? ((movement == MovementType.Relative) ? GetLocalTransformValue() + beginValue : beginValue) : GetLocalTransformValue();
        }
        protected override Tween GetTween(Vector3 targetValue, float duration)
        {
            return (worldType == WorldType.World) ? GetTweenForWorldSpace(targetValue, duration) : GetTweenForLocalSpace(targetValue, duration);
        }
        protected override Vector3 GetStartingValue() => (worldType == WorldType.World) ? startValue : startLocalValue;
        protected override Vector3 GetTargetValue() => endValue;
        /// <summary>
        /// Resets target world and local values to their original state.
        /// </summary>
        protected abstract override void ResetTargetState();
        
        protected override Vector3 GetCurrentValue() => (worldType == WorldType.World) ? GetTransformValue() : GetLocalTransformValue();

        /// <summary>
        /// Returns the value which will be tweened in local space.
        /// </summary>
        /// <returns></returns>
        protected abstract Vector3 GetLocalTransformValue();

        /// <summary>
        /// Returns a tween that animates the target in local space.
        /// </summary>
        /// <param name="targetValue">The target value to animate to.</param>
        /// <param name="duration">How long will the tween last.</param>
        /// <returns>The tween itself.</returns>
        protected abstract Tween GetTweenForLocalSpace(Vector3 targetValue, float duration);

    }
}