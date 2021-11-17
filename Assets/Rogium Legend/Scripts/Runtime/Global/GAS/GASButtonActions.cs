using BoubakProductions.GASCore;
using BoubakProductions.Safety;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Campaign;
using Rogium.Global.UISystem.UI;
using Rogium.Editors.Packs;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using Rogium.Global.UISystem;
using Rogium.Global.UISystem.AssetSelection;

namespace Rogium.Global.GASExtension
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
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundGameplayMenus);
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

        #region Return from menus
        public static void ReturnToMainMenu()
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundGameplayMenus);
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GASRogium.SwitchMenu(MenuType.MainMenu);
            GASRogium.ChangeTheme(ThemeSystem.ThemeType.Blue);
        }

        private static void ReturnToPackSelectionMenu()
        {
            PackEditorOverseer.Instance.CompleteEditing();
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToMainMenu);
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Pack);
        }
        
        private static void ReturnToAssetTypeSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetTypeSelection);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
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

        public static void OpenCampaignSelection()
        {
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundGameplayMenus);
            GASRogium.SwitchMenu(MenuType.CampaignSelection);
            CampaignAssetSelectionOverseer.Instance.SelectCampaignFirst();
            GASRogium.ChangeTheme(ThemeSystem.ThemeType.Red);
        }
        
        public static void OpenRoomSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Room);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenTileSelection()
        {
            GASRogium.SwitchMenu(MenuType.AssetSelection);
            GASRogium.ReopenSelectionMenu(AssetType.Tile);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }

        #endregion

        #region Create Assets
        public static void CreatePack()
        {
            new ModalWindowPropertyBuilderPack().OpenForCreate();
        }
        
        public static void CreateCampaign()
        {
            new ModalWindowPropertyBuilderCampaign().OpenForCreate();
        }

        public static void CreateRoom()
        {
            new ModalWindowPropertyBuilderRoom().OpenForCreate();
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

        public static void EditCampaignProperties(int campaignIndex)
        {
            LibraryOverseer.Instance.ActivateCampaignEditor(campaignIndex);
            new ModalWindowPropertyBuilderCampaign().OpenForUpdate();
        }
        
        public static void EditRoomProperties(int roomIndex)
        {
            PackEditorOverseer.Instance.ActivateRoomEditor(roomIndex);
            new ModalWindowPropertyBuilderRoom().OpenForUpdate();
        }

        public static void EditTileProperties(int tileIndex)
        {
            PackEditorOverseer.Instance.ActivateTileEditor(tileIndex);
            new ModalWindowPropertyBuilderTile().OpenForUpdate();
        }
        #endregion

        #region Remove Assets
        public static void RemovePack(int packIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedNumber = packIndex;
            window.OpenAsMessage("Do you really want to remove this pack?", ThemeType.Blue,"Yes","No", RemovePackAccept, true);
        }
        private static void RemovePackAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedNumber, 0, "StoredNumber");
            LibraryOverseer.Instance.DeletePack(storedNumber);
            GASRogium.ReopenSelectionMenu(AssetType.Pack);
            storedNumber = -1;
        }
        
        public static void RemoveCampaign(int campaignIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedNumber = campaignIndex;
            window.OpenAsMessage("Do you really want to remove this campaign?", ThemeType.Red,"Yes","No", RemoveCampaignAccept, true);
        }
        
        private static void RemoveCampaignAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedNumber, 0, "StoredNumber");
            LibraryOverseer.Instance.DeleteCampaign(storedNumber);
            CampaignShowPrevious();
            storedNumber = -1;
        }

        public static void RemoveRoom(int roomIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedNumber = roomIndex;
            window.OpenAsMessage("Do you really want to remove this room?", ThemeType.Blue, "Yes", "No", RemoveRoomAccept, true);
        }
        private static void RemoveRoomAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedNumber, 0, "StoredNumber");
            PackEditorOverseer.Instance.RemoveRoom(storedNumber);
            GASRogium.ReopenSelectionMenu(AssetType.Room);
            storedNumber = -1;
        }
        
        public static void RemoveTile(int tileIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedNumber = tileIndex;
            window.OpenAsMessage("Do you really want to remove this tile?", ThemeType.Blue, "Yes", "No", RemoveTileAccept, true);
        }
        private static void RemoveTileAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedNumber, 0, "StoredNumber");
            PackEditorOverseer.Instance.RemoveTile(storedNumber);
            GASRogium.ReopenSelectionMenu(AssetType.Tile);
            storedNumber = -1;
        }

        
        #endregion

        #region Open Editors
        public static void OpenEditor(int packIndex)
        {
            LibraryOverseer.Instance.ActivatePackEditor(packIndex);
            GASRogium.SwitchMenu(MenuType.AssetTypeSelection);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToPackSelectionMenu, null, pack.Title, pack.Icon);
        }

        public static void OpenCampaignEditor(int campaignIndex)
        {
            GASRogium.SwitchMenu(MenuType.CampaignEditor);
            LibraryOverseer.Instance.ActivateCampaignEditor(campaignIndex);
            CampaignEditorOverseerMono.GetInstance().FillMenu();
        }
        
        public static void OpenRoomEditor(int roomIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GASRogium.SwitchMenu(MenuType.RoomEditor);
            PackEditorOverseer.Instance.ActivateRoomEditor(roomIndex);
        }
        #endregion

        #region Save Editor Changes
        public static void SaveChangesRoom()
        {
            RoomEditorOverseer.Instance.CompleteEditing();
            OpenRoomSelection();
        }

        public static void SaveChangesCampaign()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            //TODO Make sure you cannot combine a campaign, when no assets are selected.
            window.OpenAsMessage("Combine selected packs into a Campaign? Changes made to the packs after this combination will not effect this campaign.", ThemeType.Red,"Combine","Cancel", SaveChangesCampaignConfirm, true);
        }

        private static void SaveChangesCampaignConfirm()
        {
            CampaignEditorOverseerMono.GetInstance().CompleteSelection();
            CampaignEditorOverseer.Instance.CompleteEditing();
            CancelChangesCampaignConfirm();
        }

        #endregion

        #region Cancel Editor Changes
        public static void CancelChangesRoom()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Blue,"Yes","No", OpenRoomSelection, true);
        }

        public static void CancelChangesCampaign()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Red,"Yes","No", CancelChangesCampaignConfirm, true);
        }

        private static void CancelChangesCampaignConfirm()
        {
            GASRogium.SwitchMenu(MenuType.CampaignSelection);
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
        }
        #endregion

        #region Campaign Editor Menu
        public static void CampaignEditorSelectAll()
        {
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.WhenAssetSelectAll();
        }
        
        public static void CampaignEditorSelectNone()
        {
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.WhenAssetDeselectAll();
        }
        
        public static void CampaignEditorSelectRandom()
        {
            
        }
        #endregion
        
        #region Campaign Selection Menu

        public static void CampaignShowNext()
        {
            CampaignAssetSelectionOverseer.Instance.SelectCampaignNext();
        }
        
        public static void CampaignShowPrevious()
        {
            CampaignAssetSelectionOverseer.Instance.SelectCampaignPrevious();
        }
        

        #endregion
        
    }
}