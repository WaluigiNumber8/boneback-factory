using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Systems.TweenAnimations
{
    /// <summary>
    /// Animate a transform as an floating item.
    /// </summary>
    public class TweenAnimFloating : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float duration = 2f;
        [SerializeField] private Ease easeType = Ease.InOutSine;
        
        private Transform ttransform;
        private Vector3 startPos;
        private Vector3 beginPos;
        private Vector3 endPos;
        
        private IEnumerator Start()
        {
            ttransform = transform;
            startPos = ttransform.localPosition;
            beginPos = startPos - offset;
            endPos = startPos + offset;

            yield return new WaitForSeconds(Random.Range(0f, 0.1f));
            
            ttransform.localPosition = beginPos;
            ttransform.DOLocalMove(endPos, duration).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() => transform.DOKill();
    }
}