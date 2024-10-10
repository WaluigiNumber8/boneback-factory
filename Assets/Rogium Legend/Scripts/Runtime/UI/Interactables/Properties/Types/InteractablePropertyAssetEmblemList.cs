using System.Collections.Generic;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyAssetEmblemList : InteractablePropertyBase<IList<Sprite>>
    {
        [SerializeField] private AssetEmblemList emblemList;
        
        public void Construct(string titleText, IList<Sprite> value)
        {
            ConstructTitle(titleText);
            emblemList.Construct(value);
        }
        
        public override void SetDisabled(bool isDisabled)
        {
        }

        public override IList<Sprite> PropertyValue { get; }
    }
}