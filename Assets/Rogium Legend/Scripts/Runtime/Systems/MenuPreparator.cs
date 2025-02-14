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
            InputSystem.GetInstance().SwitchToMenuMaps();
            ExternalLibraryOverseer.Instance.ActivateOptionsEditor();
            ExternalLibraryOverseer.Instance.RefreshOptions();
            OptionsMenuOverseer.Instance.CompleteEditing();
        }

    }
}