using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.UserInterface.AssetSelection
{
    [CreateAssetMenu(fileName = "New Selection Menu Asset", menuName = EditorDefaults.AssetMenuEditor + "Asset Selection Menu")]
    public class SelectionMenuAsset : ScriptableObject
    {
        public AssetHolderBase assetObject;
        public GameObject addButton;
    }
}