using RedRats.Core;
using Rogium.Options.OptionControllers;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Overseers the Options Menu.
    /// </summary>
    public class OptionsMenuOverseerMono : MonoSingleton<OptionsMenuOverseerMono>
    {
        [SerializeField] private Transform graphicsColumn;
        
        private OptionsGraphicsPropertyBuilder graphicsPropertyBuilder;
        
        private OptionsMenuOverseer editor;
        private GraphicsOptionsController graphics;
        
        protected override void Awake()
        {
            base.Awake();
            editor = OptionsMenuOverseer.Instance;
            graphicsPropertyBuilder = new OptionsGraphicsPropertyBuilder(graphicsColumn);
        }

        private void OnEnable() => editor.OnAssignAsset += PrepareEditor;
        private void OnDisable() => editor.OnAssignAsset -= PrepareEditor;


        /// <summary>
        /// Prepares the Options menu for the user. 
        /// </summary>
        private void PrepareEditor(GameDataAsset asset)
        {
            graphicsPropertyBuilder.Build(asset);
        }
        
    }
}