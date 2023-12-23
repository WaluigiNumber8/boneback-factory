using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFCinemachineOrthographicSize : LFEffectTweenBase<float>
    {
        [Header("Orthographic Size")]
        [SerializeField] protected float targetSize = 6f;
        [SerializeField, EnumToggleButtons] protected MovementType mode = MovementType.Absolute;

        private CinemachineVirtualCamera cam;
        private float startOrthographicSize;

        private void Start() => cam = GetActiveCamera();

        protected override void Tween(float valueToReach, float duration, bool forceAbsolute = false)
        {
            cam = GetActiveCamera();
            float targetValue = (!forceAbsolute && mode == MovementType.Relative) ? cam.m_Lens.OrthographicSize + valueToReach : valueToReach;
            tween = DOTween.To(() => cam.m_Lens.OrthographicSize, x => cam.m_Lens.OrthographicSize = x, targetValue, duration);
        }

        protected override float GetStartingValue() => startOrthographicSize;
        protected override float GetTargetValue() => targetSize;
        protected override void ResetTargetState() => cam.m_Lens.OrthographicSize = startOrthographicSize;
        
        private CinemachineVirtualCamera GetActiveCamera()
        {
            CinemachineVirtualCamera vcam = (CinemachineVirtualCamera) CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            if (vcam == cam) return cam;
            startOrthographicSize = vcam.m_Lens.OrthographicSize;
            return vcam;
        }
    }
}
