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
            inputSystem.UI.Click.OnPress += ActionHistorySystem.ForceBeginGrouping;
            inputSystem.UI.ClickAlternative.OnPress += ActionHistorySystem.ForceBeginGrouping;
            inputSystem.UI.Click.OnRelease += ActionHistorySystem.ForceEndGrouping;
            inputSystem.UI.ClickAlternative.OnRelease += ActionHistorySystem.ForceEndGrouping;
        }

    }
}