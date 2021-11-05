using BoubakProductions.Core;
using Rogium.Editors.Packs;
using UnityEngine;

namespace Rogium.Global.UISystem.AssetSelection
{
    /// <summary>
    /// Is responsible for overseeing the campaign selection menu.
    /// </summary>
    public class CampaignAssetSelectionOverseerMono : MonoSingleton<CampaignAssetSelectionOverseerMono>
    {
        [SerializeField] private AssetWallpaperController wallpaperController;
        [SerializeField] private int startingIndex;
        
        private CampaignAssetSelectionOverseer overseer;
        
        private void Start()
        {
            overseer = CampaignAssetSelectionOverseer.Instance;
            overseer.Initialize(wallpaperController, startingIndex);
        }
    }
}