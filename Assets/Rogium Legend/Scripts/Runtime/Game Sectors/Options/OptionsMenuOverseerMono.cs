using RedRats.Core;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Overseers the Options Menu.
    /// </summary>
    public class OptionsMenuOverseerMono : MonoSingleton<OptionsMenuOverseerMono>
    {
        [SerializeField] private Transform propertiesColumn;

        private OptionsPropertiesBuilder propertiesBuilder;

        protected override void Awake()
        {
            base.Awake();
            propertiesBuilder = new OptionsPropertiesBuilder(propertiesColumn);
        }
        
        
    }
}