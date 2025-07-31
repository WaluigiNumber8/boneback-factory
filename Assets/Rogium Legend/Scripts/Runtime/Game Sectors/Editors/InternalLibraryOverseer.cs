using System.Collections.Generic;
using System.Collections.ObjectModel;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Objects;
using Rogium.Editors.Sounds;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the in-game prebuild assets, that can only be created in the editor.
    /// </summary>
    public class InternalLibraryOverseer : PersistentMonoSingleton<InternalLibraryOverseer>
    {
        [SerializeField] private ObjectCollectionAsset objects;
        [SerializeField] private SoundCollectionAsset sounds;


        protected override void Awake()
        {
            base.Awake();
            Preconditions.isListWithoutDuplicates(objects.ReadOnlyList, "Iobjects list");
            Preconditions.isListWithoutDuplicates(sounds.ReadOnlyList, "sounds list");
        }

        #region Interactable Objects

        /// <summary>
        /// Returns an object with a specific ID.
        /// </summary>
        /// <param name="id">The ID of the object to return.</param>
        /// <returns>The object with the same id.</returns>
        public ObjectAsset GetObjectByID(string id) => objects.GetAssetByID(id);

        /// <summary>
        /// Returns a read-only list of all interactable objects.
        /// </summary>
        public ReadOnlyCollection<ObjectAsset> Objects { get => objects.ReadOnlyList; } 

        #endregion

        #region Sound Asset

        /// <summary>
        /// Returns a sound with a specific ID.
        /// </summary>
        /// <param name="id">The ID of the sound to return.</param>
        /// <returns>The sound with the same id.</returns>
        public SoundAsset GetSoundByID(string id) => sounds.GetAssetByID(id);

        /// <summary>
        /// Returns a read-only list of all sound assets.
        /// </summary>
        public IList<SoundAsset> Sounds { get => sounds.ReadOnlyList; }

        #endregion
    }
}