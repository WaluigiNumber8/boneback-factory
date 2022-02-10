using BoubakProductions.UI.Core;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
{
    /// <summary>
    /// Contains information that can be changed for different themes.
    /// </summary>
    [CreateAssetMenu(fileName = "New Editor Theme", menuName = "Rogium Legend/Editor Theme Style")]
    public class ThemeStyleAsset : ScriptableObject
    {
        [SerializeField] private Sprite[] elements;
        [SerializeField] private InteractableInfo[] interactables;
        [SerializeField] private FontInfo[] fonts;
        
        public Sprite[] Elements { get => elements; }
        public InteractableInfo[] Interactables { get => interactables; }
        public FontInfo[] Fonts { get => fonts; }
    }
}