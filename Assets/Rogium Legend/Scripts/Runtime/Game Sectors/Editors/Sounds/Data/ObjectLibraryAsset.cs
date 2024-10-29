using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Sounds;
using UnityEngine;

namespace Rogium.Editors.Objects
{
    [CreateAssetMenu(fileName = "New Sound Library", menuName = EditorAssetPaths.AssetMenuAssets + "Sound Library", order = 0)]
    public class SoundCollectionAsset : ScriptableObject, IInternalAssetCollectionAsset<SoundAsset>
    {
        [SerializeField] private List<SoundAsset> sounds;

        public SoundAsset GetAssetByID(string id) => sounds.FindValueFirstOrDefault(id);
        
        public ReadOnlyCollection<SoundAsset> ReadOnlyList { get => new(sounds); }
    }
}