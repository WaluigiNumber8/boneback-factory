using System;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A data object holding a reference to an asset as well as it's unique data.
    /// </summary>
    [Serializable]
    public class AssetData : IParameterAsset, IComparable
    {
        private readonly string id;
        private ParameterInfo parameters;

        #region Constructors
        public AssetData(ParameterInfo parameters)
        {
            id = EditorDefaults.EmptyAssetID;
            this.parameters = parameters;
        }
        
        public AssetData(string id, ParameterInfo parameters)
        {
            this.id = id;
            this.parameters = parameters;
        }
        #endregion

        #region Update Values
        public void UpdateParameters(ParameterInfo newParams) => parameters = newParams;
        
        public void UpdateIntValue1(int newValue) => parameters.intValue1 = newValue;
        public void UpdateIntValue2(int newValue) => parameters.intValue2 = newValue;
        public void UpdateIntValue3(int newValue) => parameters.intValue3 = newValue;
        public void UpdateIntValue4(int newValue) => parameters.intValue4 = newValue;
        public void UpdateIntValue5(int newValue) => parameters.intValue5 = newValue;
        public void UpdateIntValue6(int newValue) => parameters.intValue6 = newValue;
        public void UpdateIntValue7(int newValue) => parameters.intValue7 = newValue;
        public void UpdateIntValue8(int newValue) => parameters.intValue8 = newValue;
        
        public void UpdateFloatValue1(float newValue) => parameters.floatValue1 = newValue;
        public void UpdateFloatValue2(float newValue) => parameters.floatValue2 = newValue;
        public void UpdateFloatValue3(float newValue) => parameters.floatValue3 = newValue;
        public void UpdateFloatValue4(float newValue) => parameters.floatValue4 = newValue;
        public void UpdateFloatValue5(float newValue) => parameters.floatValue5 = newValue;
        public void UpdateFloatValue6(float newValue) => parameters.floatValue6 = newValue;
        public void UpdateFloatValue7(float newValue) => parameters.floatValue7 = newValue;
        public void UpdateFloatValue8(float newValue) => parameters.floatValue8 = newValue;
        
        public void UpdateBoolValue1(bool newValue) => parameters.boolValue1 = newValue;
        public void UpdateBoolValue2(bool newValue) => parameters.boolValue2 = newValue;
        public void UpdateBoolValue3(bool newValue) => parameters.boolValue3 = newValue;
        public void UpdateBoolValue4(bool newValue) => parameters.boolValue4 = newValue;
        public void UpdateBoolValue5(bool newValue) => parameters.boolValue5 = newValue;
        public void UpdateBoolValue6(bool newValue) => parameters.boolValue6 = newValue;
        public void UpdateBoolValue7(bool newValue) => parameters.boolValue7 = newValue;
        public void UpdateBoolValue8(bool newValue) => parameters.boolValue8 = newValue;
        #endregion
        
        public override int GetHashCode()
        {
            return (string.IsNullOrEmpty(id)) ? EditorDefaults.EmptyAssetID.GetHashCode() : id.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            int hashMe = GetHashCode();
            int other = obj.GetHashCode();
            return hashMe.CompareTo(other);
        }

        public string ID { get => id; }
        public ParameterInfo Parameters { get => parameters; }
    }
}