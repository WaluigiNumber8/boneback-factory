using System;

namespace Rogium.Options.Core
{
    [Serializable]
    public struct InputBindingData
    {
        // public string NavigateUp;
        // public string NavigateDown;
        // public string NavigateLeft;
        // public string NavigateRight;
        public string Select;
        public string Cancel;
        public string ContextSelect;
        public string ShowTooltip;
        
        public string MoveUp;
        public string MoveDown;
        public string MoveLeft;
        public string MoveRight;
        public string ButtonMain;
        public string ButtonMainAlt;
        public string ButtonSub;
        public string ButtonSubAlt;
        public string ButtonDash;
        public string ButtonDashAlt;
        
        public string Pause;
    }
}