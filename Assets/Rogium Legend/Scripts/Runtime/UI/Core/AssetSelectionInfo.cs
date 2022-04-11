using UnityEngine;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    [System.Serializable]
    public struct AssetSelectionMenuInfo
    {
        public SelectionMenuAsset pack;
        public SelectionMenuAsset palette;
        public SelectionMenuAsset sprite;
        public SelectionMenuAsset weapon;
        public SelectionMenuAsset projectile;
        public SelectionMenuAsset enemy;
        public SelectionMenuAsset room;
        public SelectionMenuAsset tile;
        [Space] 
        public SelectionMenuAsset interactableObject;
        public SelectionMenuAsset sound;
    }
}