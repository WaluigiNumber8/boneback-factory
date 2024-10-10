using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [SerializeField] private Button emblemPrefab;
        
        private readonly IList<Sprite> emblems = new List<Sprite>();

        public void Construct(IList<Sprite> values)
        {
            content.KillChildren();
            emblems.Clear();
            foreach (Sprite value in values)
            {
                Button emblem = Instantiate(emblemPrefab, content);
                emblem.GetComponentInChildren<Image>().sprite = value;
                emblems.Add(value);
            }
        }
        
        public Sprite GetEmblem(int index)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(index, emblems, nameof(index));
            return emblems[index];
        }
        
        public int EmblemCount { get => content.childCount; }
        public ReadOnlyCollection<Sprite> Emblems { get => new(emblems); }
    }
}