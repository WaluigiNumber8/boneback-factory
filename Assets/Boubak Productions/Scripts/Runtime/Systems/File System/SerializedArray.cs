using System;
using System.Linq;

namespace BoubakProductions.Systems.Serialization
{
    /// <summary>
    /// A serialized array, ready for saving to external storage.
    /// </summary>
    /// <typeparam name="T">Unity readable asset.</typeparam>
    /// <typeparam name="TS">Serialized form of the asset.</typeparam>
    [System.Serializable]
    public class SerializedArray<T, TS>
    {
        private TS[] serializedArray;

        public SerializedArray(T[] objectArray, Func<T,TS> newSerializedObject)
        {
            serializedArray = new TS[objectArray.Length];
            for (int i = 0; i < objectArray.Length; i++)
            {
                TS newObject = newSerializedObject(objectArray[i]);
                serializedArray[i] = newObject;
            }
        }

        /// <summary>
        /// Deserializes the array into a Unity readable format.
        /// </summary>
        /// <param name="newDeserializedObject">The method/constructor that creates a deserialized form of the object.</param>
        /// <returns>An array of deserialized objects.</returns>
        public T[] Deserialize(Func<TS,T> newDeserializedObject)
        {
            return serializedArray.Select(newDeserializedObject).ToArray();
        }
    }
}