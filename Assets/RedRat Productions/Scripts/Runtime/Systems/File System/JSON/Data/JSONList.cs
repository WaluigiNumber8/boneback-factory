using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;

namespace RedRats.Systems.FileSystem.JSON.Serialization
{
    /// <summary>
    /// A serialized List, ready for saving on external storage.
    /// </summary>
    /// <typeparam name="T">Unity readable asset.</typeparam>
    /// <typeparam name="TS">Serialized form of <see cref="T"/>asset.</typeparam>
    [Serializable]
    public class JSONList<T,TS> : IEncodedObject<IList<T>>
    {
        public TS[] serializedList;
        public Func<TS, T> newDeserializedObject;

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="objectList">The List we want to serialize.</param>
        /// <param name="newSerializedObject">A method creating the Serialized Data Object this list is going to contain.</param>
        public JSONList(IList<T> objectList, Func<T,TS> newSerializedObject)
        {
            serializedList = new TS[objectList.Count];
            for (int i = 0; i < objectList.Count; i++)
            {
                serializedList[i] = newSerializedObject(objectList[i]);
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
        /// Deserializes the list into a Unity readable format.
        /// </summary>
        /// <returns>A list of deserialized objects.</returns>
        public IList<T> Decode()
        {
            Preconditions.IsNotNull(newDeserializedObject, nameof(newDeserializedObject), "You need to set the decoder method before decoding.");
            return serializedList.Select(newDeserializedObject).ToList();
        }

    }
}