using System.Collections;
using Rogium.Core;
using Rogium.Tests.Core;
using Rogium.UserInterface.Editors.AssetSelection.PickerVariant;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;

namespace Rogium.Tests.Editors.AssetSelection
{
    public static class AssetPickingTestsU
    {
        public static IEnumerator BuildAssetField()
        {
            UIPropertyBuilder.GetInstance().BuildAssetField("Test", AssetType.Pack, null, Object.FindFirstObjectByType<Canvas>().transform, null);
            yield return null;
        }
        
        public static IEnumerator ClickAssetFieldToOpenAssetPickerWindow()
        {
            Object.FindFirstObjectByType<AssetField>().OnPointerClick(PointerDataCreator.LeftClick());
            yield return null;
        }

        public static IEnumerator PickFirstAssetAndConfirm()
        {
            Object.FindFirstObjectByType<AssetPickerCardController>().SetToggle(true);
            yield return null;
            Object.FindFirstObjectByType<AssetPickerWindow>().ConfirmSelection();
            yield return null;
        }
    }
}