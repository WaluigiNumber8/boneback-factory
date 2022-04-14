using System;
using System.Collections.Generic;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
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
        /// Builds the Room Properties Column for the Room Exit Interactable Object.
        /// </summary>
        /// <param name="data">The data to use.</param>
        private void BuildRoomExit(AssetData data)
        {
            b.BuildDropdown("Direction", Enum.GetNames(typeof(DirectionType)), data.Parameters.intValue1, contentMain, data.UpdateIntValue1);
            b.BuildDropdown("Next Room", Enum.GetNames(typeof(RoomType)), data.Parameters.intValue2, contentMain, data.UpdateIntValue2);
        }

        private void BuildStartingPoint(AssetData data)
        {
            b.BuildDropdown("Direction", Enum.GetNames(typeof(DirectionType)), data.Parameters.intValue1, contentMain, data.UpdateIntValue1);
        }
        
        private void BuildWeaponDrop(AssetData data)
        {
        }
        
        private void BuildChestGold(AssetData data)
        {
        }
        #endregion
        
    }
}