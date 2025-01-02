using System.Collections;
using Rogium.Editors.AssetSelection;
using Rogium.Systems.GASExtension;

namespace Rogium.Tests.Editors.AssetSelection
{
    public static class SelectionInfoColumnTestsU
    {
        public static IEnumerator SelectPack(int packIndex = 0)
        {
            GASActions.OpenSelectionPack();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex).SetToggle(true);
            yield return null;
        }
        
        public static IEnumerator OpenPackAndSelectPalette(int packIndex = 0, int paletteIndex = 0)
        {
            GASActions.OpenSelectionPack();
            GASActions.OpenEditor(packIndex);
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(paletteIndex).SetToggle(true);
            yield return null;
        }
        
        public static IEnumerator OpenPackAndSelectSprite(int packIndex = 0, int spriteIndex = 0)
        {
            GASActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASActions.OpenSelectionSprite();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(spriteIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectWeapon(int packIndex = 0, int weaponIndex = 0)
        {
            GASActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASActions.OpenSelectionWeapon();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(weaponIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectProjectile(int packIndex = 0, int projectileIndex = 0)
        {
            GASActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASActions.OpenSelectionProjectile();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(projectileIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectEnemy(int packIndex = 0, int enemyIndex = 0)
        {
            GASActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASActions.OpenSelectionEnemy();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(enemyIndex).SetToggle(true);
            yield return null;
        }
        
        public static IEnumerator OpenPackAndSelectRoom(int packIndex = 0, int roomIndex = 0)
        {
            GASActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASActions.OpenSelectionRoom();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(roomIndex).SetToggle(true);
            yield return null;
        }

        public static IEnumerator OpenPackAndSelectTile(int packIndex = 0, int tileIndex = 0)
        {
            GASActions.OpenSelectionPack();
            ((EditableAssetCardController)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(packIndex)).Edit();
            GASActions.OpenSelectionTile();
            yield return null;
            SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(tileIndex).SetToggle(true);
            yield return null;
        }
    }
}