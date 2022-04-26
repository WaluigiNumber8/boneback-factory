using System;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.PropertyEditor.Builders
{
    public abstract class PropertyEditorBuilderAnimationBase : PropertyEditorBuilderBase
    {
        protected InteractablePropertyContentBlock animationBlock1Slot;
        protected InteractablePropertyContentBlock animationBlock2Slot;
        
        public PropertyEditorBuilderAnimationBase(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond) { }

        protected abstract override void BuildImportant(Transform content);

        protected abstract override void BuildProperty(Transform content);

        protected void SwitchAnimationSlots(AnimationType type)
        {
            switch (type)
            {
                case AnimationType.NoAnimation:
                case AnimationType.HorizontalFlip:
                case AnimationType.VerticalFlip:
                    animationBlock1Slot.SetDisabled(false);
                    animationBlock2Slot.SetDisabled(true);
                    break;
                case AnimationType.SpriteSwap:
                    animationBlock1Slot.SetDisabled(true);
                    animationBlock2Slot.SetDisabled(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}