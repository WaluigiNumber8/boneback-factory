namespace Rogium.Systems.Input
{
    /// <summary>
    /// A base for all input profiles.
    /// </summary>
    public abstract class InputProfileBase
    {
        protected RogiumInputActions input;

        protected InputProfileBase()
        {
            input = new RogiumInputActions();
        }

        /// <summary>
        /// Enables the profile.
        /// </summary>
        public abstract void Enable();

        /// <summary>
        /// Disables the profile.
        /// </summary>
        public abstract void Disable();

    }
}