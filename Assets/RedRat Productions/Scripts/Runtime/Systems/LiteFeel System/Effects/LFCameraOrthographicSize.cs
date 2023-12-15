using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFCameraOrthographicSize : LFEffectTweenBase<float>
    {
        [Header("Orthographic Size")]
        [SerializeField] protected float targetSize = 6f;
        [SerializeField, EnumToggleButtons] protected MovementType mode = MovementType.Absolute;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve movementCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        private float startOrthographicSize;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            startOrthographicSize = cam.orthographicSize;
        }

        protected override void Tween(float valueToReach, float duration, bool forceAbsolute = false)
        {
            float targetValue = (!forceAbsolute && mode == MovementType.Relative) ? cam.orthographicSize + valueToReach : valueToReach;
            tween = cam.DOOrthoSize(targetValue, duration);
            tween = (smoothing == SmoothingType.Tween) ? tween.SetEase(easing) : tween.SetEase(movementCurve);
        }

        protected override float GetStartingValue() => startOrthographicSize;
        protected override float GetTargetValue() => targetSize;
        protected override void ResetTargetState() => cam.orthographicSize = startOrthographicSize;
    }
}