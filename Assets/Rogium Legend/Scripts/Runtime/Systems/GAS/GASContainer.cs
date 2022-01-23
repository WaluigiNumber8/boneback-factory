using BoubakProductions.Core;
using Rogium.UserInterface.AssetSelection;
using UnityEngine;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// Allows GAS to access MonoBehaviours.
    /// </summary>
    public class GASContainer : MonoSingleton<GASContainer>
    {
        [SerializeField] private AssetSelectionOverseerMono assetSelection;
        [SerializeField] private AssetSelectionPicker assetSelectionPicker;

        protected override void Awake()
        {
            GASRogium.assetSelection = assetSelection;
        }

        public AssetSelectionOverseerMono AssetSelection { get => assetSelection; }
        public AssetSelectionPicker AssetSelectionPicker { get => assetSelectionPicker; }
    }
}