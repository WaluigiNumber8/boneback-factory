using System;
using System.Linq;
using RedRats.Safety;

namespace RedRats.Systems.FileSystem.JSON.Serialization
{
    /// <summary>
    /// The <see cref="Array"/> object converted to JSON-understandable format.
    /// </summary>
    /// <typeparam name="T">Unity readable asset.</typeparam>
    /// <typeparam name="TS">Serialized form of the asset.</typeparam>
    [Serializable]
    public class JSONArray<T, TS> : IEncodedObject<T[]>
    {
        public TS[] serializedArray;
        public Func<TS, T> newDeserializedObject;

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="objectArray">The array we want to serialize.</param>
        /// <param name="newSerializedObject">A method creating the Serialized Data Object this list is going to contain.</param>
        public JSONArray(T[] objectArray, Func<T,TS> newSerializedObject)
        {
            serializedArray = new TS[objectArray.Length];
            for (int i = 0; i < objectArray.Length; i++)
            {
                TS newObject = newSerializedObject(objectArray[i]);
                serializedArray[i] = newObject;
            }
        }

        /// <summary>
        /// Sets the method for decoding the object.
        /// </summary>
        /// <param name="newDeserializedObject">The method that creates a decoded object from the encoded one.</param>
        public void SetDecodingMethod(Func<TS, T> newDeserializedObject)
        {
            this.newDeserializedObject = newDeserializedObject;
        }

        /// <summary>
        /// Deserializes the array into a Unity readable format.
        /// </summary>
        /// <returns>An array of deserialized objects.</returns>
        public T[] Decode()
        {
            SafetyNet.EnsureIsNotNull(newDeserializedObject, nameof(newDeserializedObject), "You need to set the decoder method before decoding.");
            return serializedArray.Select(newDeserializedObject).ToArray();
        }
        
    }
}