using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;

namespace RedRats.Systems.FileSystem.JSON.Serialization
{
    /// <summary>
    /// A serialized Set, ready for saving to external storage.
    /// </summary>
    /// <typeparam name="T">Unity readable asset.</typeparam>
    /// <typeparam name="TS">Serialized form of the <see cref="T"/> asset.</typeparam>
    [Serializable]
    public class JSONSet<T, TS> : IEncodedObject<ISet<T>>
    {
        public TS[] serializedSet;
        public Func<TS, T> newDeserializedObject;

        public JSONSet(ISet<T> objectSet, Func<T, TS> newSerializedObject)
        {
            serializedSet = new TS[objectSet.Count];
            T[] objectArray = objectSet.ToArray();
            for (int i = 0; i < objectSet.Count; i++)
            {
                serializedSet[i] = newSerializedObject(objectArray[i]);
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
        
        public ISet<T> Decode()
        {
            SafetyNet.EnsureIsNotNull(newDeserializedObject, nameof(newDeserializedObject), "You need to set the decoder method before decoding.");
            return serializedSet.Select(newDeserializedObject).ToHashSet();
        }
    }
}