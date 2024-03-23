using System;
using UnityEngine;

namespace RedRats.Systems.Clocks
{
    /// <summary>
    /// Represents a timer that counts down.
    /// </summary>
    public class CountdownTimer
    {
        private readonly Action whenStarted, whenFinished;
        private float timer;

        public CountdownTimer(Action whenFinished, Action whenStarted = null)
        {
            this.whenStarted = whenStarted;
            this.whenFinished = whenFinished;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        /// <param name="time">How many seconds the timer lasts</param>
        public void Start(float time)
        {
            timer = time;
            whenStarted?.Invoke();
        }

        /// <summary>
        /// Countdown the timer if it was started. Once reaches 0, the whenFinished method will be invoked.
        /// </summary>
        public void Tick()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                    whenFinished?.Invoke();
                }
            }
        }
        
        /// <summary>
        /// How much time is left.
        /// </summary>
        public float TimeLeft => timer;
    }
}