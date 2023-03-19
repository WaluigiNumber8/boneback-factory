using RedRats.Core;
using RedRats.UI;
using RedRats.UI.ModalWindows;
using Rogium.UserInterface.Editors.AssetSelection.PickerVariant;
using Rogium.UserInterface.Editors.Navigation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rogium.UserInterface.Core
{
    /// <summary>
    /// Overseers the game's UI.
    /// </summary>
    public class CanvasOverseer : MonoSingleton<CanvasOverseer>
    {
        [SerializeField] private AssetPickerWindow pickerWindow;
        [SerializeField] private NavigationBar navigationBar;
        
        public AssetPickerWindow PickerWindow { get => pickerWindow; }
        public NavigationBar NavigationBar { get => navigationBar; }
    }
}