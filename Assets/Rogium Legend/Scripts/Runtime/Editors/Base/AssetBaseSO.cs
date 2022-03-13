using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all internal assets.
    /// </summary>
    public abstract class AssetBaseSO : ScriptableObject, IAsset
    {
        [SerializeField] private string id;
        [SerializeField] private string title;
        [SerializeField] private Sprite icon;
        
        private string author;

        private void Awake() => author = EditorDefaults.AuthorGame;

        public string ID { get => id; }
        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
    }
}