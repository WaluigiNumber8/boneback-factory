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
        [SerializeField] private int index = -1;
        
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
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PACK");
                    GASButtonActions.OpenEditor(index);
                    break;
                case ButtonType.SelectionOpenPack:
                    GASButtonActions.OpenSelectionPack();
                    break;
                case ButtonType.SelectionOpenPalette:
                    GASButtonActions.OpenSelectionPalette();
                    break;
                case ButtonType.SelectionOpenSprite:
                    GASButtonActions.OpenSelectionSprite();
                    break;
                case ButtonType.SelectionOpenWeapon:
                    break;
                case ButtonType.SelectionOpenEnemy:
                    break;
                case ButtonType.SelectionOpenRoom:
                    GASButtonActions.OpenSelectionRoom();
                    break;
                case ButtonType.SelectionOpenTile:
                    GASButtonActions.OpenSelectionTile();
                    break;
                case ButtonType.SelectionOpenProjectile:
                    break;
                case ButtonType.SelectionOpenCampaign:
                    GASButtonActions.OpenSelectionCampaign();
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
                    GASButtonActions.CreateSprite();
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
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT PACK PROPERTIES");
                    GASButtonActions.EditPropertiesPack(index);
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
                    break;
                case ButtonType.EditEnemyProperties:
                    break;
                case ButtonType.EditRoomProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT ROOM PROPERTIES");
                    GASButtonActions.EditPropertiesRoom(index);
                    break;
                case ButtonType.EditTileProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT TILE PROPERTIES");
                    GASButtonActions.EditPropertiesTile(index);
                    break;
                case ButtonType.EditProjectileProperties:
                    break;
                case ButtonType.EditCampaignProperties:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - EDIT CAMPAIGN PROPERTIES");
                    GASButtonActions.EditPropertiesCampaign(index);
                    break;
                
                #endregion

                #region Delete Assets
                case ButtonType.DeletePack:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE PACK");
                    GASButtonActions.DeletePack(index);
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
                    break;
                case ButtonType.DeleteEnemy:
                    break;
                case ButtonType.DeleteRoom:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE ROOM");
                    GASButtonActions.DeleteRoom(index);
                    break;
                case ButtonType.DeleteTile:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE TILE");
                    GASButtonActions.DeleteTile(index);
                    break;
                case ButtonType.DeleteProjectile:
                    break;
                case ButtonType.DeleteCampaign:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - DELETE CAMPAIGN");
                    GASButtonActions.DeleteCampaign(index);
                    break;
                
                #endregion

                #region Open Editors
                case ButtonType.EditorOpenPalette:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR PALETTE");
                    GASButtonActions.OpenEditorPalette(index);
                    break;
                case ButtonType.EditorOpenSprite:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR SPRITE");
                    GASButtonActions.OpenEditorSprite(index);
                    break;
                case ButtonType.EditorOpenWeapon:
                    break;
                case ButtonType.EditorOpenEnemy:
                    break;
                case ButtonType.EditorOpenRoom:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR ROOM");
                    GASButtonActions.OpenEditorRoom(index);
                    break;
                case ButtonType.EditorOpenTile:
                    break;
                case ButtonType.EditorOpenProjectile:
                    break;
                case ButtonType.EditorOpenCampaign:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - OPEN EDITOR CAMPAIGN");
                    GASButtonActions.OpenEditorCampaign(index);
                    break;
                #endregion

                #region Save Editor Changes
                case ButtonType.SaveChangesPalette:
                    GASButtonActions.SaveChangesPalette();
                    break;
                case ButtonType.SaveChangesSprite:
                    GASButtonActions.SaveChangesSprite();
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
                    GASButtonActions.CancelChangesPalette();
                    break;
                case ButtonType.CancelChangesSprite:
                    GASButtonActions.CancelChangesSprite();
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
                
                case ButtonType.Play:
                    SafetyNet.EnsureIntIsNotEqual(index, -1, "BUTTON INTERACTION - PLAY CAMPAIGN");
                    GASButtonActions.PlayCampaign(index);
                    break;
                
                default:
                    throw new InvalidEnumArgumentException("Unknown Button Type.");
            }
        }

        public int Index { get => index; set => index = value; }
    }
}