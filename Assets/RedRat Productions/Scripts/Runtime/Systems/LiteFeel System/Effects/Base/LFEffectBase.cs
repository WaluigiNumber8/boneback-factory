using System;
using System.Collections;
using RedRats.Systems.LiteFeel.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all LF effects.
    /// </summary>
    [RequireComponent(typeof(LFEffector))]
    public abstract class LFEffectBase : MonoBehaviour
    {
        public string GroupSettings => $"Settings (+{GetDelay} s)";
        [FoldoutGroup("$GroupSettings"), SerializeField] 
        private bool playOnEnable;
        [FoldoutGroup("$GroupSettings"), HorizontalGroup("$GroupSettings/D"), SerializeField, LabelText("Initial Delay")] 
        private float initialDelayMin;
        [FoldoutGroup("$GroupSettings"), HorizontalGroup("$GroupSettings/D", MaxWidth = 0.27f), SerializeField, ShowIf("randomizeDelay"), HideLabel] 
        private float initialDelayMax;
        [FoldoutGroup("$GroupSettings"), HorizontalGroup("$GroupSettings/D", MaxWidth = 0.05f), Button("R")] 
        public void ChangeRandomizeDelay() => randomizeDelay = !randomizeDelay;
        
        [ButtonGroup, Button("Play", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f), DisableInEditorMode]
        public void TestPlay() => Play(); 
        [ButtonGroup, Button("Stop", ButtonSizes.Medium), DisableInEditorMode]
        public void TestStop() => Stop();

        private bool randomizeDelay;
        private IEnumerator delayCoroutine;
        
        protected virtual void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            if (playOnEnable) Play();
        }

        /// <summary>
        /// Play the effect.
        /// </summary>
        public void Play()
        {
            if (!isActiveAndEnabled) return;
            delayCoroutine = PlayCoroutine();
            StartCoroutine(delayCoroutine);
        }
        
        /// <summary>
        /// Stop the effect.
        /// </summary>
        public void Stop()
        {
            if (!isActiveAndEnabled) return;
            StopCoroutine(delayCoroutine);
            StopSelf();
        }
        
        private IEnumerator PlayCoroutine()
        {
            float delay = (randomizeDelay) ? Random.Range(initialDelayMin, initialDelayMax) : initialDelayMin;
            yield return new WaitForSeconds(delay);
            PlaySelf();
        }
        
        /// <summary>
        /// Initialize all values needed for the effect.
        /// </summary>
        protected abstract void Initialize();
        /// <summary>
        /// Activates the effect.
        /// </summary>
        protected abstract void PlaySelf();
        /// <summary>
        /// Disables the effect.
        /// </summary>
        protected abstract void StopSelf();

        [Serializable]
        public struct SettingsInfo
        {
            
        }
        
        private string GetDelay => (randomizeDelay) ? $"{initialDelayMin} - {initialDelayMax}" : initialDelayMin.ToString();
    }
}