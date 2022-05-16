using UnityEngine;

namespace RedRats.Systems.ClockOfTheGame
{
    /// <summary>
    /// Controls the game clock.
    /// </summary>
    public class GameClock
    {
        private const float DefaultTimeScale = 1f;
        private float currentTimeScale;
        
        #region Singleton Pattern
        private static GameClock instance;
        private static readonly object padlock = new object();
        public static GameClock Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new GameClock();
                    return instance;
                }
            }
        }

        #endregion

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