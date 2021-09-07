using BoubakProductions.Core;
using BoubakProductions.Safety;
using RogiumLegend.Core;
using RogiumLegend.Editors;
using RogiumLegend.Editors.Core;
using RogiumLegend.Editors.PackData;
using RogiumLegend.Global.MenuSystem.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RogiumLegend.Global.MenuSystem.AssetSelection
{
    /// <summary>
    /// Is responsible for controlling the Asset Selection Menu, and switching out content as needed in it.
    /// </summary>
    [RequireComponent(typeof(ObjectSwitcher))]
    public class AssetSelectionOverseer : MonoSingleton<AssetSelectionOverseer>
    {
        private EditorOverseer editor;
        private LibraryOverseer lib;
        private ObjectSwitcher switcher;
        
        [SerializeField] private Image addButtonImage;
        [Space]
        [SerializeField] private LayoutInfo layouts;
        [SerializeField] private AssetSelectionMenuInfo selectionMenus;

        private IList<IAssetHolder> assets = new List<IAssetHolder>();
        private AssetType lastTypeOpen;

        protected override void Awake()
        {
            base.Awake();
            editor = EditorOverseer.Instance;
            lib = LibraryOverseer.Instance;
            switcher = GetComponent<ObjectSwitcher>();
            lastTypeOpen = AssetType.None;
        }

        /// <summary>
        /// Opens the selection menu for weapon assets.
        /// </summary>
        public void OpenForWeapons()
        {
            throw new System.NotImplementedException();
        }

        //TODO - Instead of the SelectionMenuAsset, make the default layout load from the options save file.

        /// <summary>
        /// Opens the selection menu for room assets.
        /// </summary>
        public void OpenForRooms()
        {
            addButtonImage.sprite = selectionMenus.room.addButtonSprite;
            Setup(AssetType.Room,
                  layouts.list,
                  selectionMenus.room.assetObject,
                  (IList<IAsset>)editor.CurrentPack.Rooms);
        }

        /// <summary>
        /// Opens the selection menu for pack assets.
        /// </summary>
        public void OpenForPacks()
        {
            addButtonImage.sprite = selectionMenus.pack.addButtonSprite;
            Setup(AssetType.Pack,
                  layouts.grid,
                  selectionMenus.pack.assetObject,
                  lib.Library.ToList<IAsset>());
        }

        /// <summary>
        /// Sets up the selection menu.
        /// </summary>
        private void Setup(AssetType type, SelectionMenuLayout layout, GameObject assetObject, IList<IAsset> assetList)
        {
            switcher.DeselectAllExcept(layout.Menu);

            //Do not refill, if menu is the same.
            if (type == lastTypeOpen && type != AssetType.None)
            {
                if (assetList.Count != assets.Count)
                {
                    return;
                }
            }

            RefillMenu(type, layout.Content, assetObject, assetList);
            lastTypeOpen = type;
        }

        /// <summary>
        /// Fills the selection menu canvas with asset holder objects.
        /// </summary>
        private void RefillMenu(AssetType type, GameObject content, GameObject assetObject, IList<IAsset> assetList)
        {
            SafetyNet.EnsureIsNotNull(assetObject.GetComponent<IAssetHolder>(), "IAssetHolder in RefillMenu");
            assets.Clear();
            for (int i = 0; i < assetList.Count; i++)
            {
                IAssetHolder holder = Instantiate(assetObject, content.transform).GetComponent<IAssetHolder>();
                holder.Construct(type, i, assetList[i]);
                assets.Add(holder);
            }
        }

        /// <summary>
        /// Get amount of assets showcased by the menu.
        /// </summary>
        public int AssetCount => assets.Count;

        public GameObject ListMenu { get => layouts.list.Menu; }
        public GameObject GridMenu { get => layouts.grid.Menu; }
        
        [System.Serializable]
        public struct AssetSelectionMenuInfo
        {
            public SelectionMenuAsset pack;
            public SelectionMenuAsset weapon;
            public SelectionMenuAsset enemy;
            public SelectionMenuAsset room;
            public SelectionMenuAsset palette;
            public SelectionMenuAsset sprite;
        }

        [System.Serializable]
        public struct LayoutInfo
        {
            public SelectionMenuLayout grid;
            public SelectionMenuLayout list;
        }
    }
}