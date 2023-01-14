using System;
using RedRats.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Rooms;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Controls the content of the Selection Menu Info Column.
    /// </summary>
    public class SelectionInfoColumn : MonoBehaviour
    {
        [SerializeField] private UIInfo ui;

        private SelectionInfoColumnPropertyBuilderRoom builderRoom;

        private void Awake() => builderRoom = new SelectionInfoColumnPropertyBuilderRoom(ui.content);

        /// <summary>
        /// Prepare the Info Column for an asset.
        /// </summary>
        /// <param name="asset">The asset to prepare the column for.</param>
        public void Construct(IAsset asset)
        {
            ui.previewImage.sprite = asset.Icon;
            
            if (asset.GetType() == typeof(RoomAsset)) builderRoom.Build((RoomAsset)asset);
        }
        
        /// <summary>
        /// Empties out the Info Column.
        /// </summary>
        public void ConstructEmpty()
        {
            ui.previewImage.sprite = EditorConstants.RoomIcon;
            ui.content.KillChildren();
        }
        
        [Serializable]
        public struct UIInfo
        {
            public Image previewImage;
            public Transform content;
        }
    }
}