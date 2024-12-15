using System.Collections;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Systems.GASExtension;
using UnityEngine;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Methods for navigating the menu. Used in tests.
    /// </summary>
    public static class TUtilsMenuNavigation
    {
        public static IEnumerator OpenEditor(AssetType assetType, int cardIndex = 0)
        {
            yield return OpenSelectionMenu(assetType, cardIndex);
            yield return null;
            Object.FindFirstObjectByType<EditableAssetCardController>().Edit();
            yield return null;
        }

        public static IEnumerator OpenSelectionMenu(AssetType assetType, int cardIndex)
        {
            //TODO: Add Open campaign selection once needed
            yield return OpenSelectionMenuEditor(assetType, cardIndex);
        }
        
        private static IEnumerator OpenSelectionMenuEditor(AssetType assetType, int cardIndex)
        {
            GASActions.OpenSelectionPack();
            yield return null;
            if (assetType == AssetType.Pack) yield break;
            Object.FindObjectsByType<EditableAssetCardController>(FindObjectsSortMode.None)[cardIndex].Edit();
            SelectionMenuOverseerMono.GetInstance().Open(assetType);
            yield return null;
        }
    }
}