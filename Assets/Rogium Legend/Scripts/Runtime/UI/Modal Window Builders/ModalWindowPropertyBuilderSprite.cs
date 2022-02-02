using System;
using BoubakProductions.UI;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;

namespace Rogium.UserInterface.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Palette Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderSprite : ModalWindowPropertyBuilder
    {
        private readonly SpriteEditorOverseer spriteEditor;

        public ModalWindowPropertyBuilderSprite()
        {
            spriteEditor = SpriteEditorOverseer.Instance;
        }
        
        public override void OpenForCreate()
        {
            OpenWindow(new SpriteAsset(), CreateAsset, "Creating a new Sprite");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new SpriteAsset(spriteEditor.CurrentAsset), UpdateAsset, $"Updating {spriteEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(SpriteAsset sprite, Action onConfirmAction, string headerText)
        {
            propertyBuilder.BuildInputField("Title", sprite.Title, window.FirstColumnContent, sprite.UpdateTitle);
            propertyBuilder.BuildPlainText("Created by", sprite.Author, window.FirstColumnContent);
            propertyBuilder.BuildPlainText("Created on", sprite.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = sprite;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Blue, "Done", "Cancel", onConfirmAction, true);
        }
        
        protected override void CreateAsset()
        {
            editor.CreateNewSprite((SpriteAsset)editedAssetBase);
            selectionMenu.OpenForSprites();
        }

        protected override void UpdateAsset()
        {
            spriteEditor.UpdateAsset((SpriteAsset)editedAssetBase);
            spriteEditor.CompleteEditing();
            selectionMenu.OpenForSprites();
        }
    }
}