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
        [Range(0f, 2f)] public float hideDelay;
        public void UpdateSprite(Sprite sprite) => collectedItem.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }
}