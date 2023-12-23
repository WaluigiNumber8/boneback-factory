using DG.Tweening;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows to scale a specific target.
    /// </summary>
    public class LFScaleEffect : LFEffectTweenTransformBase
    {
        protected override void ResetTargetState() => target.localScale = startValue;

        protected override Vector3 GetTransformValue() => target.localScale;

        protected override Tween GetTweenForWorldSpace(Vector3 targetValue, float duration)
        {
            return target.DOScale(targetValue, duration);
        }
    }
}