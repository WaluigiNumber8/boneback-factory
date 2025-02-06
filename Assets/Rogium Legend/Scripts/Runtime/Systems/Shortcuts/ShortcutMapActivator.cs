using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Systems.Shortcuts
{
    /// <summary>
    /// Activates shortcut actions maps on enable.
    /// </summary>
    public class ShortcutMapActivator : MonoBehaviour
    {
        [SerializeField] private bool generalMap = true;
        [SerializeField] private bool drawingEditorsMap;
        [SerializeField] private bool selectionMenuMap;
        [SerializeField] private bool campaignSelectionMap;
        [SerializeField] private bool roomEditorMap;
        [SerializeField] private bool spriteEditorMap;
        [SerializeField] private bool campaignEditorMap;
        
        private InputSystem input;

        private void Awake() => input = InputSystem.GetInstance();

        private void OnEnable()
        {
            input.Shortcuts.ActivateGeneralMap(generalMap);
            input.Shortcuts.ActivateDrawingEditorsMap(drawingEditorsMap);
            input.Shortcuts.ActivateSelectionMenuMap(selectionMenuMap);
            input.Shortcuts.ActivateCampaignSelectionMap(campaignSelectionMap);
            input.Shortcuts.ActivateRoomEditorMap(roomEditorMap);
            input.Shortcuts.ActivateSpriteEditorMap(spriteEditorMap);
            input.Shortcuts.ActivateCampaignEditorMap(campaignEditorMap);
        }
    }
}