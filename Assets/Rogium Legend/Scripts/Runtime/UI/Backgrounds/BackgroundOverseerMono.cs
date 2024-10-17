using System;
using RedRats.Core;
using RedRats.Systems.LiteFeel.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.UserInterface.Backgrounds
{
    /// <summary>
    /// Controls the background of the menu.
    /// </summary>
    public class BackgroundOverseerMono : MonoSingleton<BackgroundOverseerMono>
    {
        [SerializeField] private LFShaderGradientEffect gradientEffect;
        [SerializeField] private ColorInfo colors;

        protected override void Awake()
        {
            base.Awake();
            gradientEffect.UpdateTargetColors(colors.MainMenu1, colors.MainMenu2, colors.MainMenu3);
            gradientEffect.Play();
        }

        public bool IsSetToMainMenu()
        {
            return gradientEffect.TargetColor1 == colors.MainMenu1 && 
                   gradientEffect.TargetColor2 == colors.MainMenu2 && 
                   gradientEffect.TargetColor3 == colors.MainMenu3;
        }

        [Serializable]
        public struct ColorInfo
        {
            [HorizontalGroup("Main Menu", Title = "Main Menu"), ColorUsage(true, true), HideLabel] 
            public Color MainMenu1, MainMenu2, MainMenu3;
        }
    }
}