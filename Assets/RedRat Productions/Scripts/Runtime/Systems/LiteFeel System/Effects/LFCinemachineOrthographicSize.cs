using Cinemachine;
using DG.Tweening;
using RedRats.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFCinemachineOrthographicSize : LFEffectTweenBase
    {
        [Header("Orthographic Size")]
        [SerializeField, EnumToggleButtons] protected MovementType movement = MovementType.Absolute;
        [SerializeField, EnumToggleButtons] protected TransitionType mode = TransitionType.ToDestination;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] protected float beginSize = 1f;
        [SerializeField] protected float targetSize = 6f;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease sizeEasing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve sizeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        private CinemachineVirtualCamera cam;
        private float startOrthographicSize;

        protected override void Initialize()
        {
            cam = GetActiveCamera();
        }

        protected override void SetBeginState()
        {
            cam = GetActiveCamera();
            
            if (mode != TransitionType.AToB) return;
            cam.m_Lens.OrthographicSize = beginSize;
        }

        protected override void SetupTweens()
        {
            float targetValue = (movement == MovementType.Relative) ? cam.m_Lens.OrthographicSize + targetSize : targetSize;
            Tween tween = DOTween.To(() => cam.m_Lens.OrthographicSize, x => cam.m_Lens.OrthographicSize = x, targetValue, duration);
            AddFloatTweenToSequence(tween, smoothing, sizeEasing, sizeCurve);
        }

        protected override void ResetTargetState() => cam.m_Lens.OrthographicSize = startOrthographicSize;
        protected override void UpdateStartingValues()
        {
            startOrthographicSize = GetActiveCamera().m_Lens.OrthographicSize;
        }

        private CinemachineVirtualCamera GetActiveCamera()
        {
            CinemachineVirtualCamera vcam = RedRatUtils.GetActiveVCam();
            return (vcam == cam) ? cam : vcam;
        }
    }
}
