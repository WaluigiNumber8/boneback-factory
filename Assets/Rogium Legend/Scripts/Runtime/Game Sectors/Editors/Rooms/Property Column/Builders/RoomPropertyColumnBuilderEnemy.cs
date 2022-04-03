using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using TMPro;

namespace Rogium.Editors.Rooms.PropertyColumn
{
    /// <summary>
    /// Builds the Room Property Column for enemies.
    /// </summary>
    public class RoomPropertyColumnBuilderEnemy : UIPropertyContentBuilderBaseColumn1
    {
        public RoomPropertyColumnBuilderEnemy(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build properties.
        /// </summary>
        /// <param name="data">The asset to read from.</param>
        public void Build(AssetData data)
        {
            EmptyContent();

            b.BuildInputField("Damage", data.Parameters.intValue1.ToString(), contentMain, s => data.UpdateIntValue1(int.Parse(s)), true, TMP_InputField.CharacterValidation.Decimal);
        }
        
    }
}