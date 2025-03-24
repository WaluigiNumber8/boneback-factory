using System;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.PropertyEditor.Builders
{
    public abstract class PropertyEditorBuilderAnimationBase<T> : PropertyEditorBuilderBase<T> where T : IAsset
    {
        protected IPContentBlock animationBlock1Slot;
        protected IPContentBlock animationBlock2Slot;

        protected readonly string[] animationOptions;

        public PropertyEditorBuilderAnimationBase(Transform contentMain, Transform contentSecond) : base(contentMain, contentSecond)
        {
            animationOptions = new[] { "No Animation", "Horizontal Flip", "Vertical Flip" };
        }

        protected abstract override void BuildColumnImportant(Transform content);

        protected abstract override void BuildColumnProperty(Transform content);

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