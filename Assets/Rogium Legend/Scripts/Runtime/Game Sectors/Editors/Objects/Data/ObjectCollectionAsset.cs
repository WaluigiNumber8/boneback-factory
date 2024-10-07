using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Objects
{
    [CreateAssetMenu(fileName = "New Object Library", menuName = EditorAssetPaths.AssetMenuAssets + "Object Library", order = 0)]
    public class ObjectCollectionAsset : ScriptableObject, IInternalAssetCollectionAsset<ObjectAsset>
    {
        [SerializeField] private List<ObjectAsset> interactableObjects;

        public ObjectAsset GetAssetByID(string id) => interactableObjects.FindValueFirst(id);
        public ReadOnlyCollection<ObjectAsset> ReadOnlyList { get => new(interactableObjects); }
    }
}