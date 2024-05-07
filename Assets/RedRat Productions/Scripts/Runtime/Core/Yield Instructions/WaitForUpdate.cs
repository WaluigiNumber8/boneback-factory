using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Waits for the next Update.
    /// </summary>
    public class WaitForUpdate : CustomYieldInstruction
    {
        public override bool keepWaiting { get => false; }
    }
}