using System;
using RedRats.Core;
using RedRats.Systems.LiteFeel.Core;
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

        private LFEffector effector;
        
        protected override void Awake()
        {
            base.Awake();
            effector = gradientEffect.GetComponentInParent<LFEffector>();
        }

        private void Start() => SwitchToMainMenu();

        public void SwitchToMainMenu()
        {
            gradientEffect.UpdateTargetColors(colors.MainMenu1, colors.MainMenu2, colors.MainMenu3);
            effector.Play();
        }
        
        public void SwitchToEditor()
        {
            gradientEffect.UpdateTargetColors(colors.Editor1, colors.Editor2, colors.Editor3);
            effector.Play();
        }
        
        public void SwitchToGameMenu()
        {
            gradientEffect.UpdateTargetColors(colors.GameMenu1, colors.GameMenu2, colors.GameMenu3);
            effector.Play();
        }
        
        public bool IsSetToMainMenu()
        {
            return gradientEffect.TargetColor1 == colors.MainMenu1 && 
                   gradientEffect.TargetColor2 == colors.MainMenu2 && 
                   gradientEffect.TargetColor3 == colors.MainMenu3;
        }
        
        public bool IsSetToEditor()
        {
            return gradientEffect.TargetColor1 == colors.Editor1 && 
                   gradientEffect.TargetColor2 == colors.Editor2 && 
                   gradientEffect.TargetColor3 == colors.Editor3;
        }
        
        public bool IsSetToGameMenu()
        {
            return gradientEffect.TargetColor1 == colors.GameMenu1 && 
                   gradientEffect.TargetColor2 == colors.GameMenu2 && 
                   gradientEffect.TargetColor3 == colors.GameMenu3;
        }

        [Serializable]
        public struct ColorInfo
        {
            [HorizontalGroup("Main Menu", Title = "Main Menu"), ColorUsage(true, true), HideLabel] 
            public Color MainMenu1, MainMenu2, MainMenu3;
            [HorizontalGroup("Editor", Title = "Editor"), ColorUsage(true, true), HideLabel] 
            public Color Editor1, Editor2, Editor3;
            [HorizontalGroup("Game Menu", Title = "Game Menu"), ColorUsage(true, true), HideLabel] 
            public Color GameMenu1, GameMenu2, GameMenu3;
        }
    }
}