using System.Collections.Generic;
using System.Collections.ObjectModel;
using RedRats.UI.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyAssetEmblemList : InteractablePropertyBase<ReadOnlyCollection<Sprite>>
    {
        [SerializeField] private AssetEmblemList emblemList;
        
        public void Construct(string titleText, IList<Sprite> value)
        {
            ConstructTitle(titleText);
            emblemList.Construct(value);
        }
        
        public void UpdateTheme(FontInfo titleFont)
        {
            if (title != null) UIExtensions.ChangeFont(title, titleFont);
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            // Do nothing
        }

        public override ReadOnlyCollection<Sprite> PropertyValue { get => emblemList.Emblems; }
    }
}