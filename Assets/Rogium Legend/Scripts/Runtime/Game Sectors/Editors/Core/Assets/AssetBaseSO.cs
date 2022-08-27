using Rogium.Editors.Core.Defaults;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all internal assets.
    /// </summary>
    public abstract class AssetBaseSO : ScriptableObject, IAsset
    {
        [HorizontalGroup("Base", 60, LabelWidth = 64, MarginRight = 32), HideLabel, PreviewField(60)] 
        [SerializeField] private Sprite icon;
        [VerticalGroup("Base/Right")]
        [SerializeField]  private string id;
        [VerticalGroup("Base/Right")]
        [SerializeField]  private string title;
        
        private string author;

        private void Awake() => author = EditorDefaults.AuthorGame;

        public string ID { get => id; }
        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
    }
}