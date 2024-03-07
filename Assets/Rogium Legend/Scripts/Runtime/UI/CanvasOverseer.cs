using RedRats.Core;
using Rogium.UserInterface.Editors.Navigation;
using Rogium.UserInterface.Editors.PropertyModalWindows;
using UnityEngine;

namespace Rogium.UserInterface.Core
{
    /// <summary>
    /// Overseers the game's UI.
    /// </summary>
    public class CanvasOverseer : MonoSingleton<CanvasOverseer>
    {
        [SerializeField] private SoundPickerModalWindow soundPickerWindow;
        [SerializeField] private NavigationBar navigationBar;
        
        public SoundPickerModalWindow SoundPickerWindow { get => soundPickerWindow; }
        public NavigationBar NavigationBar { get => navigationBar; }
    }
}