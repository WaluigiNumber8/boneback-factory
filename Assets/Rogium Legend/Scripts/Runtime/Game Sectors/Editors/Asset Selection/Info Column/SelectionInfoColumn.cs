using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Rooms;
using Rogium.UserInterface.Interactables.Properties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Controls the content of the Selection Menu Info Column.
    /// </summary>
    public class SelectionInfoColumn : MonoBehaviour
    {
        [SerializeField] private UIInfo ui;

        private SelectionInfoColumnPropertyBuilderRoom builderRoom;

        private void Awake()
        {
            builderRoom = new SelectionInfoColumnPropertyBuilderRoom(ui.content);
        }
        
        /// <summary>
        /// Prepare the Info Column for an asset.
        /// </summary>
        /// <param name="asset">The asset to prepare the column for.</param>
        public void Construct(IAsset asset)
        {
            ui.title.text = asset.Title;
            PrepareIcon(asset.Icon);
            if (asset is RoomAsset room)
            {
                PrepareBanner(room.Icon);
                builderRoom.Build(room);
            }
        }
        
        /// <summary>
        /// Empties out the Info Column.
        /// </summary>
        public void ConstructEmpty()
        {
            ui.title.text = string.Empty;
            ui.previewIconContainer.SetActive(false);
            ui.previewBannerContainer.SetActive(false);
            ui.content.KillChildren();
        }
        
        public InteractablePropertyBase<T> GetProperty<T>(int i)
        {
            SafetyNet.EnsureIntIsLowerOrEqualTo(i, ui.content.childCount, nameof(i));
            SafetyNet.EnsureIntIsInRange(i, 0, ui.content.childCount, nameof(i));
            return ui.content.GetChild(i).GetComponent<InteractablePropertyBase<T>>();
        }
        
        private void PrepareIcon(Sprite sprite)
        {
            ui.previewBannerContainer.SetActive(false);
            ui.previewIcon.sprite = sprite;
            ui.previewIconContainer.SetActive(true);
        }
        
        private void PrepareBanner(Sprite sprite)
        {
            ui.previewIconContainer.SetActive(false);
            ui.previewBanner.sprite = sprite;
            ui.previewBannerContainer.SetActive(true);
        }
        
        public int PropertiesCount => ui.content.childCount;
        public string Title { get => ui.title.text; }
        public Sprite Icon { get => ui.previewIcon.sprite; }
        public Sprite BannerIcon { get => ui.previewBanner.sprite; }
        
        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public GameObject previewBannerContainer;
            public Image previewBanner;
            public GameObject previewIconContainer;
            public Image previewIcon;
            public RectTransform content;
        }
    }
}