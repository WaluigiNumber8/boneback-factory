using System;
using BoubakProductions.Safety;
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
                case ButtonType.DoNothing:
                    Debug.LogError("This button Currently does nothing...");
                    break;
                case ButtonType.QuitGame:
                    GASButtonActions.GameQuit();
                    break;
                case ButtonType.OpenOptionsMenu:
                    GASButtonActions.OpenOptionsMenu();
                    break;

                #region Return from Menus
                case ButtonType.ReturnToAssetTypeSelection:
                    break;
                case ButtonType.ReturnToMainMenuFromSelection:
                    GASButtonActions.ReturnToMainMenuSelection();
                    break;
                case ButtonType.ReturnToMainMenuFromOptions:
                    GASButtonActions.ReturnToMainMenuOptions();
                    break;
                #endregion

                #region Open Selection Menus
                case ButtonType.SelectionOpenAssetType:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PACK");
                    GASButtonActions.OpenEditor(index);
                    break;
                case ButtonType.SelectionOpenPack:
                    GASButtonActions.OpenSelectionPack();
                    break;
                case ButtonType.SelectionOpenCampaign:
                    GASButtonActions.OpenSelectionCampaign();
                    break;
                case ButtonType.SelectionOpenPalette:
                    GASButtonActions.OpenSelectionPalette();
                    break;
                case ButtonType.SelectionOpenSprite:
                    GASButtonActions.OpenSelectionSprite();
                    break;
                case ButtonType.SelectionOpenWeapon:
                    GASButtonActions.OpenSelectionWeapon();
                    break;
                case ButtonType.SelectionOpenProjectile:
                    GASButtonActions.OpenSelectionProjectile();
                    break;
                case ButtonType.SelectionOpenEnemy:
                    GASButtonActions.OpenSelectionEnemy();
                    break;
                case ButtonType.SelectionOpenRoom:
                    GASButtonActions.OpenSelectionRoom();
                    break;
                case ButtonType.SelectionOpenTile:
                    GASButtonActions.OpenSelectionTile();
                    break;
                
                #endregion

                #region Create Assets
                case ButtonType.CreatePack:
                    GASButtonActions.CreatePack();
                    break;
                case ButtonType.CreateCampaign:
                    GASButtonActions.CreateCampaign();
                    break;
                case ButtonType.CreatePalette:
                    GASButtonActions.CreatePalette();
                    break;
                case ButtonType.CreateSprite:
                    GASButtonActions.CreateSprite();
                    break;
                case ButtonType.CreateWeapon:
                    GASButtonActions.CreateWeapon();
                    break;
                case ButtonType.CreateProjectile:
                    GASButtonActions.CreateProjectile();
                    break;
                case ButtonType.CreateEnemy:
                    GASButtonActions.CreateEnemy();
                    break;
                case ButtonType.CreateTile:
                    GASButtonActions.CreateTile();
                    break;
                case ButtonType.CreateRoom:
                    GASButtonActions.CreateRoom();
                    break;
                
                #endregion

                #region Edit Asset Properties
                case ButtonType.EditPackProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PACK PROPERTIES");
                    GASButtonActions.EditPropertiesPack(index);
                    break;
                case ButtonType.EditCampaignProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT CAMPAIGN PROPERTIES");
                    GASButtonActions.EditPropertiesCampaign(index);
                    break;
                case ButtonType.EditPaletteProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PALETTE PROPERTIES");
                    GASButtonActions.EditPropertiesPalette(index);
                    break;
                case ButtonType.EditSpriteProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT SPRITE PROPERTIES");
                    GASButtonActions.EditPropertiesSprite(index);
                    break;
                case ButtonType.EditWeaponProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT WEAPON PROPERTIES");
                    GASButtonActions.EditPropertiesWeapon(index);
                    break;
                case ButtonType.EditProjectileProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PROJECTILE PROPERTIES");
                    GASButtonActions.EditPropertiesProjectile(index);
                    break;
                case ButtonType.EditEnemyProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT ENEMY PROPERTIES");
                    GASButtonActions.EditPropertiesEnemy(index);
                    break;
                case ButtonType.EditRoomProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT ROOM PROPERTIES");
                    GASButtonActions.EditPropertiesRoom(index);
                    break;
                case ButtonType.EditTileProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT TILE PROPERTIES");
                    GASButtonActions.EditPropertiesTile(index);
                    break;
                
                #endregion

                #region Delete Assets
                case ButtonType.DeletePack:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE PACK");
                    GASButtonActions.DeletePack(index);
                    break;
                case ButtonType.DeleteCampaign:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE CAMPAIGN");
                    GASButtonActions.DeleteCampaign(index);
                    break;
                case ButtonType.DeletePalette:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE PALETTE");
                    GASButtonActions.DeletePalette(index);
                    break;
                case ButtonType.DeleteSprite:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE SPRITE");
                    GASButtonActions.DeleteSprite(index);
                    break;
                case ButtonType.DeleteWeapon:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE WEAPON");
                    GASButtonActions.DeleteWeapon(index);
                    break;
                case ButtonType.DeleteProjectile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE PROJECTILE");
                    GASButtonActions.DeleteProjectile(index);
                    break;
                case ButtonType.DeleteEnemy:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE ENEMY");
                    GASButtonActions.DeleteEnemy(index);
                    break;
                case ButtonType.DeleteRoom:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE ROOM");
                    GASButtonActions.DeleteRoom(index);
                    break;
                case ButtonType.DeleteTile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE TILE");
                    GASButtonActions.DeleteTile(index);
                    break;
                
                #endregion

                #region Open Editors
                case ButtonType.EditorOpenCampaign:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR CAMPAIGN");
                    GASButtonActions.OpenEditorCampaign(index);
                    break;
                case ButtonType.EditorOpenPalette:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR PALETTE");
                    GASButtonActions.OpenEditorPalette(index);
                    break;
                case ButtonType.EditorOpenSprite:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR SPRITE");
                    GASButtonActions.OpenEditorSprite(index);
                    break;
                case ButtonType.EditorOpenWeapon:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR WEAPON");
                    GASButtonActions.OpenEditorWeapon(index);
                    break;
                case ButtonType.EditorOpenProjectile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR PROJECTILE");
                    GASButtonActions.OpenEditorProjectile(index);
                    break;
                case ButtonType.EditorOpenEnemy:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR ENEMY");
                    GASButtonActions.OpenEditorEnemy(index);
                    break;
                case ButtonType.EditorOpenRoom:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR ROOM");
                    GASButtonActions.OpenEditorRoom(index);
                    break;
                case ButtonType.EditorOpenTile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR TILE");
                    GASButtonActions.OpenEditorTile(index);
                    break;
                #endregion

                #region Save Editor Changes
                case ButtonType.SaveChangesCampaign:
                    GASButtonActions.SaveChangesCampaign();
                    break;
                case ButtonType.SaveChangesPalette:
                    GASButtonActions.SaveChangesPalette();
                    break;
                case ButtonType.SaveChangesSprite:
                    GASButtonActions.SaveChangesSprite();
                    break;
                case ButtonType.SaveChangesWeapon:
                    GASButtonActions.SaveChangesWeapon();
                    break;
                case ButtonType.SaveChangesProjectile:
                    GASButtonActions.SaveChangesProjectile();
                    break;
                case ButtonType.SaveChangesEnemy:
                    GASButtonActions.SaveChangesEnemy();
                    break;
                case ButtonType.SaveChangesRoom:
                    GASButtonActions.SaveChangesRoom();
                    break;
                case ButtonType.SaveChangesTile:
                    GASButtonActions.SaveChangesTile();
                    break;
                #endregion

                #region Cancel Editor Changes
                case ButtonType.CancelChangesCampaign:
                    GASButtonActions.CancelChangesCampaign();
                    break;
                case ButtonType.CancelChangesPalette:
                    GASButtonActions.CancelChangesPalette();
                    break;
                case ButtonType.CancelChangesSprite:
                    GASButtonActions.CancelChangesSprite();
                    break;
                case ButtonType.CancelChangesWeapon:
                    GASButtonActions.CancelChangesWeapon();
                    break;
                case ButtonType.CancelChangesProjectile:
                    GASButtonActions.CancelChangesProjectile();
                    break;
                case ButtonType.CancelChangesEnemy:
                    GASButtonActions.CancelChangesEnemy();
                    break;
                case ButtonType.CancelChangesRoom:
                    GASButtonActions.CancelChangesRoom();
                    break;
                case ButtonType.CancelChangesTile:
                    GASButtonActions.CancelChangesTile();
                    break;
                #endregion

                #region Campaign Selection
                case ButtonType.CampaignShowNext:
                    GASButtonActions.CampaignShowNext();
                    break;
                case ButtonType.CampaignShowPrevious:
                    GASButtonActions.CampaignShowPrevious();
                    break;
                case ButtonType.CampaignEditorChangeImportState:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - CHANGE PACK IMPORT STATE");
                    GASButtonActions.ChangeImportStatus(index);
                    break;
                #endregion
                
                #region Campaign Editor Menu
                case ButtonType.CampaignEditorSelectAll:
                    GASButtonActions.CampaignEditorSelectAll();
                    break;
                case ButtonType.CampaignEditorSelectNone:
                    GASButtonActions.CampaignEditorSelectNone();
                    break;
                case ButtonType.CampaignEditorSelectRandom:
                    break;
                #endregion

                #region Sprite Editor
                case ButtonType.SpriteSwitchTool:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - SWITCH SPRITE TOOL");
                    GASButtonActions.SpriteSwitchTool(index);
                    break;
                case ButtonType.SpriteSwitchPalette:
                    GASButtonActions.SpriteSwitchPalette();
                    break;
                
                case ButtonType.SpriteClearActiveLayer:
                    GASButtonActions.SpriteClearActiveLayer();
                    break;
                #endregion
                
                #region Room Editor
                case ButtonType.RoomSwitchTool:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - SWITCH ROOM TOOL");
                    GASButtonActions.RoomSwitchTool(index);
                    break;
                
                case ButtonType.RoomSwitchPalette:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - SWITCH ROOM PALETTE");
                    GASButtonActions.RoomSwitchPalette(index);
                    break;
                
                case ButtonType.RoomClearActiveLayer:
                    GASButtonActions.RoomClearActiveLayer();
                    break;

                #endregion

                #region Gameplay Menu
                case ButtonType.GameplayPauseResume:
                    GASButtonActions.GameplayPauseResume();
                    break;
                
                case ButtonType.GameplayPauseQuit:
                    GASButtonActions.GameplayPauseQuit();
                    break;

                case ButtonType.GameplaySelectWeapon:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - GAMEPLAY SELECT WEAPON");
                    GASButtonActions.GameplaySelectWeapon(index);
                    break;
                #endregion
                
                case ButtonType.Play:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - PLAY CAMPAIGN");
                    GASButtonActions.PlayCampaign(index);
                    break;
                
                default:
                    throw new InvalidOperationException("Unknown Button Type.");
            }
        }
    }
}