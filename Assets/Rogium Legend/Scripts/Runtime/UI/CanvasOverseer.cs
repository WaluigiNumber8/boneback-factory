using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.UserInterface.Navigation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rogium.UserInterface
{
    /// <summary>
    /// Overseers the game's UI.
    /// </summary>
    public class CanvasOverseer : MonoSingleton<CanvasOverseer>
    {
        [SerializeField] private ModalWindow modalWindow;
        [SerializeField] private NavigationBar navigationBar;
        
        public ModalWindow ModalWindow { get => modalWindow; }
        public NavigationBar NavigationBar { get => navigationBar; }
    }
}