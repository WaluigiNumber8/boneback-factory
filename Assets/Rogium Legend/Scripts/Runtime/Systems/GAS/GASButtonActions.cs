using System.Collections.Generic;
using RedRats.Systems.GASCore;
using RedRats.Safety;
using RedRats.Systems.Themes;
using RedRats.UI.MenuSwitching;
using RedRats.UI.ModalWindows;
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
using Rogium.Options.Core;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.Toolbox;
using Rogium.UserInterface.Editors.AssetSelection;
using Rogium.UserInterface.Containers;
using Rogium.UserInterface.Core;
using Rogium.UserInterface.Editors.ModalWindowBuilding;
using Rogium.UserInterface.Gameplay.PauseMenu;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// A Container for GAS actions when a button is clicked.
    /// </summary>
    public static class GASButtonActions
    {
        private static int storedIndex = -1; //Used for method traveling.

        public static void GameQuit()
        {
            Application.Quit();
        }
        
        public static void PlayCampaign(int campaignIndex)
        {
            ExternalLibraryOverseer.Instance.ActivateCampaignPlaythrough(campaignIndex);
            GAS.SwitchScene(1);
        }

        public static void OpenChangelog()
        {
            GAS.SwitchMenu(MenuType.Changelog);
        }

        #region General Editor Actions

        public static void UndoLastAction()
        {
            ActionHistorySystem.UndoLastAction();
        }
        
        public static void RedoLastAction()
        {
            ActionHistorySystem.RedoLastAction();
        }

        #endregion
        
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

        public static void ReturnToMainMenuChangelog()
        {
            GAS.SwitchMenu(MenuType.MainMenu);
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
            storedIndex = packIndex;
            MessageWindowInfo data = new("Do you really want to remove this pack?","Yes","No", DeletePackAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeletePackAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            ExternalLibraryOverseer.Instance.DeletePack(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Pack);
            storedIndex = -1;
        }
        
        public static void DeleteCampaign(int campaignIndex)
        {
            storedIndex = campaignIndex;
            MessageWindowInfo data = new("Do you really want to remove this campaign?","Yes","No", DeleteCampaignAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        
        private static void DeleteCampaignAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            ExternalLibraryOverseer.Instance.DeleteCampaign(storedIndex);
            CampaignShowPrevious();
            storedIndex = -1;
        }

        public static void DeletePalette(int assetIndex)
        {
            storedIndex = assetIndex;
            MessageWindowInfo data = new("Do you really want to remove this palette?", "Yes", "No", DeletePaletteAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeletePaletteAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemovePalette(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Palette);
            storedIndex = -1;
        }
        
        public static void DeleteSprite(int assetIndex)
        {
            storedIndex = assetIndex;
            MessageWindowInfo data = new("Do you really want to remove this sprite?", "Yes", "No", DeleteSpriteAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeleteSpriteAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveSprite(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Sprite);
            storedIndex = -1;
        }
        
        public static void DeleteWeapon(int assetIndex)
        {
            storedIndex = assetIndex;
            MessageWindowInfo data = new("Do you really want to remove this weapon?", "Yes", "No", DeleteWeaponAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeleteWeaponAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveWeapon(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Weapon);
            storedIndex = -1;
        }
        
        public static void DeleteProjectile(int assetIndex)
        {
            storedIndex = assetIndex;
            MessageWindowInfo data = new("Do you really want to remove this projectile?", "Yes", "No", DeleteProjectileAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeleteProjectileAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveProjectile(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Projectile);
            storedIndex = -1;
        }
        
        public static void DeleteEnemy(int assetIndex)
        {
            storedIndex = assetIndex;
            MessageWindowInfo data = new("Do you really want to remove this enemy?", "Yes", "No", DeleteEnemyAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeleteEnemyAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            
            PackEditorOverseer.Instance.RemoveEnemy(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Enemy);
            storedIndex = -1;
        }
        
        public static void DeleteRoom(int assetIndex)
        {
            storedIndex = assetIndex;
            MessageWindowInfo data = new("Do you really want to remove this room?", "Yes", "No", DeleteRoomAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeleteRoomAccept()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(storedIndex, 0, "StoredNumber");
            PackEditorOverseer.Instance.RemoveRoom(storedIndex);
            GASRogium.OpenSelectionMenu(AssetType.Room);
            storedIndex = -1;
        }
        
        public static void DeleteTile(int assetIndex)
        {
            storedIndex = assetIndex;
            MessageWindowInfo data = new("Do you really want to remove this tile?", "Yes", "No", DeleteTileAccept);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
        }
        private static void DeleteTileAccept()
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
            storedIndex = packIndex;
        }

        public static void OpenEditorCampaign(int assetIndex)
        {
            GAS.SwitchMenu(MenuType.CampaignEditor);
            ExternalLibraryOverseer.Instance.ActivateCampaignEditor(assetIndex);
            CampaignEditorOverseerMono.GetInstance().FillMenu();
            storedIndex = assetIndex;
        }

        public static void OpenEditorPalette(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PaletteEditor);
            PackEditorOverseer.Instance.ActivatePaletteEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorSprite(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.SpriteEditor);
            PackEditorOverseer.Instance.ActivateSpriteEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorWeapon(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateWeaponEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorProjectile(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateProjectileEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorEnemy(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateEnemyEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorRoom(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GAS.SwitchMenu(MenuType.RoomEditor);
            PackEditorOverseer.Instance.ActivateRoomEditor(assetIndex);
            storedIndex = assetIndex;
        }

        public static void OpenEditorTile(int assetIndex)
        {
            CanvasOverseer.GetInstance().NavigationBar.Hide();
            GASRogium.ChangeTheme(ThemeType.Yellow);
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateTileEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenOptionsMenu()
        {
            GASRogium.ChangeTheme(ThemeType.Red);
            ExternalLibraryOverseer.Instance.ActivateOptionsEditor();
            GAS.SwitchMenu(MenuType.OptionsMenu);
        }
        #endregion

        #region Save Editor Changes
        public static void SaveChangesCampaign()
        {
            MessageWindowInfo data = (CampaignEditorOverseerMono.GetInstance().SelectionPicker.SelectionCount <= 0) 
                ? new MessageWindowInfo("Cannot save the campaign without selecting any <style=\"CardAmount\"> packs</style>.", "OK")
                : new("Combine selected packs into a Campaign?\n\nChanges made to any packs will not affect this campaign.","Combine","Cancel", SaveChangesCampaignConfirm);
            ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
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
            RoomAsset currentAsset = RoomEditorOverseer.Instance.CurrentAsset;
            if (!currentAsset.ObjectGrid.Contains(AssetDataBuilder.ForObject(InternalLibraryOverseer.GetInstance().GetObjectByID("001"))) || !currentAsset.ObjectGrid.Contains(AssetDataBuilder.ForObject(InternalLibraryOverseer.GetInstance().GetObjectByID("002"))))
            {
                MessageWindowInfo data = new("There must be at least 1 <style=\"ExitGate\"> Exit Gate</style>\n and <style=\"StartingPoint\">Starting Point</style> present.", "OK");
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
                return;
            }
            RoomEditorOverseer.Instance.CompleteEditing();
            OpenSelectionRoom();
        }

        public static void SaveChangesTile()
        {
            TileEditorOverseer.Instance.CompleteEditing();
            OpenSelectionTile();
        }
        
        public static void OptionsSavePreferences()
        {
            OptionsMenuOverseer.Instance.CompleteEditing();
            ReturnToMainMenuOptionsConfirm();
        }
        #endregion

        #region Cancel Editor Changes
        public static void CancelChangesCampaign()
        {
            bool noPacksSelected = (CampaignEditorOverseer.Instance.CurrentAsset.PackReferences.Count <= 0);
            MessageWindowInfo data = (noPacksSelected) ? new("This campaign contains no <style=\"CardAmount\"> packs</style>. Delete it?", "Delete it", "Cancel", () =>
                                                            {
                                                                DeleteCampaignAccept();
                                                                OpenSelectionCampaign();
                                                            })
                                                        : new("Leave without saving changes?","Yes","No", CancelChangesCampaignConfirm);
            if (!noPacksSelected && CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else CancelChangesCampaignConfirm();
        }
        
        private static void CancelChangesCampaignConfirm()
        {
            GAS.SwitchMenu(MenuType.CampaignSelection);
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
        }
        
        public static void CancelChangesPalette()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", OpenSelectionPalette);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else OpenSelectionPalette();
        }
        
        public static void CancelChangesSprite()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", OpenSelectionSprite);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else OpenSelectionSprite();
        }
        
        public static void CancelChangesWeapon()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", OpenSelectionWeapon);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else OpenSelectionWeapon();
        }
        
        public static void CancelChangesProjectile()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", OpenSelectionProjectile);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else OpenSelectionProjectile();
        }
        
        public static void CancelChangesEnemy()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", OpenSelectionEnemy);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else OpenSelectionEnemy();
        }
        
        public static void CancelChangesRoom()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", OpenSelectionRoom);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else OpenSelectionRoom();
        }

        public static void CancelChangesTile()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", OpenSelectionTile);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else OpenSelectionTile();
        }
        
        public static void CancelChangesOptions()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                MessageWindowInfo data = new("Leave without saving changes?", "Yes", "No", CancelChangesOptionsConfirm);
                ModalWindowBuilder.GetInstance().OpenMessageWindow(data);
            }
            else CancelChangesOptionsConfirm();
            
            void CancelChangesOptionsConfirm()
            {
                ExternalLibraryOverseer.Instance.RefreshSettings();
                ReturnToMainMenuOptionsConfirm();
            }
        }
        
        private static void ReturnToMainMenuOptionsConfirm() => GAS.SwitchMenu(MenuType.MainMenu);

        #endregion

        #region Campaign Editor Menu
        public static void ChangeImportStatus(int assetIndex)
        {
            CampaignEditorOverseerMono.GetInstance().ChangeSelectStatus(assetIndex);
        }
        public static void CampaignEditorSelectAll()
        {
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.SelectAll();
        }
        
        public static void CampaignEditorSelectNone()
        {
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.DeselectAll();
        }
        
        public static void CampaignEditorSelectRandom()
        {
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.SelectRandom();
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

        public static void CampaignRefreshAll()
        {
            ModalWindowBuilder.GetInstance().OpenMessageWindow(new MessageWindowInfo("Refresh all campaigns?\nThis can take a while.", "Yes", "No", CampaignRefreshAllAccept));
        }
        
        public static void CampaignRefreshAllAccept()
        {
            CampaignEditorOverseer editor = CampaignEditorOverseer.Instance;
            ExternalLibraryOverseer lib = ExternalLibraryOverseer.Instance;
            IList<CampaignAsset> campaigns = lib.GetCampaignsCopy;
            foreach (CampaignAsset campaign in campaigns)
            {
                IList<PackAsset> packs = lib.GetPacksCopy.GrabBasedOn(campaign.PackReferences);
                if (packs == null || packs.Count <= 0) continue;
                
                editor.AssignAsset(campaign, campaigns.IndexOf(campaign), false);
                editor.UpdateDataPack(packs);
                editor.CompleteEditing();
            }
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
        }
        
        public static void CampaignRefresh(int index)
        {
            CampaignAssetSelectionOverseer overseer = CampaignAssetSelectionOverseer.Instance;
            CampaignEditorOverseer editor = CampaignEditorOverseer.Instance;
            ExternalLibraryOverseer lib = ExternalLibraryOverseer.Instance;
            CampaignAsset currentAsset = overseer.GetSelectedCampaign();
            IList<PackAsset> packs = lib.GetPacksCopy.GrabBasedOn(currentAsset.PackReferences);

            if (packs == null || packs.Count <= 0) return;
            
            editor.AssignAsset(currentAsset, index, false);
            editor.UpdateDataPack(packs);
            editor.CompleteEditing();
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
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