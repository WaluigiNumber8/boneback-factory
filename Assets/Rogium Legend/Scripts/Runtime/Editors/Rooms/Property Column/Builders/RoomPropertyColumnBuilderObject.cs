using System;
using System.Collections.Generic;
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
        private IDictionary<string, Action<AssetData>> presets;

        public RoomPropertyColumnBuilderObject(Transform contentMain) : base(contentMain)
        {
            presets = new Dictionary<string, Action<AssetData>>();
            
            presets.Add("001", BuildRoomExit);
        }
        
        /// <summary>
        /// Decide, which object to build.
        /// </summary>
        /// <param name="data">The object data.</param>
        public void Build(AssetData data)
        {
            if (presets.TryGetValue(data.ID, out Action<AssetData> method))
            {
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
            EmptyContent();
            b.BuildDropdown("Direction", Enum.GetNames(typeof(Direction)), data.Parameters.intValue1, contentMain, data.UpdateIntValue1);
        }

        #endregion
        
    }
}