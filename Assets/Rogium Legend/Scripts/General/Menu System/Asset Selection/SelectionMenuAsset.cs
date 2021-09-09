using UnityEngine;

namespace RogiumLegend.Global.MenuSystem.AssetSelection
{
    [CreateAssetMenu(fileName = "New Selection Menu Asset", menuName = "Rogium Legend/Asset Selection Menu")]
    public class SelectionMenuAsset : ScriptableObject
    {
        public GameObject assetObject;
        public GameObject addButton;
    }
}