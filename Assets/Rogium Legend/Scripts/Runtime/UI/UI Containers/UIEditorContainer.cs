using RedRats.Core;
using UnityEngine;

namespace Rogium.UserInterface.Containers
{
    /// <summary>
    /// Contains references for various UI components of the Editor.
    /// </summary>
    public class UIEditorContainer : MonoSingleton<UIEditorContainer>
    {
        [SerializeField] private GameObject background;

        public GameObject Background { get => background;}
    }
}