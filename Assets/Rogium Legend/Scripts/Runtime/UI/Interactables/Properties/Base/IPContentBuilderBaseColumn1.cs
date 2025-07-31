using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all Property Builders working with a single column.
    /// </summary>
    public abstract class IPContentBuilderBaseColumn1<T> : IPContentBuilderBase<T> where T : IIDHolder
    {
        protected IPContentBuilderBaseColumn1(Transform contentMain) : base(contentMain) { }

        public override void Clear()
        {
            contentMain.ReleaseAllProperties();
        }
    }
}