using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Systems.Shortcuts
{
    /// <summary>
    /// Activates shortcut actions maps on enable.
    /// </summary>
    public class ShortcutMapActivator : MonoBehaviour
    {
        [SerializeField] private ShortcutActionMapType activatedMaps = ShortcutActionMapType.General;
        
        private InputSystem input;

        private void Awake() => input = InputSystem.GetInstance();

        private void OnEnable()
        {
            input.Shortcuts.ActivateGeneralMap((activatedMaps & ShortcutActionMapType.General) == ShortcutActionMapType.General);
            input.Shortcuts.ActivateGeneralSelectionMap((activatedMaps & ShortcutActionMapType.GeneralSelection) == ShortcutActionMapType.GeneralSelection);
            input.Shortcuts.ActivateDrawingEditorsMap((activatedMaps & ShortcutActionMapType.DrawingEditors) == ShortcutActionMapType.DrawingEditors);
            input.Shortcuts.ActivateSelectionMenuMap((activatedMaps & ShortcutActionMapType.SelectionMenu) == ShortcutActionMapType.SelectionMenu);
            input.Shortcuts.ActivateCampaignSelectionMap((activatedMaps & ShortcutActionMapType.CampaignSelection) == ShortcutActionMapType.CampaignSelection);
            input.Shortcuts.ActivateRoomEditorMap((activatedMaps & ShortcutActionMapType.RoomEditor) == ShortcutActionMapType.RoomEditor);
            input.Shortcuts.ActivateSpriteEditorMap((activatedMaps & ShortcutActionMapType.SpriteEditor) == ShortcutActionMapType.SpriteEditor);
            input.Shortcuts.ActivateCampaignEditorMap((activatedMaps & ShortcutActionMapType.CampaignEditor) == ShortcutActionMapType.CampaignEditor);
        }
    }
}