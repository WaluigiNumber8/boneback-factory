using System.Collections.Generic;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Objects
{
    [CreateAssetMenu(fileName = "New Object Library", menuName = EditorConstants.AssetMenuAssets + "Object Library", order = 0)]
    public class ObjectLibraryAsset : ScriptableObject
    {
        [SerializeField] private List<ObjectAsset> interactableObjects;

        public ObjectAsset GetObjectByID(string id) => interactableObjects.FindValueFirst(id);
        public IList<ObjectAsset> GetObjectsCopy() => new List<ObjectAsset>(interactableObjects);
    }
}