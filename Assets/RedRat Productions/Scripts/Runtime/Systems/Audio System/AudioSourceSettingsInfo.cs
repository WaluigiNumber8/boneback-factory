using System;
using UnityEngine;

namespace RedRats.Systems.Audio
{
    /// <summary>
    /// Contains settings used for an <see cref="AudioSource"/> playback.
    /// </summary>
    [Serializable]
    public struct AudioSourceSettingsInfo
    {
        public int id;
        public bool loop;
        public bool playOnlyWhenNotPlaying;
        public bool muteSameSound;

        public AudioSourceSettingsInfo(int id, bool loop, bool playOnlyWhenNotPlaying, bool muteSameSound)
        {
            this.id = id;
            this.loop = loop;
            this.playOnlyWhenNotPlaying = playOnlyWhenNotPlaying;
            this.muteSameSound = muteSameSound;
        }
    }
}