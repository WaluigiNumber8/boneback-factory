using Rogium.Editors.Objects;

namespace Rogium.Gameplay.InteractableObjects
{
    /// <summary>
    /// A base for all main interactable object scripts.
    /// </summary>
    public interface IInteractObject
    {
        /// <summary>
        /// Constructs an interactable object.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public void Construct(ObjectAsset data);
    }
}