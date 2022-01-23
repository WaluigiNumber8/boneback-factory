using System;
using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
{
    public class ThemeOverseerMono : MonoSingleton<ThemeOverseerMono>
    {
        [SerializeField] private int defaultTheme;
        [SerializeField] private ThemeStyleInfo[] themes;

        private ThemeOverseer overseer;
        
        private void Start()
        {
            overseer = ThemeOverseer.Instance;
            overseer.Initialize(themes, defaultTheme);
        }
    }
}