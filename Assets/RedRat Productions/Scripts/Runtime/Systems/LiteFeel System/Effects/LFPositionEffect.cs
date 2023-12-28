using DG.Tweening;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows moving a specific target.
    /// </summary>
    public class LFPositionEffect : LFEffectTweenTransformWithLocalBase
    {
        protected override void ResetTargetState()
        {
            target.position = startValue;
            target.localPosition = startLocalValue;
        }

        protected override Vector3 GetTransformValue() => target.position;

        protected override Vector3 GetLocalTransformValue() => target.localPosition;

        protected override Tween GetTweenForWorldSpace(Vector3 targetValue, float duration)
        {
            return target.DOMove(targetValue, duration);
        }

        protected override Tween GetTweenForLocalSpace(Vector3 targetValue, float duration)
        {
            return target.DOLocalMove(targetValue, duration);
        }
    }
}