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
        [SerializeField] private ParameterInfo parameters;

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
        
        public GameObject Prefab { get => prefab; }
        public ParameterInfo Parameters { get => parameters; }
    }
}