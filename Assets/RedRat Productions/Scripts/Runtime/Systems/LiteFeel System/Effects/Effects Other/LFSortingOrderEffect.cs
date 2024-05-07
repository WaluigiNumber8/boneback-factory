using System;
using System.Collections;
using RedRats.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that changes the render order of a renderer.
    /// </summary>
    public class LFSortingOrderEffect : LFEffectWithDurationBase
    {
        [SerializeField, Required] private SpriteRenderer spriteRenderer;
        [SerializeField, EnumToggleButtons] private SortingOrderType action;
        [SerializeField, SortingLayer, ShowIf("action", SortingOrderType.Set)] private int targetSortingLayer;
        [SerializeField, ShowIf("action", SortingOrderType.Set)] private int targetOrder;

        private IEnumerator playCoroutine;
        private int originalOrder;
        private int originalLayer;
        
        protected override void Initialize()
        {
            originalLayer = spriteRenderer.sortingLayerID;
            originalOrder = spriteRenderer.sortingOrder;
        }

        protected override void PlaySelf()
        {
            switch (action)
            {
                case SortingOrderType.Set:
                    spriteRenderer.sortingLayerID = targetSortingLayer;
                    spriteRenderer.sortingOrder = targetOrder;
                    break;
                case SortingOrderType.Reset:
                    spriteRenderer.sortingLayerID = originalLayer;
                    spriteRenderer.sortingOrder = originalOrder;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(action), action, "Unknown action type.");
            }
        }

        protected override void StopSelf()
        {
            //Nothing to do here.
        }

        protected override void ResetState()
        {
            spriteRenderer.sortingLayerID = originalLayer;
            spriteRenderer.sortingOrder = originalOrder;
        }
        
        protected override string FeedbackColor { get => "#FF8CCD"; }
    }
}