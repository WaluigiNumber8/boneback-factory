using System;
using BoubakProductions.Systems.ClockOfTheGame;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Allows entities to look like something and have animations.
    /// </summary>
    public class CharacteristicVisual : CharacteristicBase
    {
        [SerializeField] private new SpriteRenderer renderer;
        [SerializeField] private CharVisualInfo defaultData;

        private int frameCountdown;
        private Action whenFrameChanges;
        private bool altSpriteOn;

        private void Awake() => renderer.sprite = defaultData.baseSprite;

        private void Update()
        {
            frameCountdown -= (int)(1 * GameClock.Instance.Scale);
            ProcessSwitchFrame();
        }

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharVisualInfo newInfo)
        {
            defaultData = newInfo;
            renderer.sprite = defaultData.baseSprite;

            renderer.flipX = false;
            renderer.flipY = false;
            altSpriteOn = false;

            whenFrameChanges = defaultData.animationType switch
            {
                AnimationType.NoAnimation => null,
                AnimationType.HorizontalFlip => ProcessHorizontalFlip,
                AnimationType.VerticalFlip => ProcessVerticalFlip,
                AnimationType.SpriteSwap => ProcessSpriteSwap,
                _ => throw new ArgumentOutOfRangeException($"The Animation Type of '{defaultData.animationType}' is not supported.")
            };
            ChangeRenderState(true);
        }

        /// <summary>
        /// Change the visibility status of the renderer.
        /// </summary>
        /// <param name="isEnabled">If true, rendering wil be enabled.</param>
        public void ChangeRenderState(bool isEnabled)
        {
            renderer.enabled = isEnabled;
        }
        
        /// <summary>
        /// Resets the frame duration when the end is reached.
        /// </summary>
        private void ProcessSwitchFrame()
        {
            if (frameCountdown > 0) return;
            whenFrameChanges?.Invoke();
            frameCountdown = defaultData.frameDuration;
        }
        
        private void ProcessHorizontalFlip() => renderer.flipX = !renderer.flipX;
        private void ProcessVerticalFlip() => renderer.flipY = !renderer.flipY;
        private void ProcessSpriteSwap() => renderer.sprite = (altSpriteOn) ? defaultData.spriteAlt : defaultData.baseSprite;
    }
}