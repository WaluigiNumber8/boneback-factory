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
        [SerializeField, HideLabel, GUIColor("GetEffectColor")] private string info;
        public string GroupSettings => $"{InactiveInfo}Settings (+{GetDelay} s) ";
        [FoldoutGroup("$GroupSettings"), SerializeField]
        private bool active = true;
        [FoldoutGroup("$GroupSettings"), SerializeField]
        private bool restartOnPlay = true;
        [FoldoutGroup("$GroupSettings"), SerializeField] 
        private bool keepStateOnStop;
        [FoldoutGroup("$GroupSettings"), HorizontalGroup("$GroupSettings/D"), SerializeField, LabelText("Initial Delay")] 
        private float initialDelayMin;
        [FoldoutGroup("$GroupSettings"), HorizontalGroup("$GroupSettings/D"), SerializeField, ShowIf("randomizeDelay"), HideLabel] 
        private float initialDelayMax;
        [FoldoutGroup("$GroupSettings"), HorizontalGroup("$GroupSettings/D", Width = 0.05f), Button("R")] 
        public void ChangeRandomizeDelay() => randomizeDelay = !randomizeDelay;
        
        [ButtonGroup, Button("Play", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f), DisableInEditorMode]
        public void TestPlay() => Play(); 
        [ButtonGroup, Button("Stop", ButtonSizes.Medium), DisableInEditorMode]
        public void TestStop() => Stop();
        [ButtonGroup(), Button("Init", ButtonSizes.Medium), DisableInEditorMode]
        public void TestInit() => Initialize();

        [SerializeField, HideInInspector] private bool randomizeDelay;
        private bool isPlaying;
        private IEnumerator delayCoroutine;
        
        private void Start() => Initialize();

        /// <summary>
        /// Play the effect.
        /// </summary>
        public void Play()
        {
            if (!active) return;
            if (restartOnPlay && isPlaying)
            {
                ResetState();
                Stop();
            }
            if (isPlaying) return;
            isPlaying = true;
            delayCoroutine = PlayCoroutine();
            if (!isActiveAndEnabled) return;
            StartCoroutine(delayCoroutine);
        }
        
        /// <summary>
        /// Stop the effect.
        /// </summary>
        public void Stop()
        {
            if (delayCoroutine != null) StopCoroutine(delayCoroutine);
            StopSelf();
            if (!keepStateOnStop) ResetState();
            else Initialize();
            isPlaying = false;
        }
        
        private IEnumerator PlayCoroutine()
        {
            float delay = (randomizeDelay) ? Random.Range(initialDelayMin, initialDelayMax) : initialDelayMin;
            yield return new WaitForSeconds(delay);
            PlaySelf();
            yield return new WaitForSeconds(TotalDuration);
            Stop();
        }

        private Color GetEffectColor()
        {
            ColorUtility.TryParseHtmlString(FeedbackColor, out Color c);
            return c;
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
        /// <summary>
        /// Reset the state to what it was before playing the effect.
        /// </summary>
        protected abstract void ResetState();
        /// <summary>
        /// The entire duration the effect is run.
        /// </summary>
        protected abstract float TotalDuration { get; }
        protected abstract string FeedbackColor { get; }
        
        private string GetDelay => (randomizeDelay) ? $"{initialDelayMin} - {initialDelayMax}" : initialDelayMin.ToString();
        private string InactiveInfo => (!active) ? "[INACTIVE] " : "";
        
        public bool IsPlaying { get => isPlaying; }
    }
}