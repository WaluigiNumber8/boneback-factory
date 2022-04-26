using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Objects
{
    /// <summary>
    /// Contains all data needed for an interactable object.
    /// </summary>
    [CreateAssetMenu(fileName = "New Interactable Object", menuName = EditorDefaults.AssetMenuAssets + "Interactable Object", order = 1)]
    public class ObjectAsset : AssetBaseSO, IParameterAsset
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ParameterInfo defaultParameters;

        #region Update Values
        public void UpdateParameters(ParameterInfo newParams) => defaultParameters = newParams;
        
        public void UpdateIntValue1(int newValue) => defaultParameters.intValue1 = newValue;
        public void UpdateIntValue2(int newValue) => defaultParameters.intValue2 = newValue;
        public void UpdateIntValue3(int newValue) => defaultParameters.intValue3 = newValue;
        public void UpdateIntValue4(int newValue) => defaultParameters.intValue4 = newValue;
        public void UpdateIntValue5(int newValue) => defaultParameters.intValue5 = newValue;
        public void UpdateIntValue6(int newValue) => defaultParameters.intValue6 = newValue;
        
        public void UpdateFloatValue1(float newValue) => defaultParameters.floatValue1 = newValue;
        public void UpdateFloatValue2(float newValue) => defaultParameters.floatValue2 = newValue;
        public void UpdateFloatValue3(float newValue) => defaultParameters.floatValue3 = newValue;
        public void UpdateFloatValue4(float newValue) => defaultParameters.floatValue4 = newValue;
        public void UpdateFloatValue5(float newValue) => defaultParameters.floatValue5 = newValue;
        public void UpdateFloatValue6(float newValue) => defaultParameters.floatValue6 = newValue;
        
        public void UpdateBoolValue1(bool newValue) => defaultParameters.boolValue1 = newValue;
        public void UpdateBoolValue2(bool newValue) => defaultParameters.boolValue2 = newValue;
        public void UpdateBoolValue3(bool newValue) => defaultParameters.boolValue3 = newValue;
        public void UpdateBoolValue4(bool newValue) => defaultParameters.boolValue4 = newValue;
        public void UpdateBoolValue5(bool newValue) => defaultParameters.boolValue5 = newValue;
        public void UpdateBoolValue6(bool newValue) => defaultParameters.boolValue6 = newValue;
        
        public void UpdateStringValue1(string newValue) => defaultParameters.stringValue1 = newValue;
        public void UpdateStringValue2(string newValue) => defaultParameters.stringValue2 = newValue;
        public void UpdateStringValue3(string newValue) => defaultParameters.stringValue3 = newValue;
        public void UpdateStringValue4(string newValue) => defaultParameters.stringValue4 = newValue;
        public void UpdateStringValue5(string newValue) => defaultParameters.stringValue5 = newValue;
        public void UpdateStringValue6(string newValue) => defaultParameters.stringValue6 = newValue;
        #endregion
        
        public GameObject Prefab { get => prefab; }
        public ParameterInfo Parameters { get => defaultParameters; }
    }
}