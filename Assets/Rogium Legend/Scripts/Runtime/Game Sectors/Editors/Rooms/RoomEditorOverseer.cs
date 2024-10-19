using RedRats.Safety;
using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;
using Rogium.Systems.IconBuilders;
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

        private RoomEditorOverseer() => drawer = new SpriteDrawer(EditorDefaults.Instance.RoomSize, new Vector2Int(EditorDefaults.Instance.SpriteSize, EditorDefaults.Instance.SpriteSize), EditorDefaults.Instance.SpriteSize, false);

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="asset">The room that will be edited.</param>
        /// <param name="index">The position in the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(RoomAsset asset, int index, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Room");
            currentAsset = new RoomAsset.Builder().AsCopy(asset).Build();
            myIndex = index;

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(asset);
        }

        /// <summary>
        /// Updates the room asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(RoomAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentAsset, "Currently active asset.");
            currentAsset = new RoomAsset.Builder().AsCopy(updatedAsset).Build();
        }
        
        public void CompleteEditing()
        {
            Sprite banner = drawer.Draw(currentAsset.TileGrid, PackEditorOverseer.Instance.CurrentPack.Tiles);
            banner = drawer.Draw(currentAsset.DecorGrid, PackEditorOverseer.Instance.CurrentPack.Tiles, banner);
            banner.name = currentAsset.Title;

            IDictionary<string,TileAsset> tiles = PackEditorOverseer.Instance.CurrentPack.Tiles.ToDictionary(x => x.ID, x => x);
            Sprite icon = IconBuilder.DrawLowResIconFrom(currentAsset.TileGrid, tiles);
            icon = IconBuilder.DrawLowResIconFrom(currentAsset.DecorGrid, tiles, icon);
            currentAsset.UpdateIcon(icon);
            currentAsset.UpdateBanner(banner);
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