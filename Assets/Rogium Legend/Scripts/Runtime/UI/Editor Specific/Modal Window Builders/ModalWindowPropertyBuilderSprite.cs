using System;
using Rogium.Editors.Sprites;
using UnityEngine;

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

        private void OpenWindow(SpriteAsset sprite, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", sprite.Title, col1, sprite.UpdateTitle);
            b.BuildPlainText("Created by", sprite.Author, col1);
            b.BuildPlainText("Created on", sprite.CreationDate.ToString(), col1);
            
            editedAssetBase = sprite;
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