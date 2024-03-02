using System;
using Rogium.Editors.Core;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a sound picker property.
    /// </summary>
    public class InteractablePropertySoundPicker : InteractablePropertyBase
    {
        // [SerializeField] private AssetField type;
        
        public void Construct(string titleText, AssetData value, Action<AssetData> whenSoundEdited)
        {
            ConstructTitle(titleText);
            
            //Assign "Open Sound Picker window" action into the button.
            //and plug values change to whenSoundEdited.
            
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            
        }

        
    }
}