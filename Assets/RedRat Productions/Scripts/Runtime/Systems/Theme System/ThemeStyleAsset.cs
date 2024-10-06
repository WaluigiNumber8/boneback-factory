using System;
using RedRats.UI.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.Themes
{
    [CreateAssetMenu(fileName = "New Theme Asset", menuName = "RedRat Productions/Editor Theme", order = 0)]
    public class ThemeStyleAsset : ScriptableObject
    {
        [SerializeField] private ElementsInfo elements;
        [SerializeField] private InteractablesInfo interactables;
        [SerializeField] private IconsInfo icons;
        [SerializeField] private FontsInfo fonts;

        public ElementsInfo Elements { get => elements; }
        public InteractablesInfo Interactables { get => interactables; }
        public IconsInfo Icons { get => icons; }
        public FontsInfo Fonts { get => fonts; }
        
        [Serializable]
        public struct ElementsInfo
        {
            [PreviewField(60)] public Sprite dropdownHeader;
            [PreviewField(60)] public Sprite dropdownBackground;
            [PreviewField(60)] public Sprite dropdownArrow;
            [PreviewField(60)] public Sprite dropdownCheckmark;
            [PreviewField(60)] public Sprite toggleBorder;
            [PreviewField(60)] public Sprite toggleCheckmark;
            [PreviewField(60)] public Sprite sliderBackground;
            [PreviewField(60)] public Sprite sliderHandle;
            [PreviewField(60)] public Sprite editorBackground;
            [PreviewField(60)] public Sprite modalWindowBackground;
        }
        
        [Serializable]
        public struct InteractablesInfo
        {
            [BoxGroup] public InteractableSpriteInfo buttonMenu;
            [BoxGroup] public InteractableSpriteInfo buttonCard;
            [BoxGroup] public InteractableSpriteInfo buttonTool;
            [BoxGroup] public InteractableSpriteInfo inputField;
            [BoxGroup] public InteractableSpriteInfo dropdownItem;
            [BoxGroup] public InteractableSpriteInfo toggle;
            [BoxGroup] public InteractableSpriteInfo slider;
            [BoxGroup] public InteractableSpriteInfo assetField;
            [BoxGroup] public InteractableSpriteInfo colorField;
            [BoxGroup] public InteractableSpriteInfo scrollbarHandle;
            [BoxGroup] public InteractableSpriteInfo assetCard;
        }

        [Serializable]
        public struct IconsInfo
        {
            [PreviewField(60)] public Sprite play;
            [PreviewField(60)] public Sprite stop;
        }
        
        [Serializable]
        public struct FontsInfo
        {
            public FontInfo general;
            public FontInfo inputted;
            public FontInfo header;
            public FontInfo assetCardInfo;
        }
    }
}