using System.Collections;
using Rogium.Editors.NewAssetSelection;
using Rogium.Systems.GASExtension;
using UnityEngine;

namespace Rogium.Tests.Editors.AssetSelection
{
    public static class SelectionInfoColumnTestsU
    {
        public static IEnumerator OpenPackAndSelectRoom(int packIndex = 0, int roomIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            yield return new WaitForSeconds(1);
            ((EditableAssetCardControllerV2)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            yield return new WaitForSeconds(1);
            GASButtonActions.OpenSelectionRoom();
            yield return new WaitForSeconds(1);
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(roomIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectPalette()
        {
            GASButtonActions.OpenSelectionPack();
            yield return new WaitForSeconds(1);
            GASButtonActions.OpenEditor(0);
            yield return new WaitForSeconds(1);
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(0).SetToggle(true);
            yield return null;
        }
    }
}