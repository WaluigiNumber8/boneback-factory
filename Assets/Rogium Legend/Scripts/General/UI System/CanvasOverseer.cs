using BoubakProductions.Core;
using BoubakProductions.UI;
using UnityEngine;

namespace RogiumLegend.Global.UISystem
{
    /// <summary>
    /// Overseers the game's UI.
    /// </summary>
    public class CanvasOverseer : MonoSingleton<CanvasOverseer>
    {
        [SerializeField] private ModalWindow modalWindow;

        public ModalWindow ModalWindow { get => modalWindow; }
    }
}