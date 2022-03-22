using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all Property Builders.
    /// </summary>
    public abstract class UIPropertyContentBuilderBase : IUIPropertyContentBuilder
    {
        protected readonly UIPropertyBuilder b;
        protected readonly Transform contentMain;

        protected UIPropertyContentBuilderBase(Transform contentMain)
        {
            b = UIPropertyBuilder.GetInstance();
            this.contentMain = contentMain;
        }

        /// <summary>
        /// Empty contents.
        /// </summary>
        public abstract void EmptyContent();
    }
}