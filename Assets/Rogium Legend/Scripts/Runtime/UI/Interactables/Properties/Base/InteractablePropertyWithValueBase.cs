namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A base for all interactable properties.
    /// </summary>
    public abstract class InteractablePropertyWithValueBase<T> : InteractablePropertyBase
    {
        public abstract T PropertyValue { get; }
    }
}