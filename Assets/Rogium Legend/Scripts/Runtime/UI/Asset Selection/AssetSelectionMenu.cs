using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    public class AssetSelectionMenu : MonoBehaviour, IAssetSelectionOverseer
    {
        [SerializeField] private AssetSelectionOverseerMono assetSelection;
        [SerializeField] private ToggleGroup toggleGroup;

        #region Open Selection

        public void OpenForPacks()
        {
            Open();
            assetSelection.OpenForPacks();
        }
        public void OpenForPalettes()
        {
            Open();
            assetSelection.OpenForPalettes();
        }

        public void OpenForSprites()
        {
            Open();
            assetSelection.OpenForSprites();
        }

        public void OpenForWeapons()
        {
            Open();
            assetSelection.OpenForWeapons();
        }

        public void OpenForProjectiles()
        {
            Open();
            assetSelection.OpenForProjectiles();
        }

        public void OpenForEnemies()
        {
            Open();
            assetSelection.OpenForEnemies();
        }

        public void OpenForRooms()
        {
            Open();
            assetSelection.OpenForRooms();
        }

        public void OpenForTiles()
        {
            Open();
            assetSelection.OpenForTiles();
        }

        #endregion

        /// <summary>
        /// Prepares the menu for filling.
        /// </summary>
        private void Open() => assetSelection.BeginListeningToSpawnedCards(AssignGroup);

        /// <summary>
        /// Assign the ToggleGroup to a card.
        /// </summary>
        /// <param name="holder">The card to assign to.</param>
        private void AssignGroup(AssetHolderBase holder)
        {
            AssetCardController card = (AssetCardController) holder;
            card.RegisterToggleGroup(toggleGroup);
        }
        
    }
}