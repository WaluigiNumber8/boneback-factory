using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Editors.Packs;
using Rogium.Editors.Weapons;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.Rooms.PropertyColumn
{
    /// <summary>
    /// Builds the Room Property Column for different objects.
    /// </summary>
    public class RoomPropertyColumnBuilderObject : UIPropertyContentBuilderBaseColumn1
    {
        private readonly IDictionary<string, Action<AssetData>> presets;

        public RoomPropertyColumnBuilderObject(Transform contentMain) : base(contentMain)
        {
            presets = new Dictionary<string, Action<AssetData>>();
            
            presets.Add("001", BuildRoomExit);
            presets.Add("002", BuildStartingPoint);
            presets.Add("003", BuildWeaponDrop);
            presets.Add("004", BuildChestGold);
        }
        
        /// <summary>
        /// Decide, which object to build.
        /// </summary>
        /// <param name="data">The object data.</param>
        public void Build(AssetData data)
        {
            if (presets.TryGetValue(data.ID, out Action<AssetData> method))
            {
                Clear();
                method?.Invoke(data);
                return;
            }

            throw new InvalidOperationException($"No method is registered under ID '{data.ID}' in the {nameof(RoomPropertyColumnBuilderObject)}.");
        }

        #region Builder Methods

        /// <summary>
        /// Builds the Room Properties Column for the Room Exit Object.
        /// </summary>
        /// <param name="data">The data to use.</param>
        private void BuildRoomExit(AssetData data)
        {
            b.BuildDropdown("Move", Enum.GetNames(typeof(DirectionType)), data.Parameters.intValue1, contentMain, data.UpdateIntValue1);
            b.BuildDropdown("Next Room", Enum.GetNames(typeof(RoomType)), data.Parameters.intValue2, contentMain, data.UpdateIntValue2);
        }

        /// <summary>
        /// Builds the Room Properties Column for the Room Start Object.
        /// </summary>
        /// <param name="data">The data to use.</param>
        private void BuildStartingPoint(AssetData data)
        {
            b.BuildDropdown("Move", Enum.GetNames(typeof(DirectionType)), data.Parameters.intValue1, contentMain, data.UpdateIntValue1);
        }
        
        /// <summary>
        /// Builds the Room Properties Column for the Weapon Drop Object.
        /// </summary>
        /// <param name="data">The data to use.</param>
        private void BuildWeaponDrop(AssetData data)
        {
            IList<WeaponAsset> weapons = PackEditorOverseer.Instance.CurrentPack.Weapons;
            bool noWeaponsExist = (weapons == null || weapons.Count == 0);
            IAsset startValue =  weapons.FindValueFirstOrReturnFirstOrDefault(data.Parameters.stringValue1);

            if (!noWeaponsExist) data.UpdateStringValue1(startValue.ID);
            b.BuildAssetField("Weapon", AssetType.Weapon, startValue, contentMain, asset => data.UpdateStringValue1(asset.ID), null, noWeaponsExist);
            b.BuildToggle("Player only", data.Parameters.boolValue1, contentMain, data.UpdateBoolValue1);
        }
        
        /// <summary>
        /// Builds the Room Properties Column for the Golden Chest Object.
        /// </summary>
        /// <param name="data">The data to use.</param>
        private void BuildChestGold(AssetData data)
        {
        }
        #endregion
        
    }
}