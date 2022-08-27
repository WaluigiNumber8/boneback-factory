using UnityEngine;

namespace RedRats.Systems.FileSystem.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="Vector3"/> struct.
    /// </summary>
    [System.Serializable]
    public class SerializedVector3 : ISerializedObject<Vector3>
    {
        private float x, y, z;

        public SerializedVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public SerializedVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        /// <summary>
        /// Returns the Vector in a Unity acceptable format.
        /// </summary>
        /// <returns>The Vector3.</returns>
        public Vector3 Deserialize()
        {
            return new Vector3(x, y, z);
        }
    }
}