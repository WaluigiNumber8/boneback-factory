using System.Collections.Generic;
using BoubakProductions.Core;
using Rogium.Editors.Objects;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the in-game prebuild assets, that can only be created in the editor.
    /// </summary>
    public class InternalLibraryOverseer : MonoSingleton<InternalLibraryOverseer>
    {
        [SerializeField] private List<ObjectAsset> objects;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
        
        /// <summary>
        /// Returns a copy of the array of interactable objects stored here.
        /// </summary>
        /// <returns>A copy of Pack Library.</returns>
        public IList<ObjectAsset> GetObjectsCopy() => new List<ObjectAsset>(objects);
    }
}