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

        protected override void Awake()
        {
            base.Awake();
            SwitchToEditor();
        }

        public void SwitchToMainMenu()
        {
            gradientEffect.UpdateTargetColors(colors.MainMenu1, colors.MainMenu2, colors.MainMenu3);
            gradientEffect.Play();
        }
        
        public void SwitchToEditor()
        {
            gradientEffect.UpdateTargetColors(colors.Editor1, colors.Editor2, colors.Editor3);
            gradientEffect.GetComponentInParent<LFEffector>().Play();
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

        [Serializable]
        public struct ColorInfo
        {
            [HorizontalGroup("Main Menu", Title = "Main Menu"), ColorUsage(true, true), HideLabel] 
            public Color MainMenu1, MainMenu2, MainMenu3;
            [HorizontalGroup("Editor", Title = "Editor"), ColorUsage(true, true), HideLabel] 
            public Color Editor1, Editor2, Editor3;
            
        }

        
    }
}