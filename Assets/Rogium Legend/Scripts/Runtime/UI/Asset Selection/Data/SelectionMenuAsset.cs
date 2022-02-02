using UnityEngine;

namespace Rogium.UserInterface.AssetSelection
{
    [CreateAssetMenu(fileName = "New Selection Menu Asset", menuName = "Rogium Legend/Asset Selection Menu")]
    public class SelectionMenuAsset : ScriptableObject
    {
        public AssetHolderBase assetObject;
        public GameObject addButton;
    }
}