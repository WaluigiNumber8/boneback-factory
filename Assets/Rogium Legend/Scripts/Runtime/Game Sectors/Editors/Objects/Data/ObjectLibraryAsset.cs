using System.Collections.Generic;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Objects
{
    [CreateAssetMenu(fileName = "New Object Library", menuName = EditorDefaults.AssetMenuAssets + "Object Library", order = 0)]
    public class ObjectLibraryAsset : ScriptableObject
    {
        [SerializeField] private List<ObjectAsset> interactableObjects;
        
        public IList<ObjectAsset> GetObjectsCopy() => new List<ObjectAsset>(interactableObjects);
    }
}