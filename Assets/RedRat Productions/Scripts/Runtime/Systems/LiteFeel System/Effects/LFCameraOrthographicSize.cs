using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFCameraOrthographicSize : LFEffectTweenBase
    {
        [Header("Orthographic Size")]
        [SerializeField, EnumToggleButtons] protected MovementType movement = MovementType.Absolute;
        [SerializeField, EnumToggleButtons] protected TransitionType mode = TransitionType.ToDestination;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] protected float beginSize = 1f;
        [SerializeField] protected float targetSize = 6f;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease sizeEasing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve sizeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        private float startOrthographicSize;
        private Camera cam;

        protected override void Initialize() => cam = Camera.main;

        protected override void SetBeginState()
        {
            if (mode != TransitionType.AtoB) return;
            cam.orthographicSize = beginSize;
        }

        protected override void SetupTweens()
        {
            float targetValue = (movement == MovementType.Relative) ? cam.orthographicSize + targetSize : targetSize;
            Tween tween = cam.DOOrthoSize(targetValue, duration);
            AddFloatTweenToSequence(tween, smoothing, sizeEasing, sizeCurve);
        }

        protected override void ResetTargetState() => cam.orthographicSize = startOrthographicSize;
        protected override void UpdateStartingValues() => startOrthographicSize = cam.orthographicSize;
    }
}