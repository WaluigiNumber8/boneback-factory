using System;
using Rogium.Editors.Core;
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

        public void Construct(AssetBase asset)
        {
            ui.previewImage.sprite = asset.Icon;
        }
        
        [Serializable]
        public struct UIInfo
        {
            public Image previewImage;
        }
    }
}