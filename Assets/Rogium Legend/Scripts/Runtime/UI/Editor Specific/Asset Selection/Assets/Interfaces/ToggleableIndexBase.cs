namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// A base for all classes working with an internal toggle and index storing.
    /// </summary>
    public abstract class ToggleableIndexBase : ToggleableBase
    {
        protected int index;
        
        public int Index {get => index;}
    }
}