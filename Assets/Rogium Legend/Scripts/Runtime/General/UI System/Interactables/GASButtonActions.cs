using BoubakProductions.GASCore;
using BoubakProductions.Safety;
using BoubakProductions.UI;
using Rogium.GASExtensions;
using Rogium.Global.UISystem.UI;
using Rogium.Global.UISystem.AssetSelection;
using Rogium.Editors.PackData;
using Rogium.Editors.Core;

namespace Rogium.Global.UISystem.Interactables
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
            new ModalPropertyWindowBuilder().OpenPackPropertiesCreate();
        }

        public static void EditPack(int packIndex)
        {
            LibraryOverseer.Instance.ActivatePackEditor(packIndex);
            GASRogium.SwitchMenu(MenuType.AssetTypeSelection);
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().PackInfoHeader);
        }

        public static void EditPackProperties(int packIndex)
        {
            LibraryOverseer.Instance.ActivatePackEditor(packIndex);
            new ModalPropertyWindowBuilder().OpenPackPropertiesEdit();
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