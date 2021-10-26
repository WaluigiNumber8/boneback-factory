using BoubakProductions.GASCore;
using BoubakProductions.Safety;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.GASExtensions;
using Rogium.Global.UISystem.UI;
using Rogium.Editors.Packs;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Rooms;

namespace Rogium.Global.UISystem.Interactables
{
    /// <summary>
    /// A Container for GAS actions when a button is clicked.
    /// </summary>
    public static class GASButtonActions
    {
        private static int storedNumber = -1; //Used for method traveling.

        public static void GameStart()
        {
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(false, CanvasOverseer.GetInstance().ModalWindow.gameObject);
            GAS.ObjectSetActive(false, CanvasOverseer.GetInstance().NavigationBar.gameObject);
            CanvasOverseer.GetInstance().NavigationBar.Hide();
        }

        public static void PlayCampaign(int campaignIndex)
        {
            
            
            LibraryOverseer.Instance.ActivateCampaignPlaythrough(campaignIndex);
            GAS.SwitchScene(1);
        }

        public static void CreateTestCampaign()
        {
            //TODO Temporary solution for Campaign Creation. Fix when the actual campaign selection system works.
            LibraryOverseer.Instance.CreateAndAddCampaign(EditorDefaults.CampaignTitle, EditorDefaults.CampaignIcon, EditorDefaults.Author, LibraryOverseer.Instance.GetPacksCopy[0]);
        }
        
        #region Return from menus
        public static void ReturnToMainMenu()
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GASRogium.SwitchMenu(MenuType.MainMenu);
        }

        private static void ReturnToPackSelectionMenu()
        {
            EditorOverseer.Instance.CompleteEditing();
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToMainMenu);
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Pack);
        }
        
        private static void ReturnToAssetTypeSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetTypeSelection);
            PackAsset pack = EditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToPackSelectionMenu, null, pack.Title, pack.Icon);
        }
        #endregion

        #region Open Selection Menus

        public static void OpenPackSelection()
        {
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().Background);
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Pack);
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToMainMenu);
        }

        public static void OpenRoomSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Room);
            PackAsset pack = EditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenTileSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Tile);
            PackAsset pack = EditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
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
            LibraryOverseer.Instance.DeletePack(storedNumber);
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
            PackAsset pack = EditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToPackSelectionMenu, null, pack.Title, pack.Icon);
        }

        public static void OpenRoomEditor(int roomIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GASRogium.SwitchMenu(MenuType.RoomEditor);
            EditorOverseer.Instance.ActivateRoomEditor(roomIndex);
        }
        #endregion

        #region Save Editor Changes
        public static void SaveChangesRoom()
        {
            RoomEditorOverseer.Instance.CompleteEditing();
            OpenRoomSelection();
        }

        #endregion

        #region Cancel Editor Changes
        public static void CancelChangesRoom()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?","Yes","No", OpenRoomSelection, true);
        }
        #endregion
    }
}