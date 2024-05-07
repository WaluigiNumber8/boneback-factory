namespace Rogium.Editors.Core
{
    /// <summary>
    /// Represents all assets containing unique parameters with individual update methods.
    /// </summary>
    public interface IParameterAsset : IIDHolder
    {
        public void UpdateIntValue1(int newValue);
        public void UpdateIntValue2(int newValue);
        public void UpdateIntValue3(int newValue);
        public void UpdateIntValue4(int newValue);
        public void UpdateIntValue5(int newValue);
        public void UpdateIntValue6(int newValue);
        
        public void UpdateFloatValue1(float newValue);
        public void UpdateFloatValue2(float newValue);
        public void UpdateFloatValue3(float newValue);
        public void UpdateFloatValue4(float newValue);
        public void UpdateFloatValue5(float newValue);
        public void UpdateFloatValue6(float newValue);
        
        public void UpdateBoolValue1(bool newValue);
        public void UpdateBoolValue2(bool newValue);
        public void UpdateBoolValue3(bool newValue);
        public void UpdateBoolValue4(bool newValue);
        public void UpdateBoolValue5(bool newValue);
        public void UpdateBoolValue6(bool newValue);
        
        public void UpdateStringValue1(string newValue);
        public void UpdateStringValue2(string newValue);
        public void UpdateStringValue3(string newValue);
        public void UpdateStringValue4(string newValue);
        public void UpdateStringValue5(string newValue);
        public void UpdateStringValue6(string newValue);
        
        public ParameterInfo Parameters { get; }
    }
}