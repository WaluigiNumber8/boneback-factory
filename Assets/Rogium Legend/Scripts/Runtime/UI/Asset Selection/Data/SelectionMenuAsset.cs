using UnityEngine;

namespace Rogium.UserInterface.AssetSelection
{
    [CreateAssetMenu(fileName = "New Selection Menu Asset", menuName = "Rogium Legend/Asset Selection Menu")]
    public class SelectionMenuAsset : ScriptableObject
    {
        public GameObject assetObject;
        public GameObject addButton;
    }
}