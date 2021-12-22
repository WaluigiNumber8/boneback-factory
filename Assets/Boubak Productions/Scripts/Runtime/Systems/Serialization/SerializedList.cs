using System;
using System.Collections.Generic;
using System.Linq;

namespace BoubakProductions.Systems.Serialization
{
    /// <summary>
    /// A Serialized List, ready for saving on external storage.
    /// </summary>
    /// <typeparam name="T">Unity readable Asset.</typeparam>
    /// <typeparam name="TS">Serialized Form of <see cref="T"/>Asset.</typeparam>
    [System.Serializable]
    public class SerializedList<T,TS>
    {
        private IList<TS> serializedList = new List<TS>();

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="objectList">The List we want to serialize.</param>
        /// <param name="newSerializedObject">Constructor for the Serialized Asset this list is going to contain.</param>
        public SerializedList(IList<T> objectList, Func<T,TS> newSerializedObject)
        {
            foreach (T listObject in objectList)
            {
                TS newObject = newSerializedObject(listObject);
                serializedList.Add(newObject);
            }
        }

        public IList<T> Deserialize(Func<TS,T> newDeserializedObject)
        {
            return serializedList.Select(newDeserializedObject).ToList();
        }
    }
}