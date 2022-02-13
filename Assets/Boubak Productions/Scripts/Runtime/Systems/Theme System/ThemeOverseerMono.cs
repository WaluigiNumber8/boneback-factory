using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
{
    public class ThemeOverseerMono : MonoSingleton<ThemeOverseerMono>
    {
        [SerializeField] private int defaultThemeIndex;
        [SerializeField] private ThemeStyleAsset[] themes;
        
        private ThemeOverseer overseer;
        
        private void Start()
        {
            overseer = ThemeOverseer.Instance;
            overseer.Initialize(themes, defaultThemeIndex);
        }
    }
}