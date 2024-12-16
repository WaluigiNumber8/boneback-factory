using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Systems.GASCore;
using RedRats.Safety;
using RedRats.Systems.Themes;
using RedRats.UI.MenuSwitching;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.AssetSelection;
using Rogium.Editors.AssetSelection.Campaigns;
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
using Rogium.UserInterface.Backgrounds;
using Rogium.UserInterface.Editors.ModalWindowBuilding;
using Rogium.UserInterface.Gameplay.PauseMenu;
using UnityEngine;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// Contains general actions for the game's menu system.
    /// </summary>
    public class GASActions : MonoBehaviour
    {
        private static int storedIndex = -1; //Used for method traveling.
        
        public static void GameQuit()
        {
            Application.Quit();
        }
        
        public static void PlayCampaignCurrent() => PlayCampaign(CampaignAssetSelectionOverseer.Instance.SelectedIndex);
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
            ActionHistorySystem.Undo();
        }
        
        public static void RedoLastAction()
        {
            ActionHistorySystem.Redo();
        }

        #endregion
        
        #region Return from menus
        public static void ReturnFromSelectionMenu()
        {
            if (SelectionMenuOverseerMono.GetInstance().CurrentType != AssetType.Pack && MenuSwitcher.GetInstance().CurrentMenu  != MenuType.CampaignSelection)
            {
                OpenSelectionPack();
                return;
            }

            GAS.SwitchMenu(MenuType.MainMenu);
            BackgroundOverseerMono.GetInstance().SwitchToMainMenu();
            GASRogium.ChangeTheme(ThemeType.Blue);
        }

        public static void ReturnToMainMenuChangelog()
        {
            GAS.SwitchMenu(MenuType.MainMenu);
        }
        #endregion

        #region Open Selection Menus
        public static void OpenSelectionPack()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            BackgroundOverseerMono.GetInstance().SwitchToEditor();
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Pack);
        }

        public static void OpenSelectionCampaign()
        {
            GASRogium.ChangeTheme(ThemeType.Red);
            GAS.SwitchMenu(MenuType.CampaignSelection);
            BackgroundOverseerMono.GetInstance().SwitchToGameMenu();
            CampaignAssetSelectionOverseer.Instance.SelectCampaignFirst();
        }
        
        public static void OpenSelectionPalette()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Palette);
        }
        
        public static void OpenSelectionSprite()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Sprite);
        }
        
        public static void OpenSelectionWeapon()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Weapon);
        }
        
        public static void OpenSelectionProjectile()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Projectile);
        }
        
        public static void OpenSelectionEnemy()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Enemy);
        }
        
        public static void OpenSelectionRoom()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Room);
        }
        
        public static void OpenSelectionTile()
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASRogium.OpenSelectionMenu(AssetType.Tile);
        }

        #endregion

        #region Create Assets

        public static void CreateNewAssetBasedOnSelectionMenu()
        {
            switch (SelectionMenuOverseerMono.GetInstance().CurrentType)
            {
                case AssetType.Pack:
                    CreatePack();
                    break;
                case AssetType.Palette:
                    CreatePalette();
                    break;
                case AssetType.Sprite:
                    CreateSprite();
                    break;
                case AssetType.Weapon:
                    CreateWeapon();
                    break;
                case AssetType.Projectile:
                    CreateProjectile();
                    break;
                case AssetType.Enemy:
                    CreateEnemy();
                    break;
                case AssetType.Room:
                    CreateRoom();
                    break;
                case AssetType.Tile:
                    CreateTile();
                    break;
                default: throw new ArgumentOutOfRangeException($"Asset type {SelectionMenuOverseerMono.GetInstance().CurrentType} is not supported by this method.");
            }
        }
        
        public static void CreatePack()
        {
            new ModalWindowPropertyBuilderPack().OpenForCreate(OpenSelectionPack);
        }
        
        public static void CreateCampaign()
        {
            new ModalWindowPropertyBuilderCampaign().OpenForCreate(() =>
            {
                CampaignAssetSelectionOverseer.Instance.SelectCampaignLast();
                OpenEditorCampaign(ExternalLibraryOverseer.Instance.Campaigns.Count - 1);
            });
        }

        public static void CreatePalette()
        {
            new ModalWindowPropertyBuilderPalette().OpenForCreate(OpenSelectionPalette);
        }

        public static void CreateSprite()
        {
            new ModalWindowPropertyBuilderSprite().OpenForCreate(OpenSelectionSprite);
        }
        
        public static void CreateWeapon()
        {
            new ModalWindowPropertyBuilderWeapon().OpenForCreate(OpenSelectionWeapon);
        }
        
        public static void CreateProjectile()
        {
            new ModalWindowPropertyBuilderProjectile().OpenForCreate(OpenSelectionProjectile);
        }
        
        public static void CreateEnemy()
        {
            new ModalWindowPropertyBuilderEnemy().OpenForCreate(OpenSelectionEnemy);
        }
        
        public static void CreateTile()
        {
            new ModalWindowPropertyBuilderTile().OpenForCreate(OpenSelectionTile);
        }
        
        public static void CreateRoom()
        {
            new ModalWindowPropertyBuilderRoom().OpenForCreate(OpenSelectionRoom);
        }
        #endregion
        
        #region Edit Asset Properties
        public static void EditPropertiesPack(int packIndex)
        {
            ExternalLibraryOverseer.Instance.ActivatePackEditor(packIndex);
            new ModalWindowPropertyBuilderPack().OpenForUpdate(OpenSelectionPack);
        }

        public static void EditPropertiesCampaign(int campaignIndex)
        {
            ExternalLibraryOverseer.Instance.ActivateCampaignEditor(campaignIndex, false);
            new ModalWindowPropertyBuilderCampaign().OpenForUpdate(CampaignAssetSelectionOverseer.Instance.SelectAgain);
        }
        
        public static void EditPropertiesPalette(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivatePaletteEditor(assetIndex, false);
            new ModalWindowPropertyBuilderPalette().OpenForUpdate(OpenSelectionPalette);
        }
        
        public static void EditPropertiesSprite(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateSpriteEditor(assetIndex, false);
            new ModalWindowPropertyBuilderSprite().OpenForUpdate(OpenSelectionSprite);
        }
        
        public static void EditPropertiesWeapon(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateWeaponEditor(assetIndex, false);
            new ModalWindowPropertyBuilderWeapon().OpenForUpdate(OpenSelectionWeapon);
        }
        
        public static void EditPropertiesProjectile(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateProjectileEditor(assetIndex, false);
            new ModalWindowPropertyBuilderProjectile().OpenForUpdate(OpenSelectionProjectile);
        }
        
        public static void EditPropertiesEnemy(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateEnemyEditor(assetIndex, false);
            new ModalWindowPropertyBuilderEnemy().OpenForUpdate(OpenSelectionEnemy);
        }
        
        public static void EditPropertiesRoom(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateRoomEditor(assetIndex, false);
            new ModalWindowPropertyBuilderRoom().OpenForUpdate(OpenSelectionRoom);
        }
        
        public static void EditPropertiesTile(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateTileEditor(assetIndex, false);
            new ModalWindowPropertyBuilderTile().OpenForUpdate(OpenSelectionTile);
        }
        #endregion

        #region Clone Assets

        public static void ClonePack(int packIndex)
        {
            ExternalLibraryOverseer.Instance.ActivatePackEditor(packIndex);
            new ModalWindowPropertyBuilderPack().OpenForClone(OpenSelectionPack);
        }
        
        public static void CloneCampaign(int campaignIndex)
        {
            ExternalLibraryOverseer.Instance.ActivateCampaignEditor(campaignIndex);
            new ModalWindowPropertyBuilderCampaign().OpenForClone(CampaignAssetSelectionOverseer.Instance.SelectAgain);
        }
        
        public static void ClonePalette(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivatePaletteEditor(assetIndex);
            new ModalWindowPropertyBuilderPalette().OpenForClone(OpenSelectionPalette);
        }
        
        public static void CloneSprite(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateSpriteEditor(assetIndex);
            new ModalWindowPropertyBuilderSprite().OpenForClone(OpenSelectionSprite);
        }
        
        public static void CloneWeapon(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateWeaponEditor(assetIndex);
            new ModalWindowPropertyBuilderWeapon().OpenForClone(OpenSelectionWeapon);
        }

        public static void CloneProjectile(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateProjectileEditor(assetIndex);
            new ModalWindowPropertyBuilderProjectile().OpenForClone(OpenSelectionProjectile);
        }
        
        public static void CloneEnemy(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateEnemyEditor(assetIndex);
            new ModalWindowPropertyBuilderEnemy().OpenForClone(OpenSelectionEnemy);
        }
        
        public static void CloneRoom(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateRoomEditor(assetIndex);
            new ModalWindowPropertyBuilderRoom().OpenForClone(OpenSelectionRoom);
        }
        
        public static void CloneTile(int assetIndex)
        {
            PackEditorOverseer.Instance.ActivateTileEditor(assetIndex);
            new ModalWindowPropertyBuilderTile().OpenForClone(OpenSelectionTile);
        }

        #endregion
        
        #region Remove Assets
        public static void DeletePack(int packIndex)
        {
            storedIndex = packIndex;
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this pack?")
                .WithAcceptButton("Yes", DeletePackAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this campaign?")
                .WithAcceptButton("Yes", DeleteCampaignAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this palette?")
                .WithAcceptButton("Yes", DeletePaletteAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this sprite?")
                .WithAcceptButton("Yes", DeleteSpriteAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this weapon?")
                .WithAcceptButton("Yes", DeleteWeaponAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this projectile?")
                .WithAcceptButton("Yes", DeleteProjectileAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this enemy?")
                .WithAcceptButton("Yes", DeleteEnemyAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this room?")
                .WithAcceptButton("Yes", DeleteRoomAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Do you really want to remove this tile?")
                .WithAcceptButton("Yes", DeleteTileAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
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
            storedIndex = packIndex;
            SelectionMenuOverseerMono.GetInstance().ResetTabGroup();
            OpenSelectionPalette();
        }

        public static void OpenEditorCampaignCurrent() => OpenEditorCampaign(CampaignAssetSelectionOverseer.Instance.SelectedIndex);

        public static void OpenEditorCampaign(int assetIndex)
        {
            GAS.SwitchMenu(MenuType.CampaignEditor);
            ExternalLibraryOverseer.Instance.ActivateCampaignEditor(assetIndex);
            storedIndex = assetIndex;
        }

        public static void OpenEditorPalette(int assetIndex)
        {
            GASRogium.ChangeTheme(ThemeType.Purple);
            GAS.SwitchMenu(MenuType.PaletteEditor);
            PackEditorOverseer.Instance.ActivatePaletteEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorSprite(int assetIndex)
        {
            GASRogium.ChangeTheme(ThemeType.Pink);
            GAS.SwitchMenu(MenuType.SpriteEditor);
            PackEditorOverseer.Instance.ActivateSpriteEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorWeapon(int assetIndex)
        {
            GASRogium.ChangeTheme(ThemeType.Green);
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateWeaponEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorProjectile(int assetIndex)
        {
            GASRogium.ChangeTheme(ThemeType.Teal);
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateProjectileEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorEnemy(int assetIndex)
        {
            GASRogium.ChangeTheme(ThemeType.Red);
            GAS.SwitchMenu(MenuType.PropertyEditor);
            PackEditorOverseer.Instance.ActivateEnemyEditor(assetIndex);
            storedIndex = assetIndex;
        }
        
        public static void OpenEditorRoom(int assetIndex)
        {
            GASRogium.ChangeTheme(ThemeType.Blue);
            GAS.SwitchMenu(MenuType.RoomEditor);
            PackEditorOverseer.Instance.ActivateRoomEditor(assetIndex);
            storedIndex = assetIndex;
        }

        public static void OpenEditorTile(int assetIndex)
        {
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
            bool noPacksSelected = (CampaignEditorOverseerMono.GetInstance().SelectionPicker.SelectedAssetsCount <= 0);
            ModalWindowData noPackData = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Cannot save the campaign without selecting any <style=\"CardAmount\"> packs</style>.")
                .WithAcceptButton("OK")
                .Build();
            ModalWindowData data = (noPacksSelected) ? noPackData
                : new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Combine selected packs into a Campaign?\n\nChanges made to any packs will not affect this campaign.")
                    .WithAcceptButton("Combine", SaveChangesCampaignConfirm)
                    .WithDenyButton("Cancel")
                    .Build();
            GASRogium.OpenWindow(data);
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
            //If sprite's palette is missing, don't save
            if (SpriteEditorOverseer.Instance.CurrentAsset.AssociatedPaletteID == EditorDefaults.EmptyAssetID)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("No palette was assigned, but placeholder was edited. Save as a copy?")
                    .WithAcceptButton("Save as Copy", () =>
                    {
                        PaletteEditorOverseer.Instance.AssignAsset(SpriteEditorOverseer.Instance.CurrentPalette, PackEditorOverseer.Instance.CurrentPack.Palettes.Count, false);
                        SaveEditedPaletteAsClone();
                    })
                    .WithDenyButton("No Save", () => { SpriteEditorOverseer.Instance.ResetPalette(); SaveChangesSpriteConfirm();})
                    .Build();
                GASRogium.OpenWindow(data);
                return;
            }
            
            if (SpriteEditorOverseerMono.GetInstance().PaletteChanged)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("The palette was edited. Save it's changes?")
                    .WithAcceptButton("Override", () => { SavePaletteAsOverride(); SaveChangesSpriteConfirm(); })
                    .WithSpecialButton("Save as Copy", SavePaletteAsNew)
                    .WithDenyButton("No Save", () => { SpriteEditorOverseer.Instance.ResetPalette(); SaveChangesSpriteConfirm();})
                    .Build();
                GASRogium.OpenWindow(data);
                return;
            }
            
            SaveChangesSpriteConfirm();
        }

        public static void SavePaletteAsOverride()
        {
            int index = PackEditorOverseer.Instance.CurrentPack.Palettes.FindIndexFirst(SpriteEditorOverseer.Instance.CurrentPalette.ID);
            PaletteEditorOverseer.Instance.AssignAsset(SpriteEditorOverseer.Instance.CurrentPalette, index, false);
            PaletteEditorOverseer.Instance.CompleteEditing();
        }
        
        public static void SavePaletteAsNew()
        {
            //Create new palette as copy of the current one
            int index = PackEditorOverseer.Instance.CurrentPack.Palettes.FindIndexFirst(SpriteEditorOverseer.Instance.CurrentPalette.ID);
            PaletteEditorOverseer.Instance.AssignAsset(SpriteEditorOverseer.Instance.CurrentPalette, index, false);
            SaveEditedPaletteAsClone();
        }

        private static void SaveEditedPaletteAsClone()
        {
            new ModalWindowPropertyBuilderPalette().OpenForClone((() =>
            {
                //Associate the new palette with the current sprite
                PaletteAsset clone = PackEditorOverseer.Instance.CurrentPack.Palettes[^1];
                SpriteEditorOverseer.Instance.UpdatePalette(clone);
                
                //Save sprite changes
                SaveChangesSpriteConfirm();
            }));
        }

        private static void SaveChangesSpriteConfirm()
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
            InternalLibraryOverseer lib = InternalLibraryOverseer.GetInstance();
            if (!currentAsset.ObjectGrid.Contains(AssetDataBuilder.ForObject(lib.GetObjectByID("001"))) || !currentAsset.ObjectGrid.Contains(AssetDataBuilder.ForObject(lib.GetObjectByID("002"))))
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("There must be at least 1 <style=\"ExitGate\"> Exit Gate</style>\n and <style=\"StartingPoint\">Starting Point</style> present.")
                    .WithAcceptButton("OK")
                    .Build();
                GASRogium.OpenWindow(data);
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
            bool noPacksSelected = (CampaignEditorOverseerMono.GetInstance().SelectionPicker.SelectedAssetsCount <= 0);
            bool campaignIsNew = (CampaignEditorOverseer.Instance.CurrentAsset.PackReferences.Count <= 0);
            ModalWindowData noPackData = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("This campaign contains no <style=\"CardAmount\"> packs</style>. Delete it?")
                .WithAcceptButton("Delete it", () => { DeleteCampaignAccept(); OpenSelectionCampaign(); })
                .WithDenyButton("Cancel")
                .Build();
            ModalWindowData data = (noPacksSelected || campaignIsNew) ? noPackData : new ModalWindowData.Builder().
                WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Leave without saving changes?")
                .WithAcceptButton("Yes", CancelChangesCampaignConfirm)
                .WithDenyButton("No")
                .Build();
            
            if (noPacksSelected ||campaignIsNew || (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0))
            {
                GASRogium.OpenWindow(data);
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
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", OpenSelectionPalette)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else OpenSelectionPalette();
        }
        
        public static void CancelChangesSprite()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", OpenSelectionSprite)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else OpenSelectionSprite();
        }
        
        public static void CancelChangesWeapon()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", OpenSelectionWeapon)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else OpenSelectionWeapon();
        }
        
        public static void CancelChangesProjectile()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", OpenSelectionProjectile)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else OpenSelectionProjectile();
        }
        
        public static void CancelChangesEnemy()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", OpenSelectionEnemy)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else OpenSelectionEnemy();
        }
        
        public static void CancelChangesRoom()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", OpenSelectionRoom)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else OpenSelectionRoom();
        }

        public static void CancelChangesTile()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", OpenSelectionTile)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else OpenSelectionTile();
        }
        
        public static void CancelChangesOptions()
        {
            if (CurrentAssetDetector.Instance.WasEdited && ActionHistorySystem.UndoCount > 0)
            {
                ModalWindowData data = new ModalWindowData.Builder()
                    .WithLayout(ModalWindowLayoutType.Message)
                    .WithMessage("Leave without saving changes?")
                    .WithAcceptButton("Yes", CancelChangesOptionsConfirm)
                    .WithDenyButton("No")
                    .Build();
                GASRogium.OpenWindow(data);
            }
            else CancelChangesOptionsConfirm();
            
            void CancelChangesOptionsConfirm()
            {
                ExternalLibraryOverseer.Instance.RefreshOptions();
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
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.SelectAll(true);
        }
        
        public static void CampaignEditorSelectNone()
        {
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.SelectAll(false);
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
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Message)
                .WithMessage("Refresh all campaigns?\nThis can take a while.")
                .WithAcceptButton("Yes", CampaignRefreshAllAccept)
                .WithDenyButton("No")
                .Build();
            GASRogium.OpenWindow(data);
        }
        
        public static void CampaignRefreshAllAccept()
        {
            CampaignEditorOverseer editor = CampaignEditorOverseer.Instance;
            ExternalLibraryOverseer lib = ExternalLibraryOverseer.Instance;
            IList<CampaignAsset> campaigns = lib.Campaigns;
            foreach (CampaignAsset campaign in campaigns)
            {
                ISet<PackAsset> packs = lib.Packs.ToHashSet().GrabBasedOn(campaign.PackReferences);
                if (packs == null || packs.Count <= 0) continue;
                
                editor.AssignAsset(campaign, campaigns.IndexOf(campaign), false);
                editor.UpdateDataPack(packs);
                editor.CompleteEditing();
            }
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
        }
        
        public static void CampaignRefreshCurrent() => CampaignRefresh(CampaignAssetSelectionOverseer.Instance.SelectedIndex);
        public static void CampaignRefresh(int index)
        {
            CampaignAssetSelectionOverseer overseer = CampaignAssetSelectionOverseer.Instance;
            CampaignEditorOverseer editor = CampaignEditorOverseer.Instance;
            ExternalLibraryOverseer lib = ExternalLibraryOverseer.Instance;
            CampaignAsset currentAsset = overseer.GetSelectedCampaign();
            ISet<PackAsset> packs = lib.Packs.ToHashSet().GrabBasedOn(currentAsset.PackReferences);

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