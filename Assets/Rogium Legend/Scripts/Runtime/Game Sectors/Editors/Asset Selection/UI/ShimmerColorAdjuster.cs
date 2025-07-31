using RedRats.Core.Utils;
using RedRats.Systems.Themes;
using UnityEngine;

namespace Rogium.Editors.AssetSelection.UI
{
    /// <summary>
    /// Adjusts a shimmer effect color based on current theme.
    /// </summary>
    public class ShimmerColorAdjuster : MonoBehaviour
    {
        private static readonly int ShimmerColor = Shader.PropertyToID("_ShimmerColor");
        
        [SerializeField] private MaterialExtractor material;
        [SerializeField] private bool adjustOnEnable = true;
        private ThemeOverseerMono themeOverseer;

        private void Awake() => themeOverseer = ThemeOverseerMono.Instance;

        private void OnEnable()
        {
            if (adjustOnEnable) Adjust();
        }
        
        public async void Adjust(ThemeType theme = ThemeType.Current)
        {
            await Awaitable.NextFrameAsync();
            material.Get().SetColor(ShimmerColor, themeOverseer.GetThemeData(theme).Colors.shimmerEffects);
        }
    }
}