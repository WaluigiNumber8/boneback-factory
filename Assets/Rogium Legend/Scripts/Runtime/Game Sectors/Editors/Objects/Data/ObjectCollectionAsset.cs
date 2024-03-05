using System.Collections.Generic;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Objects
{
    [CreateAssetMenu(fileName = "New Object Library", menuName = EditorConstants.AssetMenuAssets + "Object Library", order = 0)]
    public class ObjectCollectionAsset : ScriptableObject, IInternalAssetCollectionAsset<ObjectAsset>
    {
        [SerializeField] private List<ObjectAsset> interactableObjects;

        public ObjectAsset GetAssetByID(string id) => interactableObjects.FindValueFirst(id);
        public IList<ObjectAsset> GetAssetListCopy() => new List<ObjectAsset>(interactableObjects);
    }
}