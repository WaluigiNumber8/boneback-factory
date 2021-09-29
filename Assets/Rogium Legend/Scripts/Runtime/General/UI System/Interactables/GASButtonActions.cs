using BoubakProductions.GASCore;
using BoubakProductions.Safety;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.GASExtensions;
using Rogium.Global.UISystem.UI;
using Rogium.Global.UISystem.AssetSelection;
using Rogium.Editors.PackData;
using Rogium.Editors.Core;
using Rogium.Editors.RoomData;

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
            GASRogium.ReopenSelectionMenu(AssetType.Pack);
        }

        public static void OpenRoomSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Room);
        }
        
        public static void OpenTileSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Tile);
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
        public static void CreateTile()
        {
            new ModalWindowPropertyBuilderTile().OpenForCreate();
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

        public static void EditTileProperties(int tileIndex)
        {
            EditorOverseer.Instance.ActivateTileEditor(tileIndex);
            new ModalWindowPropertyBuilderTile().OpenForUpdate();
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
            GASRogium.ReopenSelectionMenu(AssetType.Pack);
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
            GASRogium.ReopenSelectionMenu(AssetType.Room);
            storedNumber = -1;
        }
        
        public static void RemoveTile(int tileIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedNumber = tileIndex;
            window.OpenAsMessage("Do you really want to remove this tile?", "Yes", "No", RemoveTileAccept, true);
        }
        private static void RemoveTileAccept()
        {
            SafetyNet.EnsureIntIsBiggerThan(storedNumber, 0, "StoredNumber");
            EditorOverseer.Instance.RemoveTile(storedNumber);
            GASRogium.ReopenSelectionMenu(AssetType.Tile);
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

        public static void OpenRoomEditor(int roomIndex)
        {
            EditorOverseer.Instance.ActivateRoomEditor(roomIndex);
            GASRogium.SwitchMenu(MenuType.RoomEditor);
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().PackInfoHeader);
        }
        #endregion

        #region Save Editor Changes
        public static void SaveChangesRoom()
        {
            RoomEditorOverseer.Instance.CompleteEditing();
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Room);
        }

        #endregion

        #region Cancel Editor Changes
        public static void CancelChangesRoom()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?","Yes","No", CancelChangesRoomAccept, true);
        }
        
        private static void CancelChangesRoomAccept()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Room);
        }

        #endregion
        
    }
}