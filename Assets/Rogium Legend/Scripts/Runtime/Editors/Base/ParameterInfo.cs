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
        public int intValue7;
        public int intValue8;
        [Space]
        public float floatValue1;
        public float floatValue2;
        public float floatValue3;
        public float floatValue4;
        public float floatValue5;
        public float floatValue6;
        public float floatValue7;
        public float floatValue8;
        [Space]
        public bool boolValue1;
        public bool boolValue2;
        public bool boolValue3;
        public bool boolValue4;
        public bool boolValue5;
        public bool boolValue6;
        public bool boolValue7;
        public bool boolValue8;

        public ParameterInfo(int intValue1, int intValue2, int intValue3, int intValue4, int intValue5, int intValue6, int intValue7, int intValue8, float floatValue1, float floatValue2, float floatValue3, float floatValue4, float floatValue5, float floatValue6, float floatValue7, float floatValue8, bool boolValue1, bool boolValue2, bool boolValue3, bool boolValue4, bool boolValue5, bool boolValue6, bool boolValue7, bool boolValue8)
        {
            this.intValue1 = intValue1;
            this.intValue2 = intValue2;
            this.intValue3 = intValue3;
            this.intValue4 = intValue4;
            this.intValue5 = intValue5;
            this.intValue6 = intValue6;
            this.intValue7 = intValue7;
            this.intValue8 = intValue8;
            this.floatValue1 = floatValue1;
            this.floatValue2 = floatValue2;
            this.floatValue3 = floatValue3;
            this.floatValue4 = floatValue4;
            this.floatValue5 = floatValue5;
            this.floatValue6 = floatValue6;
            this.floatValue7 = floatValue7;
            this.floatValue8 = floatValue8;
            this.boolValue1 = boolValue1;
            this.boolValue2 = boolValue2;
            this.boolValue3 = boolValue3;
            this.boolValue4 = boolValue4;
            this.boolValue5 = boolValue5;
            this.boolValue6 = boolValue6;
            this.boolValue7 = boolValue7;
            this.boolValue8 = boolValue8;
        }
    }
}