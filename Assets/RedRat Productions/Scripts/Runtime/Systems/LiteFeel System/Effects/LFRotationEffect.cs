using DG.Tweening;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows to rotate a specific target.
    /// </summary>
    public class LFRotationEffect : LFEffectTweenTransformWithLocalBase
    {
        protected override Tween GetTweenForWorldSpace(Vector3 targetValue, float duration)
        {
            return target.DORotate(targetValue, duration, RotateMode.FastBeyond360);
        }

        protected override Tween GetTweenForLocalSpace(Vector3 targetValue, float duration)
        {
            return target.DOLocalRotate(targetValue, duration, RotateMode.FastBeyond360);
        }
        
        protected override void ResetTargetState()
        {
            target.eulerAngles = startValue;
            target.localEulerAngles = startLocalValue;
        }

        protected override Vector3 GetTransformValue() => target.eulerAngles;
        protected override Vector3 GetLocalTransformValue() => target.localEulerAngles;
    }
}