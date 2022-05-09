using DG.Tweening;
using UnityEngine;

namespace Rogium.DOTween.Core
{
    /// <summary>
    /// Rotates a transform when it is enabled.
    /// </summary>
    public class TweenRotate : MonoBehaviour
    {
        [SerializeField] private Vector3 rotAmount = new Vector3(0, 0, 360);
        [SerializeField] private float rotTime = 2f;
        [SerializeField] private Ease ease = Ease.Linear;
        
        private Tween rotateTween;
        
        private void OnEnable()
        {
            rotateTween = transform.DORotate(rotAmount, rotTime, RotateMode.FastBeyond360)
                                    .SetLoops(-1, LoopType.Restart)
                                    .SetEase(ease);
        }

        private void OnDisable() => rotateTween.Kill();
    }
}