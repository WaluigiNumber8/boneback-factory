using RedRats.Core;
using UnityEngine;

namespace RedRats.Systems.Clocks
{
    /// <summary>
    /// Controls the game clock.
    /// </summary>
    public sealed class GameClock : Singleton<GameClock>
    {
        private const float DefaultTimeScale = 1f;
        private float currentTimeScale;
        
        private GameClock() => currentTimeScale = DefaultTimeScale;

        /// <summary>
        /// Change the time scale of the game.
        /// </summary>
        /// <param name="timeScale">The new scale to use for the game clock.</param>
        public void Change(float timeScale)
        {
            currentTimeScale = timeScale;
            UpdateTimeScale(currentTimeScale);
        }

        /// <summary>
        /// Resets the Time Scale to default.
        /// </summary>
        public void Reset()
        {
            currentTimeScale = DefaultTimeScale;
            UpdateTimeScale(currentTimeScale);
        }

        /// <summary>
        /// Stops the time.
        /// </summary>
        public void Pause() => UpdateTimeScale(0f);

        /// <summary>
        /// Resumes the time.
        /// </summary>
        public void Resume() => UpdateTimeScale(currentTimeScale);

        /// <summary>
        /// Updates the time scale.
        /// </summary>
        /// <param name="timeScale">The new time scale.</param>
        private void UpdateTimeScale(float timeScale) => Time.timeScale = timeScale;
        
        public float Scale { get => Time.timeScale; }
    }
}