using BoubakProductions.Safety;
using Rogium.Global.GASExtension;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Handles input from the button component via the GAS System.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class InteractableButton : MonoBehaviour, IInteractableButton
    {
        [SerializeField] private ButtonType action;
        [SerializeField] private int number = -1;
        
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            switch (action)
            {
                case ButtonType.DoNothing:
                    Debug.LogError("This button Currently does nothing...");
                    break;

                #region Return from Menus
                case ButtonType.ReturnToAssetTypeSelection:
                    break;
                case ButtonType.ReturnToMainMenu:
                    GASButtonActions.ReturnToMainMenu();
                    break;
                #endregion

                #region Open Selection Menus
                case ButtonType.SelectionOpenAssetType:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT PACK");
                    GASButtonActions.OpenEditor(number);
                    break;
                case ButtonType.SelectionOpenPack:
                    GASButtonActions.OpenPackSelection();
                    break;
                case ButtonType.SelectionOpenPalette:
                    GASButtonActions.OpenPaletteSelection();
                    break;
                case ButtonType.SelectionOpenSprite:
                    break;
                case ButtonType.SelectionOpenWeapon:
                    break;
                case ButtonType.SelectionOpenEnemy:
                    break;
                case ButtonType.SelectionOpenRoom:
                    GASButtonActions.OpenRoomSelection();
                    break;
                case ButtonType.SelectionOpenTile:
                    GASButtonActions.OpenTileSelection();
                    break;
                case ButtonType.SelectionOpenProjectile:
                    break;
                case ButtonType.SelectionOpenCampaign:
                    GASButtonActions.OpenCampaignSelection();
                    break;
                
                #endregion

                #region Create Assets
                case ButtonType.CreatePack:
                    GASButtonActions.CreatePack();
                    break;
                case ButtonType.CreatePalette:
                    GASButtonActions.CreatePalette();
                    break;
                case ButtonType.CreateSprite:
                    break;
                case ButtonType.CreateWeapon:
                    break;
                case ButtonType.CreateProjectile:
                    break;
                case ButtonType.CreateEnemy:
                    break;
                case ButtonType.CreateTile:
                    GASButtonActions.CreateTile();
                    break;
                case ButtonType.CreateRoom:
                    GASButtonActions.CreateRoom();
                    break;
                case ButtonType.CreateCampaign:
                    GASButtonActions.CreateCampaign();
                    break;
                
                #endregion

                #region Edit Asset Properties
                case ButtonType.EditPackProperties:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT PACK PROPERTIES");
                    GASButtonActions.EditPackProperties(number);
                    break;
                case ButtonType.EditPaletteProperties:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT PALETTE PROPERTIES");
                    GASButtonActions.EditPaletteProperties(number);
                    break;
                case ButtonType.EditSpriteProperties:
                    break;
                case ButtonType.EditWeaponProperties:
                    break;
                case ButtonType.EditEnemyProperties:
                    break;
                case ButtonType.EditRoomProperties:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT ROOM PROPERTIES");
                    GASButtonActions.EditRoomProperties(number);
                    break;
                case ButtonType.EditTileProperties:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT TILE PROPERTIES");
                    GASButtonActions.EditTileProperties(number);
                    break;
                case ButtonType.EditProjectileProperties:
                    break;
                case ButtonType.EditCampaignProperties:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT CAMPAIGN PROPERTIES");
                    GASButtonActions.EditCampaignProperties(number);
                    break;
                
                #endregion

                #region Delete Assets
                case ButtonType.DeletePack:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - REMOVE PACK");
                    GASButtonActions.RemovePack(number);
                    break;
                case ButtonType.DeletePalette:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - REMOVE PALETTE");
                    GASButtonActions.RemovePalette(number);
                    break;
                case ButtonType.DeleteSprite:
                    break;
                case ButtonType.DeleteWeapon:
                    break;
                case ButtonType.DeleteEnemy:
                    break;
                case ButtonType.DeleteRoom:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - REMOVE ROOM");
                    GASButtonActions.RemoveRoom(number);
                    break;
                case ButtonType.DeleteTile:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - REMOVE TILE");
                    GASButtonActions.RemoveTile(number);
                    break;
                case ButtonType.DeleteProjectile:
                    break;
                case ButtonType.DeleteCampaign:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - REMOVE CAMPAIGN");
                    GASButtonActions.RemoveCampaign(number);
                    break;
                
                #endregion

                #region Open Editors
                case ButtonType.EditorOpenPalette:
                    break;
                case ButtonType.EditorOpenSprite:
                    break;
                case ButtonType.EditorOpenWeapon:
                    break;
                case ButtonType.EditorOpenEnemy:
                    break;
                case ButtonType.EditorOpenRoom:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - OPEN ROOM EDITOR");
                    GASButtonActions.OpenRoomEditor(number);
                    break;
                case ButtonType.EditorOpenTile:
                    break;
                case ButtonType.EditorOpenProjectile:
                    break;
                case ButtonType.EditorOpenCampaign:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - OPEN CAMPAIGN EDITOR");
                    GASButtonActions.OpenCampaignEditor(number);
                    break;
                #endregion

                #region Save Editor Changes
                case ButtonType.SaveChangesPalette:
                    break;
                case ButtonType.SaveChangesSprite:
                    break;
                case ButtonType.SaveChangesWeapon:
                    break;
                case ButtonType.SaveChangesEnemy:
                    break;
                case ButtonType.SaveChangesRoom:
                    GASButtonActions.SaveChangesRoom();
                    break;
                case ButtonType.SaveChangesTile:
                    break;
                case ButtonType.SaveChangesProjectile:
                    break;
                case ButtonType.SaveChangesCampaign:
                    GASButtonActions.SaveChangesCampaign();
                    break;
                #endregion

                #region Cancel Editor Changes
                case ButtonType.CancelChangesPalette:
                    break;
                case ButtonType.CancelChangesSprite:
                    break;
                case ButtonType.CancelChangesWeapon:
                    break;
                case ButtonType.CancelChangesEnemy:
                    break;
                case ButtonType.CancelChangesRoom:
                    GASButtonActions.CancelChangesRoom();
                    break;
                case ButtonType.CancelChangesTile:
                    break;
                case ButtonType.CancelChangesProjectile:
                    break;
                case ButtonType.CancelChangesCampaign:
                    GASButtonActions.CancelChangesCampaign();
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
                
                #region Campaign Editor
                case ButtonType.CampaignShowNext:
                    GASButtonActions.CampaignShowNext();
                    break;
                case ButtonType.CampaignShowPrevious:
                    GASButtonActions.CampaignShowPrevious();
                    break;
                case ButtonType.CampaignEditorChangeImportState:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - CHANGE PACK IMPORT STATE");
                    GASButtonActions.ChangeImportStatus(number);
                    break;
                #endregion
                
                case ButtonType.Play:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - PLAY CAMPAIGN");
                    GASButtonActions.PlayCampaign(number);
                    break;
                
                default:
                    throw new InvalidEnumArgumentException("Unknown Button Type.");
            }
        }

        public int Number { get => number; set => number = value; }
    }
}