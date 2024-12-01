using System.Collections;
using Rogium.Editors.NewAssetSelection;
using Rogium.Systems.GASExtension;
using UnityEngine;

namespace Rogium.Tests.Editors.AssetSelection
{
    public static class SelectionInfoColumnTestsU
    {
        public static IEnumerator SelectPack(int packIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex).SetToggle(true);
            yield return null;
        }
        
        public static IEnumerator OpenPackAndSelectPalette(int packIndex = 0, int paletteIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            GASButtonActions.OpenEditor(packIndex);
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(paletteIndex).SetToggle(true);
            yield return null;
        }
        
        public static IEnumerator OpenPackAndSelectSprite(int packIndex = 0, int spriteIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASButtonActions.OpenSelectionSprite();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(spriteIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectWeapon(int packIndex = 0, int weaponIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASButtonActions.OpenSelectionWeapon();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(weaponIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectProjectile(int packIndex = 0, int projectileIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASButtonActions.OpenSelectionProjectile();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(projectileIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectEnemy(int packIndex = 0, int enemyIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASButtonActions.OpenSelectionEnemy();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(enemyIndex).SetToggle(true);
            yield return null;
        }
        
        public static IEnumerator OpenPackAndSelectRoom(int packIndex = 0, int roomIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASButtonActions.OpenSelectionRoom();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(roomIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectTile(int packIndex = 0, int tileIndex = 0)
        {
            GASButtonActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASButtonActions.OpenSelectionTile();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(tileIndex).SetToggle(true);
            yield return null;
        }
    }
}