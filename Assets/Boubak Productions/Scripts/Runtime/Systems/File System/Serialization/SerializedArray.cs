using System;
using System.Linq;

namespace BoubakProductions.Systems.FileSystem.Serialization
{
    /// <summary>
    /// A serialized array, ready for saving to external storage.
    /// </summary>
    /// <typeparam name="T">Unity readable asset.</typeparam>
    /// <typeparam name="TS">Serialized form of the asset.</typeparam>
    [Serializable]
    public class SerializedArray<T, TS> : ISerializedObject<T[]>
    {
        private TS[] serializedArray;
        private readonly Func<TS, T> newDeserializedObject;

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="objectArray">The array we want to serialize.</param>
        /// <param name="newSerializedObject">A method creating the Serialized Data Object this list is going to contain.</param>
        /// <param name="newDeserializedObject">A method that creates a deserialized form of the object.</param>
        public SerializedArray(T[] objectArray, Func<T,TS> newSerializedObject, Func<TS,T> newDeserializedObject)
        {
            this.newDeserializedObject = newDeserializedObject;
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
        /// <returns>An array of deserialized objects.</returns>
        public T[] Deserialize()
        {
            return serializedArray.Select(newDeserializedObject).ToArray();
        }
        
    }
}