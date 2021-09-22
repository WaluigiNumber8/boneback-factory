using BoubakProductions.Safety;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Global.UISystem.Interactables
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

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            switch (action)
            {
                case ButtonType.DoNothing:
                    Debug.LogError("This button Currently does nothing...");
                    break;

                case ButtonType.ReturnToAssetTypeSelection:
                    break;
                case ButtonType.ReturnToMainMenu:
                    GASButtonActions.ReturnToMainMenu();
                    break;

                case ButtonType.SelectionOpenAssetType:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT PACK");
                    GASButtonActions.OpenEditor(number);
                    break;
                case ButtonType.SelectionOpenPack:
                    GASButtonActions.OpenPackSelection();
                    break;
                case ButtonType.SelectionOpenPalette:
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
                    break;
                case ButtonType.SelectionOpenProjectile:
                    break;

                case ButtonType.CreatePack:
                    GASButtonActions.CreatePack();
                    break;
                case ButtonType.CreateRoom:
                    GASButtonActions.CreateRoom();
                    break;
                case ButtonType.CreatePalette:
                    break;
                case ButtonType.CreateSprite:
                    break;
                case ButtonType.CreateWeapon:
                    break;
                case ButtonType.CreateEnemy:
                    break;
                case ButtonType.CreateTile:
                    break;
                case ButtonType.CreateProjectile:
                    break;

                case ButtonType.EditPackProperties:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - EDIT PACK PROPERTIES");
                    GASButtonActions.EditPackProperties(number);
                    break;
                case ButtonType.EditPaletteProperties:
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
                    break;
                case ButtonType.EditProjectileProperties:
                    break;

                case ButtonType.DeletePack:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - REMOVE PACK");
                    GASButtonActions.RemovePack(number);
                    break;
                case ButtonType.DeletePalette:
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
                    break;
                case ButtonType.DeleteProjectile:
                    break;

                case ButtonType.EditorOpenPalette:
                    break;
                case ButtonType.EditorOpenSprite:
                    break;
                case ButtonType.EditorOpenWeapon:
                    break;
                case ButtonType.EditorOpenEnemy:
                    break;
                case ButtonType.EditorOpenRoom:
                    break;
                case ButtonType.EditorOpenTile:
                    break;
                case ButtonType.EditorOpenProjectile:
                    break;
                default:
                    throw new InvalidEnumArgumentException("Unknown Button Type.");
            }
        }

        public int Number { get => number; set => number = value; }
    }
}