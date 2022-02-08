using BoubakProductions.Safety;
using System;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Systems.IconBuilders;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Overseers everything happening in the room editor.
    /// </summary>
    public class RoomEditorOverseer : IEditorOverseer
    {
        public event Action<RoomAsset> OnAssignAsset;
        public event Action<RoomAsset, int> OnCompleteEditing;

        private IconBuilderAsset iconBuilder;
        
        private RoomAsset currentAsset;
        private int myIndex;

        #region Singleton Pattern
        private static RoomEditorOverseer instance;
        private static readonly object padlock = new object();
        public static RoomEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new RoomEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private RoomEditorOverseer()
        {
            iconBuilder = new IconBuilderAsset();
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
            Sprite newIcon = iconBuilder.Build(currentAsset.TileGrid, EditorDefaults.PixelsPerUnit, PackEditorOverseer.Instance.CurrentPack.Tiles);
            currentAsset.UpdateIcon(newIcon);
            OnCompleteEditing?.Invoke(CurrentAsset, myIndex);
        }

        public RoomAsset CurrentAsset 
        {
            get 
            {
                if (currentAsset == null) throw new MissingReferenceException("Current Room has not been set. Did you forget to activate the editor?");
                return this.currentAsset;
            } 
        }
    }
}