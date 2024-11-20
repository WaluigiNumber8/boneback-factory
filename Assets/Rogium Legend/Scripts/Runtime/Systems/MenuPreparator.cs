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
        private void Awake()
        {
            ActionHistorySystem.ClearHistory();
        }

        private void Start()
        {
            InputSystem inputSystem = InputSystem.GetInstance();

            inputSystem.EnableUIMap();
            
            //Force grouping on click/right click
            inputSystem.UI.Select.OnPress += ActionHistorySystem.ForceBeginGrouping;
            inputSystem.UI.ContextSelect.OnPress += ActionHistorySystem.ForceBeginGrouping;
            inputSystem.UI.Select.OnRelease += ActionHistorySystem.ForceEndGrouping;
            inputSystem.UI.ContextSelect.OnRelease += ActionHistorySystem.ForceEndGrouping;
        }

    }
}