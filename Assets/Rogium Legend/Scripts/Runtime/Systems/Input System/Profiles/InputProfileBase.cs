namespace Rogium.Systems.Input
{
    /// <summary>
    /// A base for all input profiles.
    /// </summary>
    public abstract class InputProfileBase
    {
        protected readonly RogiumInputActions input;

        protected InputProfileBase(RogiumInputActions input) => this.input = input;

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