namespace Rogium.Systems.Input
{
    public interface IInputType
    {
        /// <summary>
        /// Enable input reading.
        /// </summary>
        public void Enable();
        
        /// <summary>
        /// Disable input reading.
        /// </summary>
        public void Disable();
        
    }
}