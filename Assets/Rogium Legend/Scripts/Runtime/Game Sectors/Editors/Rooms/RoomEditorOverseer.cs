using RedRats.Safety;
using System;
using RedRats.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Overseers everything happening in the room editor.
    /// </summary>
    public sealed class RoomEditorOverseer : Singleton<RoomEditorOverseer>, IEditorOverseer
    {
        public event Action<RoomAsset> OnAssignAsset;
        public event Action<RoomAsset, int> OnCompleteEditing;

        private readonly SpriteDrawer drawer;
        
        private RoomAsset currentAsset;
        private int myIndex;

        private RoomEditorOverseer()
        {
            drawer = new SpriteDrawer(EditorConstants.RoomSize, new Vector2Int(EditorConstants.SpriteSize, EditorConstants.SpriteSize), EditorConstants.SpriteSize);
        }

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="room">The room that will be edited.</param>
        /// <param name="index">The position in the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(RoomAsset room, int index, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(room, "Assigned Room");
            currentAsset = new RoomAsset(room);
            myIndex = index;

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(room);
        }

        /// <summary>
        /// Updates the room asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(RoomAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new RoomAsset(updatedAsset);
        }
        
        public void CompleteEditing()
        {
            Sprite newIcon = drawer.Build(currentAsset.TileGrid, PackEditorOverseer.Instance.CurrentPack.Tiles);
            currentAsset.UpdateIcon(newIcon);
            OnCompleteEditing?.Invoke(CurrentAsset, myIndex);
        }

        public RoomAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Room has not been set. Did you forget to activate the editor?");
                return currentAsset;
            } 
        }
    }
}