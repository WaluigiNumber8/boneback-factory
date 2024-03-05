using System.Collections.Generic;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Sounds;
using UnityEngine;

namespace Rogium.Editors.Objects
{
    [CreateAssetMenu(fileName = "New Sound Library", menuName = EditorConstants.AssetMenuAssets + "Sound Library", order = 0)]
    public class SoundCollectionAsset : ScriptableObject, IInternalAssetCollectionAsset<SoundAsset>
    {
        [SerializeField] private List<SoundAsset> sounds;

        public SoundAsset GetAssetByID(string id) => sounds.FindValueFirst(id);
        
        public IList<SoundAsset> GetAssetListCopy() => new List<SoundAsset>(sounds);
    }
}