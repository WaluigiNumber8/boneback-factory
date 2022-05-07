using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.DataLoading;
using Rogium.Gameplay.Entities.Player;
using Rogium.Gameplay.InteractableObjects;
using Rogium.Systems.SAS;
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
        [Space]
        [SerializeField] private float transportRunSpeed;
        [SerializeField] private float transportWalkSpeed;

        private SASCore sas;
        private IDictionary<Vector2, Vector2> startingPoints;
        private Transform playerTransform;

        private void Awake()
        {
            sas = SASCore.GetInstance();
            startingPoints = new Dictionary<Vector2, Vector2>();
            playerTransform = player.transform;
        }

        private void OnEnable() => InteractObjectStartingPoint.OnConstruct += startingPoints.Add;
        private void OnDisable() => InteractObjectStartingPoint.OnConstruct -= startingPoints.Add;

        /// <summary>
        /// Runs the opening gameplay sequence.
        /// </summary>
        public void RunIntro(int difficultyTier)
        {   
            roomLoader.Init();
            StartCoroutine(RunIntroCoroutine(difficultyTier));
            IEnumerator RunIntroCoroutine(int tier)
            {
                player.SetCollideMode(false);
                startingPoints.Clear();

                roomLoader.LoadNext(RoomType.Entrance, tier);
                (Vector2 pos, Vector2 dir) = startingPoints.ElementAt(GetPlayerStartPositionIndex());

                playerTransform.position = pos - dir * 2;
                yield return sas.Wait(0.5f);
                yield return sas.FadeIn(2, false);
                yield return sas.Transport(playerTransform, playerTransform.position + (Vector3)dir * 3, transportWalkSpeed * 0.5f);
            
                player.SetCollideMode(true);
            }
        }

        /// <summary>
        /// Runs the transition between rooms.
        /// </summary>
        public void RunTransition(RoomType nextRoomType, Vector2 direction, int difficultyTier)
        {
            StartCoroutine(RunTransitionCoroutine(nextRoomType, direction, difficultyTier));
            IEnumerator RunTransitionCoroutine(RoomType roomType, Vector2 direction, int tier)
            {
                player.SetCollideMode(false);
                startingPoints.Clear();
            
                yield return sas.FadeOut(0.5f, true);
                yield return sas.Transport(playerTransform, playerTransform.position + (Vector3)direction, transportWalkSpeed);
            
                roomLoader.LoadNext(roomType, tier);
                (Vector2 pos, Vector2 dir) = startingPoints.ElementAt(GetPlayerStartPositionIndex());
            
                yield return sas.Transport(playerTransform, pos, transportRunSpeed);
                yield return sas.FadeIn(1f, false);
                yield return sas.Transport(playerTransform, playerTransform.position + (Vector3)dir, transportWalkSpeed);
                player.SetCollideMode(true);
            }
        }

        /// <summary>
        /// Runs the gameplay finish transition.
        /// </summary>
        public IEnumerator RunEndCoroutine(Vector2 direction)
        {
            player.SetCollideMode(false);
            startingPoints.Clear();
            
            yield return sas.Transport(playerTransform, playerTransform.position + (Vector3)direction, transportWalkSpeed);
            yield return sas.FadeOut(3f, true);
            
            player.gameObject.SetActive(false);
            yield return sas.Wait(1f);
            roomLoader.Clear();
        }

        public IEnumerator RunGameOverCoroutine()
        {
            yield return sas.FadeOut(3f, true);
            yield return sas.Wait(0.5f);
            roomLoader.Clear();
        }
        
        //Decides on, which position will the start in the next room.
        private int GetPlayerStartPositionIndex()
        {
            if (startingPoints.Count <= 0) throw new InvalidOperationException("The list of starting positions cannot be empty.");
            if (startingPoints.Count == 1) return 0;
            return Random.Range(0, startingPoints.Count);
        }
    }
}