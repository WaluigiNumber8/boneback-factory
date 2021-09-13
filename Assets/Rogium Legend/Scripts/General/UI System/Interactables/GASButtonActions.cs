using BoubakProductions.GASCore;
using RogiumLegend.GASExtensions;
using RogiumLegend.Global.UISystem.UI;
using RogiumLegend.Global.UISystem.AssetSelection;
using RogiumLegend.Editors.PackData;
using BoubakProductions.UI;
using BoubakProductions.Safety;
using RogiumLegend.Editors.Core;

namespace RogiumLegend.Global.UISystem.Interactables
{
    /// <summary>
    /// A Container for GAS actions when a button is clicked.
    /// </summary>
    public static class GASButtonActions
    {
        private static int storedNumber = -1; //Used for method traveling.

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
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedNumber = packIndex;
            window.OpenAsMessage("Do you really want to remove this pack?","Yes","No", RemovePackAccept, true);
        }
        private static void RemovePackAccept()
        {
            SafetyNet.EnsureIntIsBiggerThan(storedNumber, 0, "StoredNumber");
            LibraryOverseer.Instance.RemovePack(storedNumber);
            AssetSelectionOverseer.GetInstance().OpenForPacks();
            storedNumber = -1;
        }

        public static void CreateRoom()
        {
            EditorOverseer.Instance.CreateNewRoom();
            AssetSelectionOverseer.GetInstance().OpenForPacks();
        }

    }
}