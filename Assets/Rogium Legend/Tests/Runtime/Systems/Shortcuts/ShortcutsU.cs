using System.Collections;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Systems.GASExtension;
using UnityEngine;

namespace Rogium.Tests.Systems.Shortcuts
{
    public static class ShortcutsU
    {
        public static IEnumerator OpenEditor(AssetType assetType)
        {
            GASButtonActions.OpenSelectionPack();
            yield return null;
            Object.FindFirstObjectByType<EditableAssetCardController>().Edit();
            SelectionMenuOverseerMono.GetInstance().Open(assetType);
            yield return null;
            Object.FindFirstObjectByType<EditableAssetCardController>().Edit();
            yield return null;
        }
    }
}