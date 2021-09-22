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

        public static void ReturnToMainMenu()
        {
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().PackInfoHeader);
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GASRogium.SwitchMenu(MenuType.MainMenu);
        }

        #region Open Selection Menus

        public static void OpenPackSelection()
        {
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().Background);
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            AssetSelectionOverseer.GetInstance().ReopenForPacks();
        }

        public static void OpenRoomSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            AssetSelectionOverseer.GetInstance().ReopenForRooms();
        }

        #endregion

        #region Create Assets
        public static void CreatePack()
        {
            new ModalWindowPropertyBuilderPack().OpenForCreate();
        }

        public static void CreateRoom()
        {
            new ModalWindowPropertyBuilderRooms().OpenForCreate();
        }

        #endregion

        #region Edit Asset Properties
        public static void EditPackProperties(int packIndex)
        {
            LibraryOverseer.Instance.ActivatePackEditor(packIndex);
            new ModalWindowPropertyBuilderPack().OpenForUpdate();
        }

        public static void EditRoomProperties(int roomIndex)
        {
            EditorOverseer.Instance.ActivateRoomEditor(roomIndex);
            new ModalWindowPropertyBuilderRooms().OpenForUpdate();
        }

        #endregion

        #region Remove Assets
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
            AssetSelectionOverseer.GetInstance().ReopenForPacks();
            storedNumber = -1;
        }

        public static void RemoveRoom(int roomIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedNumber = roomIndex;
            window.OpenAsMessage("Do you really want to remove this room?", "Yes", "No", RemoveRoomAccept, true);
        }
        private static void RemoveRoomAccept()
        {
            SafetyNet.EnsureIntIsBiggerThan(storedNumber, 0, "StoredNumber");
            EditorOverseer.Instance.RemoveRoom(storedNumber);
            AssetSelectionOverseer.GetInstance().ReopenForRooms();
            storedNumber = -1;
        }

        #endregion

        #region Open Editors
        public static void OpenEditor(int packIndex)
        {
            LibraryOverseer.Instance.ActivatePackEditor(packIndex);
            GASRogium.SwitchMenu(MenuType.AssetTypeSelection);
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().PackInfoHeader);
        }

        #endregion
    }
}