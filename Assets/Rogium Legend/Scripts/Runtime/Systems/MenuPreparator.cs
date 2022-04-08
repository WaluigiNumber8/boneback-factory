using Rogium.Systems.GASExtension;
using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Core
{
    /// <summary>
    /// Prepares the Game on Boot.
    /// </summary>
    public class MenuPreparator : MonoBehaviour
    {
        protected void Awake()
        {
            InputSystem.Instance.EnableUIMap();
            GASButtonActions.GameStart();
        }

    }
}