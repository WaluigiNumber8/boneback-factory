using BoubakProductions.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.PropertyEditor.Builders
{
    /// <summary>
    /// A base for all property editor builders.
    /// </summary>
    public abstract class PropertyEditorBuilderBase
    {
        protected UIPropertyBuilder builder;
        protected Transform importantContent;
        protected Transform propertyContent;

        public PropertyEditorBuilderBase(Transform importantContent, Transform propertyContent)
        {
            builder = UIPropertyBuilder.GetInstance();
            this.importantContent = importantContent;
            this.propertyContent = propertyContent;
        }

        protected void EmptyEditor()
        {
            importantContent.gameObject.KillChildren();
            propertyContent.gameObject.KillChildren();
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