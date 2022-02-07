using BoubakProductions.Core;
using Rogium.UserInterface.AssetSelection;
using Rogium.UserInterface.AssetSelection.PickerVariant;
using UnityEngine;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// Allows GAS to access MonoBehaviours.
    /// </summary>
    public class GASContainer : MonoSingleton<GASContainer>
    {
        [SerializeField] private AssetSelectionMenu assetSelection;

        protected override void Awake()
        {
            GASRogium.assetSelection = assetSelection;
        }

        public AssetSelectionMenu AssetSelection { get => assetSelection; }
    }
}