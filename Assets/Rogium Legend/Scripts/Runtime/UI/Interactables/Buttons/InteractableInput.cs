using System;
using RedRats.Safety;
using Rogium.Systems.GASExtension;
using UnityEngine;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Processes input from InteractableButtons.
    /// </summary>
    public static class InteractableInput
    {
        public static void Handle(ButtonType action, int index)
        {
            switch (action)
            {
                case ButtonType.None:
                    Debug.LogError("This button Currently does nothing...");
                    break;
                case ButtonType.QuitGame:
                    GASActions.GameQuit();
                    break;
                case ButtonType.OpenOptionsMenu:
                    GASActions.OpenOptionsMenu();
                    break;
                case ButtonType.OpenChangelog:
                    GASActions.OpenChangelog();
                    break;

                #region Return from Menus

                case ButtonType.ReturnToAssetTypeSelection:
                    break;
                case ButtonType.SelectionMenuReturn:
                    GASActions.ReturnFromSelectionMenu();
                    break;
                case ButtonType.ReturnToMainMenuFromOptions:
                    GASActions.CancelChangesOptions();
                    break;
                case ButtonType.ReturnToMainMenuFromChangelog:
                    GASActions.ReturnToMainMenuChangelog();
                    break;

                #endregion

                #region Open Selection Menus

                case ButtonType.OpenEditor:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PACK");
                    GASActions.OpenEditor(index);
                    break;
                case ButtonType.SelectionOpenPack:
                    GASActions.OpenSelectionPack();
                    break;
                case ButtonType.SelectionOpenCampaign:
                    GASActions.OpenSelectionCampaign();
                    break;
                case ButtonType.SelectionOpenPalette:
                    GASActions.OpenSelectionPalette();
                    break;
                case ButtonType.SelectionOpenSprite:
                    GASActions.OpenSelectionSprite();
                    break;
                case ButtonType.SelectionOpenWeapon:
                    GASActions.OpenSelectionWeapon();
                    break;
                case ButtonType.SelectionOpenProjectile:
                    GASActions.OpenSelectionProjectile();
                    break;
                case ButtonType.SelectionOpenEnemy:
                    GASActions.OpenSelectionEnemy();
                    break;
                case ButtonType.SelectionOpenRoom:
                    GASActions.OpenSelectionRoom();
                    break;
                case ButtonType.SelectionOpenTile:
                    GASActions.OpenSelectionTile();
                    break;

                #endregion

                #region Create Assets

                case ButtonType.CreatePack:
                    GASActions.CreatePack();
                    break;
                case ButtonType.CreateCampaign:
                    GASActions.CreateCampaign();
                    break;
                case ButtonType.CreatePalette:
                    GASActions.CreatePalette();
                    break;
                case ButtonType.CreateSprite:
                    GASActions.CreateSprite();
                    break;
                case ButtonType.CreateWeapon:
                    GASActions.CreateWeapon();
                    break;
                case ButtonType.CreateProjectile:
                    GASActions.CreateProjectile();
                    break;
                case ButtonType.CreateEnemy:
                    GASActions.CreateEnemy();
                    break;
                case ButtonType.CreateTile:
                    GASActions.CreateTile();
                    break;
                case ButtonType.CreateRoom:
                    GASActions.CreateRoom();
                    break;

                #endregion

                #region Edit Asset Properties

                case ButtonType.EditPackProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PACK PROPERTIES");
                    GASActions.EditPropertiesPack(index);
                    break;
                case ButtonType.EditCampaignProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT CAMPAIGN PROPERTIES");
                    GASActions.EditPropertiesCampaign(index);
                    break;
                case ButtonType.EditPaletteProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PALETTE PROPERTIES");
                    GASActions.EditPropertiesPalette(index);
                    break;
                case ButtonType.EditSpriteProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT SPRITE PROPERTIES");
                    GASActions.EditPropertiesSprite(index);
                    break;
                case ButtonType.EditWeaponProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT WEAPON PROPERTIES");
                    GASActions.EditPropertiesWeapon(index);
                    break;
                case ButtonType.EditProjectileProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PROJECTILE PROPERTIES");
                    GASActions.EditPropertiesProjectile(index);
                    break;
                case ButtonType.EditEnemyProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT ENEMY PROPERTIES");
                    GASActions.EditPropertiesEnemy(index);
                    break;
                case ButtonType.EditRoomProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT ROOM PROPERTIES");
                    GASActions.EditPropertiesRoom(index);
                    break;
                case ButtonType.EditTileProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT TILE PROPERTIES");
                    GASActions.EditPropertiesTile(index);
                    break;

                #endregion

                #region Delete Assets

                case ButtonType.DeletePack:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE PACK");
                    GASActions.DeletePack(index);
                    break;
                case ButtonType.DeleteCampaign:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE CAMPAIGN");
                    GASActions.DeleteCampaign(index);
                    break;
                case ButtonType.DeletePalette:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE PALETTE");
                    GASActions.DeletePalette(index);
                    break;
                case ButtonType.DeleteSprite:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE SPRITE");
                    GASActions.DeleteSprite(index);
                    break;
                case ButtonType.DeleteWeapon:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE WEAPON");
                    GASActions.DeleteWeapon(index);
                    break;
                case ButtonType.DeleteProjectile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE PROJECTILE");
                    GASActions.DeleteProjectile(index);
                    break;
                case ButtonType.DeleteEnemy:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE ENEMY");
                    GASActions.DeleteEnemy(index);
                    break;
                case ButtonType.DeleteRoom:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE ROOM");
                    GASActions.DeleteRoom(index);
                    break;
                case ButtonType.DeleteTile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE TILE");
                    GASActions.DeleteTile(index);
                    break;

                #endregion

                #region Open Editors

                case ButtonType.EditorOpenCampaign:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR CAMPAIGN");
                    GASActions.OpenEditorCampaign(index);
                    break;
                case ButtonType.EditorOpenPalette:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR PALETTE");
                    GASActions.OpenEditorPalette(index);
                    break;
                case ButtonType.EditorOpenSprite:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR SPRITE");
                    GASActions.OpenEditorSprite(index);
                    break;
                case ButtonType.EditorOpenWeapon:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR WEAPON");
                    GASActions.OpenEditorWeapon(index);
                    break;
                case ButtonType.EditorOpenProjectile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR PROJECTILE");
                    GASActions.OpenEditorProjectile(index);
                    break;
                case ButtonType.EditorOpenEnemy:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR ENEMY");
                    GASActions.OpenEditorEnemy(index);
                    break;
                case ButtonType.EditorOpenRoom:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR ROOM");
                    GASActions.OpenEditorRoom(index);
                    break;
                case ButtonType.EditorOpenTile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR TILE");
                    GASActions.OpenEditorTile(index);
                    break;

                #endregion

                #region Save Editor Changes

                case ButtonType.SaveChangesCampaign:
                    GASActions.SaveChangesCampaign();
                    break;
                case ButtonType.SaveChangesPalette:
                    GASActions.SaveChangesPalette();
                    break;
                case ButtonType.SaveChangesSprite:
                    GASActions.SaveChangesSprite();
                    break;
                case ButtonType.SaveChangesWeapon:
                    GASActions.SaveChangesWeapon();
                    break;
                case ButtonType.SaveChangesProjectile:
                    GASActions.SaveChangesProjectile();
                    break;
                case ButtonType.SaveChangesEnemy:
                    GASActions.SaveChangesEnemy();
                    break;
                case ButtonType.SaveChangesRoom:
                    GASActions.SaveChangesRoom();
                    break;
                case ButtonType.SaveChangesTile:
                    GASActions.SaveChangesTile();
                    break;

                #endregion

                #region Cancel Editor Changes

                case ButtonType.CancelChangesCampaign:
                    GASActions.CancelChangesCampaign();
                    break;
                case ButtonType.CancelChangesPalette:
                    GASActions.CancelChangesPalette();
                    break;
                case ButtonType.CancelChangesSprite:
                    GASActions.CancelChangesSprite();
                    break;
                case ButtonType.CancelChangesWeapon:
                    GASActions.CancelChangesWeapon();
                    break;
                case ButtonType.CancelChangesProjectile:
                    GASActions.CancelChangesProjectile();
                    break;
                case ButtonType.CancelChangesEnemy:
                    GASActions.CancelChangesEnemy();
                    break;
                case ButtonType.CancelChangesRoom:
                    GASActions.CancelChangesRoom();
                    break;
                case ButtonType.CancelChangesTile:
                    GASActions.CancelChangesTile();
                    break;

                #endregion

                #region Campaign Selection Menu

                case ButtonType.CampaignShowNext:
                    GASActions.CampaignShowNext();
                    break;
                case ButtonType.CampaignShowPrevious:
                    GASActions.CampaignShowPrevious();
                    break;
                case ButtonType.CampaignRefresh:
                    GASActions.CampaignRefresh(index);
                    break;
                case ButtonType.CampaignRefreshAll:
                    GASActions.CampaignRefreshAll();
                    break;

                #endregion

                #region Campaign Editor

                case ButtonType.CampaignEditorSelectAll:
                    GASActions.CampaignEditorSelectAll();
                    break;
                case ButtonType.CampaignEditorSelectNone:
                    GASActions.CampaignEditorSelectNone();
                    break;
                case ButtonType.CampaignEditorSelectRandom:
                    GASActions.CampaignEditorSelectRandom();
                    break;
                case ButtonType.CampaignEditorChangeImportState:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - CHANGE PACK IMPORT STATE");
                    GASActions.ChangeImportStatus(index);
                    break;

                #endregion

                #region Sprite Editor

                case ButtonType.SpriteSwitchTool:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - SWITCH SPRITE TOOL");
                    GASActions.SpriteSwitchTool(index);
                    break;
                case ButtonType.SpriteSwitchPalette:
                    GASActions.SpriteSwitchPalette();
                    break;

                case ButtonType.SpriteClearActiveLayer:
                    GASActions.SpriteClearActiveLayer();
                    break;

                #endregion

                #region Room Editor

                case ButtonType.RoomSwitchTool:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - SWITCH ROOM TOOL");
                    GASActions.RoomSwitchTool(index);
                    break;

                case ButtonType.RoomSwitchPalette:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - SWITCH ROOM PALETTE");
                    GASActions.RoomSwitchPalette(index);
                    break;

                case ButtonType.RoomClearActiveLayer:
                    GASActions.RoomClearActiveLayer();
                    break;

                #endregion

                #region Gameplay Menu

                case ButtonType.GameplayPauseResume:
                    GASActions.GameplayPauseResume();
                    break;

                case ButtonType.GameplayPauseQuit:
                    GASActions.GameplayPauseQuit();
                    break;

                case ButtonType.GameplaySelectWeapon:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - GAMEPLAY SELECT WEAPON");
                    GASActions.GameplaySelectWeapon(index);
                    break;

                #endregion

                #region Options Menu

                case ButtonType.OptionsSavePreferences:
                    GASActions.OptionsSavePreferences();
                    break;

                #endregion

                #region General Editor Actions

                case ButtonType.Undo:
                    GASActions.UndoLastAction();
                    break;
                case ButtonType.Redo:
                    GASActions.RedoLastAction();
                    break;

                #endregion

                case ButtonType.Play:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - PLAY CAMPAIGN");
                    GASActions.PlayCampaign(index);
                    break;

                default:
                    throw new InvalidOperationException("Unknown Button Type.");
            }
        }
    }
}