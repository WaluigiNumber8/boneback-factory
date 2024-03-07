using RedRats.Core;
using Rogium.UserInterface.Editors.Navigation;
using UnityEngine;

namespace Rogium.UserInterface.Core
{
    /// <summary>
    /// Overseers the game's UI.
    /// </summary>
    public class CanvasOverseer : MonoSingleton<CanvasOverseer>
    {
        [SerializeField] private NavigationBar navigationBar;
        
        public NavigationBar NavigationBar { get => navigationBar; }
    }
}