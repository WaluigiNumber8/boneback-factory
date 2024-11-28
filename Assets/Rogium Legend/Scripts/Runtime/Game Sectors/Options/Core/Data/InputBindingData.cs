using System;

namespace Rogium.Options.Core
{
    [Serializable]
    public struct InputBindingData
    {
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
        public string Select;
        public string Cancel;
        public string ContextSelect;
        public string ShowTooltip;
    }
}