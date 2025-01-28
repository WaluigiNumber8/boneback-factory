using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all Property Builders working with a single column.
    /// </summary>
    public abstract class UIPropertyContentBuilderBaseColumn1<T> : UIPropertyContentBuilderBase<T> where T : IIDHolder
    {
        protected UIPropertyContentBuilderBaseColumn1(Transform contentMain) : base(contentMain) { }

        public override void Clear()
        {
            contentMain.ReleaseAllProperties();
        }
    }
}