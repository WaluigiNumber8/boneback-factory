using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Controls the content of the Selection Menu Info Column.
    /// </summary>
    public class SelectionInfoColumn : MonoBehaviour
    {
        [SerializeField] private UIInfo ui;

        public void Construct(IAsset asset)
        {
            ui.previewImage.sprite = asset.Icon;
        }

        public void ConstructEmpty()
        {
            ui.previewImage.sprite = EditorDefaults.RoomIcon;
        }
        
        [Serializable]
        public struct UIInfo
        {
            public Image previewImage;
        }
    }
}