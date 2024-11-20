using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all Property Builders working with a 2-column setup.
    /// </summary>
    public abstract class UIPropertyContentBuilderBaseColumn2<T> : UIPropertyContentBuilderBase<T> where T : IIDHolder
    {
        protected readonly Transform contentSecond;
        
        protected UIPropertyContentBuilderBaseColumn2(Transform contentMain, Transform contentSecond) : base(contentMain)
        {
            SafetyNet.EnsureIsNotNull(contentSecond, "Secondary Content Property Transform");
            
            this.contentSecond = contentSecond;
        }

        public override void Clear()
        {
            contentMain.gameObject.KillChildren();
            contentSecond.gameObject.KillChildren();
        }
    }
}