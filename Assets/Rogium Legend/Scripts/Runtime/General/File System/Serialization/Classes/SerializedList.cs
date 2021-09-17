using System;
using System.Collections.Generic;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// A Serialized List, ready for saving on external storage.
    /// </summary>
    /// <typeparam name="T">Unity readable Asset.</typeparam>
    /// <typeparam name="S">Serialized Form of <see cref="T"/>Asset.</typeparam>
    [System.Serializable]
    public class SerializedList<T,S>
    {
        private IList<S> serializedList = new List<S>();

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="objectList">The List we want to serialize.</param>
        /// <param name="NewSerializedObject">Constructor for the Serialized Asset this list is going to contain.</param>
        public SerializedList(IList<T> objectList, Func<T,S> NewSerializedObject)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                S newObject = NewSerializedObject(objectList[i]);
                serializedList.Add(newObject);
            }
        }

        public IList<T> Deserialize(Func<S,T> NewDeserializedObject)
        {
            IList<T> deserializedList = new List<T>();

            foreach (S serializedObject in serializedList)
            {
                T newObject = NewDeserializedObject(serializedObject);
                deserializedList.Add(newObject);
            }

            return deserializedList;
        }
    }
}