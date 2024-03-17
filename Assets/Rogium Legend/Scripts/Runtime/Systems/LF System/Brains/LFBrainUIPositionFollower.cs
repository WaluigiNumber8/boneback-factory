using RedRats.Systems.LiteFeel.Core;
using Rogium.UserInterface.Editors.Utils;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to a <see cref="UIPositionFollower"/>.
    /// </summary>
    public class LFBrainUIPositionFollower : MonoBehaviour
    {
        [SerializeField] private UIPositionFollower follower;
        [Space]
        [SerializeField] private LFEffector onBeginMoveEffector;

        private void OnEnable() => follower.OnBeginMove += onBeginMoveEffector.Play;
        private void OnDisable() => follower.OnBeginMove -= onBeginMoveEffector.Play;
    }
}