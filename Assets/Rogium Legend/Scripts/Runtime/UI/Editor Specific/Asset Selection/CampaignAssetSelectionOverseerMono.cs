using RedRats.Core;
using UnityEngine;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Is responsible for overseeing the campaign selection menu.
    /// </summary>
    public class CampaignAssetSelectionOverseerMono : MonoSingleton<CampaignAssetSelectionOverseerMono>
    {
        [SerializeField] private AssetWallpaperController wallpaperController;
        
        private CampaignAssetSelectionOverseer overseer;
        
        private void Start()
        {
            overseer = CampaignAssetSelectionOverseer.Instance;
            overseer.Initialize(wallpaperController);
        }
    }
}