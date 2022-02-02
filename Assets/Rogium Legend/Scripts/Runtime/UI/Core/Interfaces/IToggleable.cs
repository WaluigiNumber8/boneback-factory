namespace Rogium.UserInterface.Core
{
    /// <summary>
    /// A Base for all classes allowing the control of Toggle Components via code.
    /// </summary>
    public interface IToggleable
    {
        /// <summary>
        /// Controls the toggle value.
        /// </summary>
        /// <param name="value">Value of the toggle.</param>
        void SetToggle(bool value);
    }
}