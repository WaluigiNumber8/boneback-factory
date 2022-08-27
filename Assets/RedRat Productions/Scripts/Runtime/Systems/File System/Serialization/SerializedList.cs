using System;
using System.Collections.Generic;
using System.Linq;

namespace RedRats.Systems.FileSystem.Serialization
{
    /// <summary>
    /// A Serialized List, ready for saving on external storage.
    /// </summary>
    /// <typeparam name="T">Unity readable Asset.</typeparam>
    /// <typeparam name="TS">Serialized Form of <see cref="T"/>Asset.</typeparam>
    [Serializable]
    public class SerializedList<T,TS> : ISerializedObject<IList<T>>
    {
        private IList<TS> serializedList = new List<TS>();
        private readonly Func<TS, T> newDeserializedObject;

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="objectList">The List we want to serialize.</param>
        /// <param name="newSerializedObject">A method creating the Serialized Data Object this list is going to contain.</param>
        /// <param name="newDeserializedObject">A method that creates a deserialized form of the object.</param>
        public SerializedList(IList<T> objectList, Func<T,TS> newSerializedObject, Func<TS,T> newDeserializedObject)
        {
            this.newDeserializedObject = newDeserializedObject;
            foreach (T listObject in objectList)
            {
                TS newObject = newSerializedObject(listObject);
                serializedList.Add(newObject);
            }
        }

        /// <summary>
        /// Deserializes the list into a Unity readable format.
        /// </summary>
        /// <returns>A list of deserialized objects.</returns>
        public IList<T> Deserialize()
        {
            return serializedList.Select(newDeserializedObject).ToList();
        }

    }
}