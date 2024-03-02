using System.Collections.Generic;
using RedRats.Core;
using Rogium.Editors.Objects;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the in-game prebuild assets, that can only be created in the editor.
    /// </summary>
    public class InternalLibraryOverseer : PersistentMonoSingleton<InternalLibraryOverseer>
    {
        [SerializeField] private ObjectLibraryAsset objects;
        
        /// <summary>
        /// Returns an object with a specific ID.
        /// </summary>
        /// <param name="id">The ID of the object to return.</param>
        /// <returns>The object with the same id.</returns>
        public ObjectAsset GetObjectByID(string id) => objects.GetObjectByID(id);

        /// <summary>
        /// Returns a copy of the array of interactable objects stored here.
        /// </summary>
        /// <returns>A copy of Pack Library.</returns>
        public IList<ObjectAsset> GetObjectsCopy() => objects.GetObjectsCopy();
    }
}