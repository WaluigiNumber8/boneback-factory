using System;
using Rogium.Editors.Core;
using TMPro;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Represents an asset in the form of a card.
    /// </summary>
    public class AssetCardControllerV2 : MonoBehaviour
    {
        [SerializeField] private UIInfo ui;
        
        public void Construct(IAsset asset, AssetCardData data)
        {
            ui.title.text = asset.Title;
        }

        public string Title { get => ui.title.text; }

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
        }
    }
}