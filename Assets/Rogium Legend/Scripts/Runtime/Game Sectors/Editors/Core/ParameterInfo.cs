using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Contains all available parameters for <see cref="AssetData"/>.
    /// </summary>
    [System.Serializable]
    public struct ParameterInfo
    {
        public int intValue1;
        public int intValue2;
        public int intValue3;
        public int intValue4;
        public int intValue5;
        public int intValue6;
        [Space]
        public float floatValue1;
        public float floatValue2;
        public float floatValue3;
        public float floatValue4;
        public float floatValue5;
        public float floatValue6;
        [Space]
        public bool boolValue1;
        public bool boolValue2;
        public bool boolValue3;
        public bool boolValue4;
        public bool boolValue5;
        public bool boolValue6;
        [Space]
        public string stringValue1;
        public string stringValue2;
        public string stringValue3;
        public string stringValue4;
        public string stringValue5;
        public string stringValue6;
        
        public ParameterInfo(int intValue1, int intValue2, int intValue3, int intValue4, int intValue5, int intValue6,
                             float floatValue1, float floatValue2, float floatValue3, float floatValue4, float floatValue5,
                             float floatValue6, bool boolValue1, bool boolValue2, bool boolValue3, bool boolValue4, 
                             bool boolValue5, bool boolValue6, string stringValue1, string stringValue2, string stringValue3, 
                             string stringValue4, string stringValue5, string stringValue6)
        {
            this.intValue1 = intValue1;
            this.intValue2 = intValue2;
            this.intValue3 = intValue3;
            this.intValue4 = intValue4;
            this.intValue5 = intValue5;
            this.intValue6 = intValue6;
            this.floatValue1 = floatValue1;
            this.floatValue2 = floatValue2;
            this.floatValue3 = floatValue3;
            this.floatValue4 = floatValue4;
            this.floatValue5 = floatValue5;
            this.floatValue6 = floatValue6;
            this.boolValue1 = boolValue1;
            this.boolValue2 = boolValue2;
            this.boolValue3 = boolValue3;
            this.boolValue4 = boolValue4;
            this.boolValue5 = boolValue5;
            this.boolValue6 = boolValue6;
            this.stringValue1 = stringValue1;
            this.stringValue2 = stringValue2;
            this.stringValue3 = stringValue3;
            this.stringValue4 = stringValue4;
            this.stringValue5 = stringValue5;
            this.stringValue6 = stringValue6;
        }
    }
}