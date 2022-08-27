using UnityEngine;

namespace RedRats.Systems.FileSystem.Serialization
{
    [System.Serializable]
    public class SerializedQuaternion : ISerializedObject<Quaternion>
    {
        private float x, y, z, w;

        public SerializedQuaternion(Quaternion quaternion) : this(quaternion.x, quaternion.y, quaternion.z, quaternion.w) {}
        public SerializedQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Returns the quaternion in a Unity readable format.
        /// </summary>
        /// <returns></returns>
        public Quaternion Deserialize()
        {
            return new Quaternion(x, y, z, w);
        }
        
    }
}