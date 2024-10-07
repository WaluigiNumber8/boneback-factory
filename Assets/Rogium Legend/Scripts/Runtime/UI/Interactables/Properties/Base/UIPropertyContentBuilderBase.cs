using RedRats.Safety;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all Property Builders.
    /// </summary>
    public abstract class UIPropertyContentBuilderBase<T> : IUIPropertyContentBuilder where T : IIDHolder
    {
        protected readonly UIPropertyBuilder b;
        protected readonly Transform contentMain;

        protected UIPropertyContentBuilderBase(Transform contentMain)
        {
            SafetyNet.EnsureIsNotNull(contentMain, "Property Content Transform");
            
            b = UIPropertyBuilder.GetInstance();
            this.contentMain = contentMain;
        }

        /// <summary>
        /// Build properties for the asset.
        /// </summary>
        public abstract void Build(T asset);
        
        /// <summary>
        /// Empty contents.
        /// </summary>
        public abstract void Clear();
    }
}