using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    [CreateAssetMenu(fileName = "New Selection Menu Asset", menuName = EditorAssetPaths.AssetMenuEditor + "Asset Selection Menu")]
    public class SelectionMenuAsset : ScriptableObject
    {
        public AssetHolderBase assetObject;
        public GameObject addButton;
    }
}