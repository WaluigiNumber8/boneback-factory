using RedRats.Safety;
using Rogium.Editors.Packs;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Controls the property column for a selected pack.
    /// </summary>
    public class CampaignSelectedPackPropertyController : MonoBehaviour
    {
        public event Action<PackImportInfo> OnUpdateData;
        
        [SerializeField] private UIInfo ui;
        
        private PackAsset currentPack;
        private PackImportInfo currentImportInfo;

        private void OnEnable()
        {
            ui.weaponsToggle.onValueChanged.AddListener(UpdateImportWeapons);
            ui.enemiesToggle.onValueChanged.AddListener(UpdateImportEnemies);
            ui.roomsToggle.onValueChanged.AddListener(UpdateImportRooms);
        }

        private void OnDisable()
        {
            ui.weaponsToggle.onValueChanged.RemoveListener(UpdateImportWeapons);
            ui.enemiesToggle.onValueChanged.RemoveListener(UpdateImportEnemies);
            ui.roomsToggle.onValueChanged.RemoveListener(UpdateImportRooms);
        }

        /// <summary>
        /// Assign a new pack to the property column.
        /// </summary>
        /// <param name="pack">The pack to assign.</param>
        /// <param name="importInfo">The import information about the pack.</param>
        public void AssignAsset(PackAsset pack, PackImportInfo importInfo)
        {
            SafetyNet.EnsureIsNotNull(pack, "Current Pack");
            currentPack = pack;
            currentImportInfo = importInfo;
            
            RefreshUI();
        }

        /// <summary>
        /// Refreshes the UI.
        /// </summary>
        private void RefreshUI()
        {
            ui.titleText.text = currentPack.Title;
            ui.icon.sprite = currentPack.Icon;
            ui.descriptionText.text = currentPack.PackInfo.Description;
            ui.weaponsToggle.isOn = currentImportInfo.weapons;
            ui.enemiesToggle.isOn = currentImportInfo.enemies;
            ui.roomsToggle.isOn = currentImportInfo.rooms;
        }

        /// <summary>
        /// Changes the import status for weapons.
        /// </summary>
        private void UpdateImportWeapons(bool value)
        {
            currentImportInfo.weapons = value;
            UpdateData();
        }
        
        /// <summary>
        /// Changes the import status for enemies.
        /// </summary>
        private void UpdateImportEnemies(bool value)
        {
            currentImportInfo.enemies = value;
            UpdateData();
        }
        
        /// <summary>
        /// Changes the import status for rooms.
        /// </summary>
        private void UpdateImportRooms(bool value)
        {
            currentImportInfo.rooms = value;
            UpdateData();
        }

        /// <summary>
        /// Sends out a signal about updated data.
        /// </summary>
        private void UpdateData()
        {
            OnUpdateData?.Invoke(currentImportInfo);
        }
        
        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI titleText;
            public Image icon;
            public TextMeshProUGUI descriptionText;
            public Toggle weaponsToggle;
            public Toggle enemiesToggle;
            public Toggle roomsToggle;
        }
    }
}