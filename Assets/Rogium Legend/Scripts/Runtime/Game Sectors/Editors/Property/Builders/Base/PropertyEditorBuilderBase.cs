using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// A base for all property editor builders.
    /// </summary>
    public abstract class PropertyEditorBuilderBase : UIPropertyContentBuilderBaseColumn2
    {
        protected PropertyEditorBuilderBase(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond)
        {
        }
        
        /// <summary>
        /// Builds the Important column with properties.
        /// </summary>
        protected abstract void BuildImportant(Transform content);
        
        /// <summary>
        /// Builds the Property column with properties.
        /// </summary>
        protected abstract void BuildProperty(Transform content);

    }
}