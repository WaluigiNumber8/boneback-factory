using Rogium.Editors.Core;
using Rogium.Options.Core;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Core
{
    /// <summary>
    /// Prepares the Game on Boot.
    /// </summary>
    public class MenuPreparator : MonoBehaviour
    {
        private void Awake() => ActionHistorySystem.ClearHistory();

        private void Start()
        {
            InputSystem input = InputSystem.GetInstance();
            input.SwitchToMenuMaps();
            ExternalLibraryOverseer.Instance.RefreshOptions();
            OptionsMenuOverseer.Instance.CompleteEditing();
            
            //Force grouping on click/right click
            input.UI.Select.OnPress += ActionHistorySystem.ForceBeginGrouping;
            input.UI.ContextSelect.OnPress += ActionHistorySystem.ForceBeginGrouping;
            input.UI.Select.OnRelease += ActionHistorySystem.ForceEndGrouping;
            input.UI.ContextSelect.OnRelease += ActionHistorySystem.ForceEndGrouping;
        }

    }
}