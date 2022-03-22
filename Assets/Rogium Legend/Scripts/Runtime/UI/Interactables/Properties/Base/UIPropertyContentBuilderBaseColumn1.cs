using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all Property Builders working with a single column.
    /// </summary>
    public abstract class UIPropertyContentBuilderBaseColumn1 : UIPropertyContentBuilderBase
    {
        protected UIPropertyContentBuilderBaseColumn1(Transform contentMain) : base(contentMain) { }

        public override void EmptyContent() => contentMain.gameObject.KillChildren();
    }
}