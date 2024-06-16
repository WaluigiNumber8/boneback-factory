using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Editors.Core.Defaults
{
    /// <summary>
    /// Stores default sprites for the editor.
    /// </summary>
    [CreateAssetMenu(fileName = "asset_DefaultSprites", menuName = EditorConstants.AssetMenuEditor + "Default Sprite Constants", order = 500)]
    public class EditorSpriteConstants : ScriptableObject
    {
        [Title("General")]
        [SerializeField, Required, PreviewField] private Sprite emptySprite;
        [SerializeField, Required, PreviewField] private Sprite missingSprite;
        [Title("Assets")]
        [SerializeField, Required, PreviewField] private Sprite packIcon;
        [SerializeField, Required, PreviewField] private Sprite weaponIcon;
        [SerializeField, Required, PreviewField] private Sprite projectileIcon;
        [SerializeField, Required, PreviewField] private Sprite enemyIcon;
        [SerializeField, Required, PreviewField] private Sprite tileIcon;
        
        private static EditorSpriteConstants instance;
        public static EditorSpriteConstants Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<EditorSpriteConstants>("Defaults/asset_DefaultSprites");
                }
                return instance;
            }
        }
        
        public Sprite EmptySprite { get => emptySprite; }
        public Sprite MissingSprite { get => missingSprite; }
        public Sprite PackIcon { get => packIcon; }
        public Sprite WeaponIcon { get => weaponIcon; }
        public Sprite ProjectileIcon { get => projectileIcon; }
        public Sprite EnemyIcon { get => enemyIcon; }
        public Sprite TileIcon { get => tileIcon; }
    }
}