using System.Collections;
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
        [SerializeField] protected AnimationCurve sizeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        private CinemachineVirtualCamera cam;
        private float startOrthographicSize;
        private Camera camMain;

        protected override void Initialize()
        {
            camMain = Camera.main;
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                cam = GetActiveCamera();
                startOrthographicSize = camMain.orthographicSize;
                base.Initialize();
            }
        }

        protected override void SetBeginState()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                cam = GetActiveCamera();
                if (mode != TransitionType.AToB) yield break;
                cam.m_Lens.OrthographicSize = beginSize;
            }
        }

        protected override void SetupTweens()
        {
            float targetValue = (movement == MovementType.Relative) ? cam.m_Lens.OrthographicSize + targetSize : targetSize;
            Tween tween = DOTween.To(() => cam.m_Lens.OrthographicSize, x => cam.m_Lens.OrthographicSize = x, targetValue, Duration);
            AddTweenToSequence(tween, sizeCurve);
        }

        protected override void ResetTargetState()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return null;
                cam.m_Lens.OrthographicSize = startOrthographicSize;
            }
        }

        protected override void UpdateStartingValues()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame();
                startOrthographicSize = GetActiveCamera().m_Lens.OrthographicSize;
            }
        }

        private CinemachineVirtualCamera GetActiveCamera()
        {
            CinemachineVirtualCamera vcam = RedRatUtils.GetActiveVCam();
            return (vcam == cam) ? cam : vcam;
        }
        
        protected override string FeedbackColor { get => "#56CDFF"; }
    }
}
