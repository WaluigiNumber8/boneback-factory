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
        [FoldoutGroup("$GroupSettings"), SerializeField] 
        private bool noPlayWhenPlaying;
        [FoldoutGroup("$GroupSettings"), SerializeField] 
        private bool isRealtime;
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
        [ButtonGroup, Button("Init", ButtonSizes.Medium), DisableInEditorMode]
        public void TestInit() => Initialize();

        [SerializeField, HideInInspector] private bool randomizeDelay;
        private bool isPlaying;
        private bool playImmediately = true;
        private IEnumerator delayCoroutine;
        
        protected virtual void Start() => Initialize();

        private void OnEnable()
        {
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForEndOfFrame(); // For GOD sakes be REALLY careful when toying around with enabling/disabling the effects.
                ResetState();                         // ANY CHANGE might return "flashing" when played OnEnable.
            }
        }
        
        private void OnDisable()
        {
            if (!isPlaying) return;
            Stop();
        }

        private void OnDestroy()
        {
            if (!isPlaying) return;
            Stop();
        }


        /// <summary>
        /// Play the effect.
        /// </summary>
        public void Play()
        {
            if (!active) return;
            if (!gameObject.activeInHierarchy) return;
            if (noPlayWhenPlaying && isPlaying) return;
            if (restartOnPlay && isPlaying)
            {
                Stop();
                ResetState();
            }
            if (isPlaying) return;
            isPlaying = true;
            delayCoroutine = PlayCoroutine();
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
            isPlaying = false;
        }
        
        /// <summary>
        /// Marks the effect as an effect that plays immediately on enable.
        /// </summary>
        public void MarkAsNotPlayImmediately() => playImmediately = false;
        
        /// <summary>
        /// Set to TRUE if the effect should be played in unscaled time.
        /// </summary>
        public void UpdateTimescale(bool isRealtime) => this.isRealtime = isRealtime;
        private IEnumerator PlayCoroutine()
        {
            float delay = (randomizeDelay) ? Random.Range(initialDelayMin, initialDelayMax) : initialDelayMin;
            if (delay > 0) yield return WaitFor(delay);
            PlaySelf();
            yield return WaitFor(TotalDuration);
            Stop();
        }

        /// <summary>
        /// Like WaitForSeconds, but turns to WaitForSecondsRealtime if isRealtime is true.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        protected IEnumerator WaitFor(float seconds)
        {
            if (!isRealtime) yield return new WaitForSeconds(seconds);
            else yield return new WaitForSecondsRealtime(seconds);
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
        
        public bool IsPlaying { get => isPlaying; }
        protected bool PlayImmediately { get => playImmediately; }
        protected bool IsRealtime { get => isRealtime; }
        private string GetDelay => (randomizeDelay) ? $"{initialDelayMin} - {initialDelayMax}" : initialDelayMin.ToString();
        private string InactiveInfo => (!active) ? "[INACTIVE] " : "";
        
    }
}