using BoubakProductions.Core;
using UnityEngine;

namespace RogiumLegend.Global.UISystem.UI
{
    /// <summary>
    /// Contains references for various UI components of the Editor.
    /// </summary>
    public class UIEditorContainer : MonoSingleton<UIEditorContainer>
    {
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject packNavigationBar;

        public GameObject PackInfoHeader { get => packNavigationBar; }
        public GameObject Background { get => background;}
    }
}