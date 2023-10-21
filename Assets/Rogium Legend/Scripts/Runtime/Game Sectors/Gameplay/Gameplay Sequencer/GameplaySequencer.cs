using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;
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
        public event Action OnRoomLoaded;
        
        [SerializeField] private RoomLoader roomLoader;
        [SerializeField] private PlayerController player;
        [Space]
        [SerializeField] private float transportRunSpeed;
        [SerializeField] private float transportWalkSpeed;
        [SerializeField] private Vector2 defaultStartPos;
        /// <summary>
        /// The delay before the player enters the scene.
        /// </summary>
        [Tooltip("The delay before the player enters the scene.")]
        [Space]
        [SerializeField, Min(0)] private float beforeIntroDelay;
        

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
        public void RunIntro(int nextRoomIndex)
        {   
            roomLoader.Init();
            
            StartCoroutine(RunIntroCoroutine(nextRoomIndex));
            IEnumerator RunIntroCoroutine(int roomIndex)
            {
                player.ChangeCollideMode(false);
                startingPoints.Clear();

                roomLoader.LoadNext(roomIndex);
                OnRoomLoaded?.Invoke();
                (Vector2 pos, Vector2 dir) = startingPoints.ElementAt(GetPlayerStartPositionIndex());
                
                playerTransform.position = pos - dir * 2;
                yield return sas.Wait(0.5f);
                yield return sas.FadeIn(2, false);
                yield return sas.Wait(beforeIntroDelay);
                yield return sas.Transport(playerTransform, playerTransform.position + (Vector3)dir * 2, transportWalkSpeed * 0.5f);
            
                player.ChangeCollideMode(true);
            }
        }

        /// <summary>
        /// Runs the transition between rooms.
        /// </summary>
        public void RunTransition(int nextRoomIndex, Vector2 direction)
        {
            StartCoroutine(RunTransitionCoroutine(nextRoomIndex, direction));
            IEnumerator RunTransitionCoroutine(int roomIndex, Vector2 direction)
            {
                player.ChangeCollideMode(false);
                startingPoints.Clear();
            
                yield return sas.FadeOut(0.5f, true);
                yield return sas.Transport(playerTransform, playerTransform.position + (Vector3)direction * Random.Range(2, 6), transportWalkSpeed);
            
                roomLoader.LoadNext(roomIndex);
                OnRoomLoaded?.Invoke();
                (Vector2 pos, Vector2 dir) = startingPoints.ElementAt(GetPlayerStartPositionIndex());

                yield return sas.Transport(playerTransform, new Vector2(Random.Range(-7.5f, 6.5f), Random.Range(-4.5f, 4.5f)), transportRunSpeed);
                yield return sas.Transport(playerTransform, pos - dir * 2f, transportRunSpeed);
                yield return sas.FadeIn(1f, false);
                yield return sas.Transport(playerTransform, pos, transportWalkSpeed);
                player.ChangeCollideMode(true);
            }
        }

        /// <summary>
        /// Runs the gameplay finish transition.
        /// </summary>
        public IEnumerator RunEndCoroutine(Vector2 direction)
        {
            player.ChangeCollideMode(false);
            startingPoints.Clear();
            
            yield return sas.FadeOut(1.75f, true);
            yield return sas.Wait(0.15f);
            yield return sas.Transport(playerTransform, playerTransform.position + (Vector3)direction, transportWalkSpeed);
            
            player.gameObject.SetActive(false);
            yield return sas.Wait(0.25f);
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
            if (startingPoints.Count <= 0)
            {
                SafetyNet.ThrowMessage("There cannot be 0 starting points in a room!");
                startingPoints.Add(defaultStartPos, Vector2.down);
                return 0;
            }
            if (startingPoints.Count == 1) return 0;
            return Random.Range(0, startingPoints.Count);
        }

    }
}