using BoubakProductions.UI.Core;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
{
    [CreateAssetMenu(fileName = "New Theme Asset", menuName = "Boubak Productions/Editor Theme", order = 0)]
    public class ThemeStyleAsset : ScriptableObject
    {
        [SerializeField] private ElementsInfo elements;
        [SerializeField] private InteractablesInfo interactables;
        [SerializeField] private FontsInfo fonts;

        public ElementsInfo Elements { get => elements; }
        public InteractablesInfo Interactables { get => interactables; }
        public FontsInfo Fonts { get => fonts; }
        
        [System.Serializable]
        public struct ElementsInfo
        {
            public Sprite dropdownHeader;
            public Sprite dropdownBackground;
            public Sprite dropdownArrow;
            public Sprite toggleBorder;
            public Sprite toggleCheckmark;
            public Sprite sliderBackground;
            public Sprite sliderHandle;
            public Sprite editorBackground;
            public Sprite modalWindowBackground;
        }
        
        [System.Serializable]
        public struct InteractablesInfo
        {
            public InteractableInfo buttonMenu;
            public InteractableInfo buttonCard;
            public InteractableInfo buttonTool;
            public InteractableInfo inputField;
            public InteractableInfo toggle;
            public InteractableInfo assetField;
            public InteractableInfo slider;
        }

        [System.Serializable]
        public struct FontsInfo
        {
            public FontInfo general;
            public FontInfo inputted;
            public FontInfo header;
        }
    }
}