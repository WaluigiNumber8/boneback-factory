namespace BoubakProductions.Core
{
    /// <summary>
    /// An interface for all scripts working with enabling/disabling object visibility.
    /// </summary>
    public interface IObjectVisibilityController
    {
        /// <summary>
        /// Changes the visibility state of the object.
        /// <param name="isVisible">Is the object active?</param>
        /// </summary>
        public void SwitchVisibilityStatus(bool isVisible);
        
        /// <summary>
        /// Changes the visibility state of the object.
        /// </summary>
        public void SwitchVisibilityStatus();
    }
}