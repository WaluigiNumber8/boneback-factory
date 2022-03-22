using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all Property Builders working with a 2-column setup.
    /// </summary>
    public abstract class UIPropertyContentBuilderBaseColumn2 : UIPropertyContentBuilderBase
    {
        protected readonly Transform contentSecond;
        
        protected UIPropertyContentBuilderBaseColumn2(Transform contentMain, Transform contentSecond) : base(contentMain)
        {
            this.contentSecond = contentSecond;
        }

        public override void EmptyContent()
        {
            contentMain.gameObject.KillChildren();
            contentSecond.gameObject.KillChildren();
        }
    }
}