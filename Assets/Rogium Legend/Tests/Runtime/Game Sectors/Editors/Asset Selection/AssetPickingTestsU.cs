using System.Collections;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Editors.Packs;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;

namespace Rogium.Tests.Editors.AssetSelection
{
    public static class AssetPickingTestsU
    {
        public static IEnumerator BuildAssetField(PackAsset value = null)
        {
            UIPropertyBuilder.GetInstance().BuildAssetField("Test", AssetType.Pack, value, Object.FindFirstObjectByType<Canvas>().transform, null);
            yield return null;
        }
        
        public static IEnumerator ClickAssetFieldToOpenAssetPickerWindow()
        {
            Object.FindFirstObjectByType<AssetField>().OnPointerClick(PointerDataCreator.LeftClick());
            yield return null;
        }

        public static IEnumerator PickAssetAndConfirm(int childIndex = 0)
        {
            AssetPickerWindow window = Object.FindFirstObjectByType<AssetPickerWindow>();
            AssetCardController card = window.SelectorContent.GetChild(childIndex).GetComponent<AssetCardController>();
            card.SetToggle(true);
            yield return null;
            window.ConfirmSelection();
            yield return null;
        }
    }
}