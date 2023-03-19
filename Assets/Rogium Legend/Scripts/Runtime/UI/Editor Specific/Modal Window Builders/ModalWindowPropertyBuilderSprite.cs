using System;
using RedRats.UI;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
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
            b.BuildInputField("Title", sprite.Title, windowColumn1, sprite.UpdateTitle);
            b.BuildPlainText("Created by", sprite.Author, windowColumn1);
            b.BuildPlainText("Created on", sprite.CreationDate.ToString(), windowColumn1);
            
            editedAssetBase = sprite;
            Open(new PropertyWindowInfo(headerText, PropertyLayoutType.Column1, ThemeType.Pink, "Done", "Cancel", onConfirmAction));
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