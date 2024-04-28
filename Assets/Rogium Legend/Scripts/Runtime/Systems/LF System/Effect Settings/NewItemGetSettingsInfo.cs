using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel
{
    /// <summary>
    /// Settings for a 'Get new Item' object.
    /// </summary>
    [Serializable]
    public struct NewItemGetSettingsInfo
    {
        [Required] public Transform collectedItem;
        public void UpdateSprite(Sprite sprite) => collectedItem.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }
}