using System;
using RedRats.Systems.GASCore;
using RedRats.Safety;
using RedRats.UI;
using RedRats.UI.MenuSwitching;
using Rogium.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.Editors.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Inventory;
using Rogium.Systems.Toolbox;
using Rogium.UserInterface.Editors.AssetSelection;
using Rogium.UserInterface.Containers;
using Rogium.UserInterface.Core;
using Rogium.UserInterface.Editors.ModalWindowBuilding;
using Rogium.UserInterface.Gameplay.PauseMenu;
using UnityEngine;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// A Container for GAS actions when a button is clicked.
    /// </summary>
    public static class GASButtonActions
    {
        private static int storedIndex = -1; //Used for method traveling.

        public static void GameStart()
        {
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundGameplayMenus);
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(false, CanvasOverseer.GetInstance().ModalWindow.gameObject);
            GAS.ObjectSetActive(false, CanvasOverseer.GetInstance().NavigationBar.transform.GetChild(0).gameObject);
            GAS.ObjectSetActive(false, CanvasOverseer.GetInstance().PickerWindow.transform.GetChild(0).gameObject);
            CanvasOverseer.GetInstance().NavigationBar.Hide();
        }

        public static void GameQuit()
        {
            Application.Quit();
        }
        
        public static void PlayCampaign(int campaignIndex)
        {
            ExternalLibraryOverseer.Instance.ActivateCampaignPlaythrough(campaignIndex);
            GAS.SwitchScene(1);
        }

        public static void OpenOptionsMenu()
        {
            GAS.SwitchMenu(MenuType.OptionsMenu);
        }

        public static void ReturnToMainMenuOptions()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Return to Main Menu without confirming changes?", ThemeType.Blue,"Yes","No", ReturnToMainMenuOptionsConfirm, true);
        }
        
        private static void ReturnToMainMenuOptionsConfirm()
        {
            GAS.SwitchMenu(MenuType.MainMenu);
        }
        
        #region Return from menus
        public static void ReturnToMainMenuSelection()
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.ObjectSetActive(false, UIEditorContainer.GetInstance().Background);
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundGameplayMenus);
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundMain);
            GAS.SwitchMenu(MenuType.MainMenu);
            GASRogium.ChangeTheme(ThemeType.Blue);
        }

        private static void ReturnToPackSelectionMenu()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            PackEditorOverseer.Instance.CompleteEditing();
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToMainMenuSelection);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Pack);
        }
        
        private static void ReturnToAssetTypeSelection()
        {
            GAS.SwitchMenu(MenuType.AssetTypeSelection);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToPackSelectionMenu, null, pack.Title, pack.Icon);
        }
        #endregion

        #region Open Selection Menus
        public static void OpenSelectionPack()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(true, UIEditorContainer.GetInstance().Background);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Pack);
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToMainMenuSelection);
        }

        public static void OpenSelectionCampaign()
        {
            GASRogium.ChangeTheme(ThemeType.Red);
            GAS.ObjectSetActive(false, UIMainContainer.GetInstance().BackgroundMain);
            GAS.ObjectSetActive(true, UIMainContainer.GetInstance().BackgroundGameplayMenus);
            GAS.SwitchMenu(MenuType.CampaignSelection);
            CampaignAssetSelectionOverseer.Instance.SelectCampaignFirst();
            GASRogium.ChangeTheme(ThemeType.Red);
        }
        
        public static void OpenSelectionPalette()
        {
            GASRogium.ChangeTheme(ThemeType.Purple);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Palette);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenSelectionSprite()
        {
            GASRogium.ChangeTheme(ThemeType.Pink);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Sprite);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenSelectionWeapon()
        {
            GASRogium.ChangeTheme(ThemeType.Green);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Weapon);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenSelectionProjectile()
        {
            GASRogium.ChangeTheme(ThemeType.Teal);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Projectile);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenSelectionEnemy()
        {
            GASRogium.ChangeTheme(ThemeType.Red);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Enemy);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenSelectionRoom()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Room);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToAssetTypeSelection, null, pack.Title, pack.Icon);
        }
        
        public static void OpenSelectionTile()
        {
            GASRogium.ChangeTheme(ThemeType.Yellow);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Tile);
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

        public static void CreatePalette()
        {
            new ModalWindowPropertyBuilderPalette().OpenForCreate();
        }

        public static void CreateSprite()
        {
            new ModalWindowPropertyBuilderSprite().OpenForCreate();
        }
        
        public static void CreateWeapon()
        {
            new ModalWindowPropertyBuilderWeapon().OpenForCreate();
        }
        
        public static void CreateProjectile()
        {
            new ModalWindowPropertyBuilderProjectile().OpenForCreate();
        }
        
        public static void CreateEnemy()
        {
            new ModalWindowPropertyBuilderEnemy().OpenForCreate();
        }
        
        public static void CreateTile()
        {
            new ModalWindowPropertyBuilderTile().OpenForCreate();
        }
        
        public static void CreateRoom()
        {
            new ModalWindowPropertyBuilderRoom().OpenForCreate();
        }
        #endregion

        #region Edit Asset Properties
        public static void EditPropertiesPack(int packIndex)
        {
            ExternalLibraryOverseer.Instance.ActivatePackEditor(packIndex);
            new ModalWindowPropertyBuilderPack().OpenForUpdate();
        }

        public static void EditPropertiesCampaign(int campaignIndex)
        {
            ExternalLibraryOverseer.Instance.ActivateCampaignEditor(campaignIndex, false);
            new ModalWindowPropertyBuilderCampaign().OpenForUpdate();
        }
        
        public static void EditPropertiesPalette(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivatePaletteEditor(assetIndex, false);
            new ModalWindowPropertyBuilderPalette().OpenForUpdate();
        }
        
        public static void EditPropertiesSprite(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateSpriteEditor(assetIndex, false);
            new ModalWindowPropertyBuilderSprite().OpenForUpdate();
        }
        
        public static void EditPropertiesWeapon(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateWeaponEditor(assetIndex, false);
            new ModalWindowPropertyBuilderWeapon().OpenForUpdate();
        }
        
        public static void EditPropertiesProjectile(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateProjectileEditor(assetIndex, false);
            new ModalWindowPropertyBuilderProjectile().OpenForUpdate();
        }
        
        public static void EditPropertiesEnemy(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateEnemyEditor(assetIndex, false);
            new ModalWindowPropertyBuilderEnemy().OpenForUpdate();
        }
        
        public static void EditPropertiesRoom(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateRoomEditor(assetIndex, false);
            new ModalWindowPropertyBuilderRoom().OpenForUpdate();
        }
        
        public static void EditPropertiesTile(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateTileEditor(assetIndex, false);
            new ModalWindowPropertyBuilderTile().OpenForUpdate();
        }
        #endregion

        #region Remove Assets
        public static void DeletePack(int packIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = packIndex;
            window.OpenAsMessage("Do you really want to remove this pack?", ThemeType.Blue,"Yes","No", RemovePackAccept, true);
        }
        private static void RemovePackAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            ExternalLibraryOverseer.Instance.DeletePack(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Pack);
            storedIndex = -1;
        }
        
        public static void DeleteCampaign(int campaignIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = campaignIndex;
            window.OpenAsMessage("Do you really want to remove this campaign?", ThemeType.Red,"Yes","No", RemoveCampaignAccept, true);
        }
        
        private static void RemoveCampaignAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            ExternalLibraryOverseer.Instance.DeleteCampaign(storedIndex);
            CampaignShowPrevious();
            storedIndex = -1;
        }

        public static void DeletePalette(int assetIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = assetIndex;
            window.OpenAsMessage("Do you really want to remove this palette?", ThemeType.Purple, "Yes", "No", RemovePaletteAccept, true);
        }
        private static void RemovePaletteAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemovePalette(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Palette);
            storedIndex = -1;
        }
        
        public static void DeleteSprite(int assetIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = assetIndex;
            window.OpenAsMessage("Do you really want to remove this sprite?", ThemeType.Blue, "Yes", "No", RemoveSpriteAccept, true);
        }
        private static void RemoveSpriteAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveSprite(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Sprite);
            storedIndex = -1;
        }
        
        public static void DeleteWeapon(int assetIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = assetIndex;
            window.OpenAsMessage("Do you really want to remove this weapon?", ThemeType.Green, "Yes", "No", RemoveWeaponAccept, true);
        }
        private static void RemoveWeaponAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveWeapon(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Weapon);
            storedIndex = -1;
        }
        
        public static void DeleteProjectile(int assetIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = assetIndex;
            window.OpenAsMessage("Do you really want to remove this projectile?", ThemeType.Teal, "Yes", "No", RemoveProjectileAccept, true);
        }
        private static void RemoveProjectileAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveProjectile(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Projectile);
            storedIndex = -1;
        }
        
        public static void DeleteEnemy(int assetIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = assetIndex;
            window.OpenAsMessage("Do you really want to remove this sprite?", ThemeType.Red, "Yes", "No", RemoveEnemyAccept, true);
        }
        private static void RemoveEnemyAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveEnemy(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Enemy);
            storedIndex = -1;
        }
        
        public static void DeleteRoom(int assetIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = assetIndex;
            window.OpenAsMessage("Do you really want to remove this room?", ThemeType.Blue, "Yes", "No", RemoveRoomAccept, true);
        }
        private static void RemoveRoomAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            PackEditorOverseer.Instance.RemoveRoom(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Room);
            storedIndex = -1;
        }
        
        public static void DeleteTile(int assetIndex)
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            storedIndex = assetIndex;
            window.OpenAsMessage("Do you really want to remove this tile?", ThemeType.Yellow, "Yes", "No", RemoveTileAccept, true);
        }
        private static void RemoveTileAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            PackEditorOverseer.Instance.RemoveTile(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Tile);
            storedIndex = -1;
        }
        #endregion

        #region Open Editors
        public static void OpenEditor(int packIndex)
        {
            ExternalLibraryOverseer.Instance.ActivatePackEditor(packIndex);
            GAS.SwitchMenu(MenuType.AssetTypeSelection);
            PackAsset pack = PackEditorOverseer.Instance.CurrentPack;
            CanvasOverseer.GetInstance().NavigationBar.Show(ReturnToPackSelectionMenu, null, pack.Title, pack.Icon);
        }

        public static void OpenEditorCampaign(int assetIndex)
        {
            GAS.SwitchMenu(MenuType.CampaignEditor);
            ExternalLibraryOverseer.Instance.ActivateCampaignEditor(assetIndex);
            CampaignEditorOverseerMono.GetInstance().FillMenu();
        }

        public static void OpenEditorPalette(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PaletteEditor);
            PackEditorOverseer.Instance.ActivatePaletteEditor(assetIndex);
        }
        
        public static void OpenEditorSprite(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.SpriteEditor);
            PackEditorOverseer.Instance.ActivateSpriteEditor(assetIndex);
        }
        
        public static void OpenEditorWeapon(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateWeaponEditor(assetIndex);
        }
        
        public static void OpenEditorProjectile(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateProjectileEditor(assetIndex);
        }
        
        public static void OpenEditorEnemy(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateEnemyEditor(assetIndex);
        }
        
        public static void OpenEditorRoom(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.RoomEditor);
            PackEditorOverseer.Instance.ActivateRoomEditor(assetIndex);
        }

        public static void OpenEditorTile(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GASRogium.ChangeTheme(ThemeType.Yellow);
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateTileEditor(assetIndex);
        }
        #endregion

        #region Save Editor Changes
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

        public static void SaveChangesPalette()
        {
            PaletteEditorOverseer.Instance.CompleteEditing();
            OpenSelectionPalette();
        }
        
        public static void SaveChangesSprite()
        {
            SpriteEditorOverseer.Instance.CompleteEditing();
            OpenSelectionSprite();
        }
        
        public static void SaveChangesWeapon()
        {
            WeaponEditorOverseer.Instance.CompleteEditing();
            OpenSelectionWeapon();
        }
        
        public static void SaveChangesProjectile()
        {
            ProjectileEditorOverseer.Instance.CompleteEditing();
            OpenSelectionProjectile();
        }
        
        public static void SaveChangesEnemy()
        {
            EnemyEditorOverseer.Instance.CompleteEditing();
            OpenSelectionEnemy();
        }
        
        public static void SaveChangesRoom()
        {
            RoomEditorOverseer.Instance.CompleteEditing();
            OpenSelectionRoom();
        }

        public static void SaveChangesTile()
        {
            TileEditorOverseer.Instance.CompleteEditing();
            OpenSelectionTile();
        }
        #endregion

        #region Cancel Editor Changes
        public static void CancelChangesCampaign()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Red,"Yes","No", CancelChangesCampaignConfirm, true);
        }
        
        private static void CancelChangesCampaignConfirm()
        {
            GAS.SwitchMenu(MenuType.CampaignSelection);
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
        }
        
        public static void CancelChangesPalette()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Blue,"Yes","No", OpenSelectionPalette, true);
        }
        
        public static void CancelChangesSprite()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Pink,"Yes","No", OpenSelectionSprite, true);
        }
        
        public static void CancelChangesWeapon()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Green,"Yes","No", OpenSelectionWeapon, true);
        }
        
        public static void CancelChangesProjectile()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Teal,"Yes","No", OpenSelectionProjectile, true);
        }
        
        public static void CancelChangesEnemy()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Red,"Yes","No", OpenSelectionEnemy, true);
        }
        
        public static void CancelChangesRoom()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Blue,"Yes","No", OpenSelectionRoom, true);
        }

        public static void CancelChangesTile()
        {
            ModalWindow window = CanvasOverseer.GetInstance().ModalWindow;
            window.OpenAsMessage("Do you really not want to save changes?", ThemeType.Yellow,"Yes","No", OpenSelectionTile, true);
        }
        #endregion

        #region Campaign Editor Menu
        public static void ChangeImportStatus(int assetIndex)
        {
            CampaignEditorOverseerMono.GetInstance().ChangeSelectStatus(assetIndex);
        }
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

        #region Sprite Editor
        public static void SpriteSwitchTool(int index)
        {
            SpriteEditorOverseerMono.GetInstance().Toolbox.SwitchTool((ToolType) index);
        }

        public static void SpriteSwitchPalette()
        {
            SpriteEditorOverseerMono.GetInstance().SwitchPaletteViaWindow();
        }

        public static void SpriteClearActiveLayer()
        {
            SpriteEditorOverseerMono.GetInstance().ClearActiveGrid();
        }
        #endregion

        #region Room Editor

        public static void RoomSwitchTool(int index)
        {
            RoomEditorOverseerMono.GetInstance().Toolbox.SwitchTool((ToolType) index);
        }

        public static void RoomSwitchPalette(int index)
        {
            RoomEditorOverseerMono.GetInstance().SwitchLayer(index);
        }

        public static void RoomClearActiveLayer()
        {
            RoomEditorOverseerMono.GetInstance().ClearActiveLayer();
        }
        #endregion

        #region Gameplay Menu

        public static void GameplayPauseResume()
        {
            PauseMenuOverseerMono.GetInstance().SwitchMenuState();
        }

        public static void GameplayPauseQuit()
        {
            PauseMenuOverseerMono.GetInstance().ReturnToMainMenu();
        }

        public static void GameplaySelectWeapon(int index)
        {
            WeaponSelectMenu.GetInstance().Select(index);
        }
        #endregion
        
    }
}