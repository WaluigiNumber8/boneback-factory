using RedRats.Core;
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

        public override bool Equals(object obj)
        {
            if (obj is ParameterInfo other)
            {
                return intValue1 == other.intValue1 && intValue2 == other.intValue2 && intValue3 == other.intValue3 &&
                       intValue4 == other.intValue4 && intValue5 == other.intValue5 && intValue6 == other.intValue6 &&
                       floatValue1.IsSameAs(other.floatValue1) && floatValue2.IsSameAs(other.floatValue2) && floatValue3.IsSameAs(other.floatValue3) &&
                       floatValue4.IsSameAs(other.floatValue4) && floatValue5.IsSameAs(other.floatValue5) && floatValue6.IsSameAs(other.floatValue6) &&
                       boolValue1 == other.boolValue1 && boolValue2 == other.boolValue2 && boolValue3 == other.boolValue3 &&
                       boolValue4 == other.boolValue4 && boolValue5 == other.boolValue5 && boolValue6 == other.boolValue6 &&
                       stringValue1 == other.stringValue1 && stringValue2 == other.stringValue2 && stringValue3 == other.stringValue3 &&
                       stringValue4 == other.stringValue4 && stringValue5 == other.stringValue5 && stringValue6 == other.stringValue6;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return intValue1.GetHashCode() ^ intValue2.GetHashCode() ^ intValue3.GetHashCode() ^ intValue4.GetHashCode() ^ intValue5.GetHashCode() ^ intValue6.GetHashCode() ^
                   floatValue1.GetHashCode() ^ floatValue2.GetHashCode() ^ floatValue3.GetHashCode() ^ floatValue4.GetHashCode() ^ floatValue5.GetHashCode() ^ floatValue6.GetHashCode() ^
                   boolValue1.GetHashCode() ^ boolValue2.GetHashCode() ^ boolValue3.GetHashCode() ^ boolValue4.GetHashCode() ^ boolValue5.GetHashCode() ^ boolValue6.GetHashCode() ^
                   stringValue1.GetHashCode() ^ stringValue2.GetHashCode() ^ stringValue3.GetHashCode() ^ stringValue4.GetHashCode() ^ stringValue5.GetHashCode() ^ stringValue6.GetHashCode();
        }
    }
}