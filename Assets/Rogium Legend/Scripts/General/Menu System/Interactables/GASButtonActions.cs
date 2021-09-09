using BoubakProductions.GASCore;
using RogiumLegend.GASExtensions;
using RogiumLegend.Global.MenuSystem.UI;
using RogiumLegend.Global.MenuSystem.AssetSelection;
using RogiumLegend.Editors.PackData;

namespace RogiumLegend.Global.MenuSystem.Interactables
{
    /// <summary>
    /// A Container for GAS actions when a button is clicked.
    /// </summary>
    public static class GASButtonActions
    {
        public static void OpenPackSelection()
        {
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().Background);
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            AssetSelectionOverseer.GetInstance().OpenForPacks();
        }

        public static void ReturnToMainMenu()
        {
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().PackInfoHeader);
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GASRogium.SwitchMenu(MenuType.MainMenu);
        }

        public static void OpenRoomEditor()
        {

        }

        public static void CreatePack()
        {
            LibraryOverseer.Instance.CreateAndAddPack();
            AssetSelectionOverseer.GetInstance().OpenForPacks();
        }

        public static void EditPack(int packIndex)
        {
            LibraryOverseer.Instance.ActivatePackEditor(packIndex);
            GASRogium.SwitchMenu(MenuType.AssetTypeSelection);
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().PackInfoHeader);
        }

        public static void RemovePack(int packIndex)
        {
            LibraryOverseer.Instance.RemovePack(packIndex);
            AssetSelectionOverseer.GetInstance().OpenForPacks();
        }
    }
}