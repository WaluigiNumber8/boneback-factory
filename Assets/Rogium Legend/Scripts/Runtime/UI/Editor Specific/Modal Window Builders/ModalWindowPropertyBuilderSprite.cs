using System;
using Rogium.Core;
using Rogium.Editors.Sprites;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Palette Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderSprite : ModalWindowPropertyBuilderBase
    {
        private readonly SpriteEditorOverseer spriteEditor = SpriteEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null)
        {
            OpenWindow(new SpriteAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Sprite");
        }

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new SpriteAsset.Builder().AsCopy(spriteEditor.CurrentAsset).Build() , () => UpdateAsset(whenConfirm), $"Updating {spriteEditor.CurrentAsset.Title}");

        private void OpenWindow(SpriteAsset sprite, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", sprite.Title, col1, sprite.UpdateTitle);
            b.BuildPlainText("Created by", sprite.Author, col1);
            b.BuildPlainText("Created on", sprite.CreationDate.ToString(), col1);
            
            editedAssetBase = sprite;
        }
        
        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewSprite((SpriteAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            spriteEditor.UpdateAsset((SpriteAsset)editedAssetBase);
            spriteEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
    }
}