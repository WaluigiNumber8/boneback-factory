using BoubakProductions.GASCore;
using RogiumLegend.GASExtensions;
using RogiumLegend.Global.MenuSystem.UI;
using RogiumLegend.Global.MenuSystem.AssetSelection;

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
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().PackInfoHeader);
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

    }
}