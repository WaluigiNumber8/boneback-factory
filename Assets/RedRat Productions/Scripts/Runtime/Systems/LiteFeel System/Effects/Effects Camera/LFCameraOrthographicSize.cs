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
        [SerializeField] protected AnimationCurve sizeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        private float startOrthographicSize;
        private Camera cam;

        protected override void Initialize()
        {
            cam = Camera.main;
            base.Initialize();
        }

        protected override void SetBeginState()
        {
            if (mode != TransitionType.AToB) return;
            cam.orthographicSize = beginSize;
        }

        protected override void SetupTweens()
        {
            float targetValue = (movement == MovementType.Relative) ? cam.orthographicSize + targetSize : targetSize;
            Tween tween = cam.DOOrthoSize(targetValue, Duration);
            AddTweenToSequence(tween, sizeCurve);
        }

        protected override void ResetTargetState() => cam.orthographicSize = startOrthographicSize;
        protected override void UpdateStartingValues() => startOrthographicSize = cam.orthographicSize;
        protected override string FeedbackColor { get => "#56CDFF"; }
    }
}