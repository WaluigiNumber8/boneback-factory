using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that controls the active status of a game object.
    /// </summary>
    public class LFGameObjectActiveStatusEffect : LFEffectWithDurationBase
    {
        [SerializeField, Required] private GameObject target;
        [SerializeField, EnumToggleButtons] private GameObjectStatusType newState = GameObjectStatusType.Activate;
        
        private bool initState;
        private Coroutine playCoroutine;
        private bool stopped;
        
        protected override void Initialize()
        {
            // Nothing to do here.
        }

        protected override void PlaySelf()
        {
            stopped = false;
            playCoroutine = StartCoroutine(PlayCoroutine());
        }

        protected override void StopSelf()
        {
            stopped = true;
            if (playCoroutine != null) StopCoroutine(playCoroutine);
        }

        protected override void ResetState()
        {
            target.SetActive(initState);
        }

        private IEnumerator PlayCoroutine()
        {
            int loops = 0;
            while (loops < TotalLoops)
            {
                initState = target.activeSelf;
                target.SetActive(GetNewActiveState());
                
                yield return new WaitForSeconds(Duration);
                
                if (stopped) break;
                loops++;
            }
        }

        private bool GetNewActiveState()
        {
            return newState switch
            {
                GameObjectStatusType.Activate => true,
                GameObjectStatusType.Deactivate => false,
                GameObjectStatusType.Toggle => !target.activeSelf,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }
        
        protected override string FeedbackColor { get => "#FFFFFF"; }
    }
}