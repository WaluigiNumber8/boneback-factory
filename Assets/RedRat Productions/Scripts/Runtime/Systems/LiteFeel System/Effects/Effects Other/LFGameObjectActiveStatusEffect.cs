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
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private GameObject target;
        [SerializeField, EnumToggleButtons] private GameObjectStatusType newState = GameObjectStatusType.Activate;
        
        private bool initState;
        private Coroutine playCoroutine;
        private bool stopped;
        
        protected override void Initialize()
        {
            // Nothing to do here.
        }

        protected override void PlaySetup()
        {
            stopped = false;
            playCoroutine = StartCoroutine(PlayCoroutine());
        }

        protected override void StopSetup()
        {
            stopped = true;
            target.SetActive(initState);
            if (playCoroutine != null) StopCoroutine(playCoroutine);
        }

        private IEnumerator PlayCoroutine()
        {
            int loops = 0;
            int finalLoops = (loopAmount == -1) ? int.MaxValue : loopAmount;
            while (loops < finalLoops)
            {
                initState = target.activeSelf;
                target.SetActive(GetNewActiveState());
                
                yield return new WaitForSeconds(duration);
                
                if (resetOnEnd) target.SetActive(initState);
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