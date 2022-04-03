using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.DataLoading;
using Rogium.Gameplay.Entities.Player;
using Rogium.Gameplay.InteractableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Gameplay.Sequencer
{
    /// <summary>
    /// Controls gameplay sequences.
    /// </summary>
    public class GameplaySequencer : MonoBehaviour
    {
        [SerializeField] private RoomLoader roomLoader;
        [SerializeField] private PlayerController player;
        [SerializeField] private float transportSpeed;
        
        private IDictionary<Vector2, Vector2> startingPoints;

        private void Awake()
        {
            startingPoints = new Dictionary<Vector2, Vector2>();
        }

        private void OnEnable()
        {
            InteractObjectDoorLeave.OnTrigger += RunTransition;
            InteractObjectStartingPoint.OnConstruct += startingPoints.Add;
        }

        private void OnDisable()
        {
            InteractObjectDoorLeave.OnTrigger -= RunTransition;
            InteractObjectStartingPoint.OnConstruct -= startingPoints.Add;
        }

        /// <summary>
        /// Runs the opening gameplay sequence.
        /// </summary>
        public void RunIntro()
        {
            startingPoints.Clear();
            roomLoader.LoadEntranceRoom();
            (Vector2 pos, Vector2 dir) = startingPoints.ElementAt(GetPlayerStartPositionIndex());
            
            player.Transport(pos, transportSpeed, Vector2.up, dir);
        }

        /// <summary>
        /// Runs the transition between rooms.
        /// </summary>
        public void RunTransition(RoomType nextRoomType, Vector2 direction)
        {
            startingPoints.Clear();
            roomLoader.LoadNext(nextRoomType);
            (Vector2 pos, Vector2 dir) = startingPoints.ElementAt(GetPlayerStartPositionIndex());
            
            player.Transport(pos, transportSpeed, direction, dir);
        }

        /// <summary>
        /// Runs the gameplay finish transition.
        /// </summary>
        public void RunEnd()
        {
            
        }

        //Decides on, which position will the start in the next room.
        private int GetPlayerStartPositionIndex()
        {
            SafetyNet.EnsureIsNotNull(startingPoints, "List of starting positions");
            if (startingPoints.Count <= 0) throw new InvalidOperationException("The list of starting positions cannot be empty.");
            if (startingPoints.Count == 1) return 0;
            return Random.Range(0, startingPoints.Count);
        }
        
    }
}