using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Contains a list of emblems.
    /// </summary>
    public class AssetEmblemList : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private Image emblemPrefab;

        public void Construct(IList<Sprite> values)
        {
            content.KillChildren();
            foreach (Sprite value in values)
            {
                Image emblem = Instantiate(emblemPrefab, content);
                emblem.sprite = value;
            }
        }
        
        public Sprite GetEmblem(int index)
        {
            SafetyNet.EnsureIntIsLowerOrEqualTo(index, content.childCount, nameof(index));
            return content.GetChild(index).GetComponent<Image>().sprite;
        }
        
        public int EmblemCount { get => content.childCount; }
    }
}