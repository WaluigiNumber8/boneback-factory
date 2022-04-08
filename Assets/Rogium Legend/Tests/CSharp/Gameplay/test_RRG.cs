using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.AssetRandomGenerator;
using UnityEditor;
using UnityEngine.TestTools;

namespace Rogium_Legend.Tests.CSharp.Gameplay
{
    public class test_RRG
    {
        private ExternalLibraryOverseer lib;
        private RRG rrg;
        
        private IList<RoomAsset> rooms;

        [SetUp]
        public void Setup()
        {
            lib = ExternalLibraryOverseer.Instance;
            lib.ReloadFromExternalStorage();
            rooms = lib.GetCampaignsCopy[0].DataPack.Rooms;
            
            rrg = new RRG(rooms, 0);
        }
        
        [Test]
        public void random_number_is_from_proper_difficulty_single()
        {
            RoomType type = RoomType.Normal;
            int difficulty = 0;
            
            RoomAsset room = rooms[rrg.LoadNext(type, difficulty)];
            
            Assert.AreEqual(difficulty, room.DifficultyLevel);
        }
        
        [Test]
        public void random_number_is_from_proper_difficulty_multiple()
        {
            RoomType type = RoomType.Normal;
            int difficulty = 0;
            
            RoomAsset room1 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room2 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room3 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room4 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room5 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room6 = rooms[rrg.LoadNext(type, difficulty)];
            
            Assert.AreEqual(difficulty, room1.DifficultyLevel);
            Assert.AreEqual(difficulty, room2.DifficultyLevel);
            Assert.AreEqual(difficulty, room3.DifficultyLevel);
            Assert.AreEqual(difficulty, room4.DifficultyLevel);
            Assert.AreEqual(difficulty, room5.DifficultyLevel);
            Assert.AreEqual(difficulty, room6.DifficultyLevel);
            
        }
        
        [Test]
        public void random_number_is_from_proper_difficulty_single_alt()
        {
            RoomType type = RoomType.Normal;
            int difficulty = 2;
            
            RoomAsset room = rooms[rrg.LoadNext(type, difficulty)];
            
            Assert.AreEqual(difficulty, room.DifficultyLevel);
        }
        
        [Test]
        public void random_number_is_from_proper_difficulty_multiple_alt()
        {
            RoomType type = RoomType.Normal;
            int difficulty = 2;
            
            RoomAsset room1 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room2 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room3 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room4 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room5 = rooms[rrg.LoadNext(type, difficulty)];
            RoomAsset room6 = rooms[rrg.LoadNext(type, difficulty)];
            
            Assert.AreEqual(difficulty, room1.DifficultyLevel);
            Assert.AreEqual(difficulty, room2.DifficultyLevel);
            Assert.AreEqual(difficulty, room3.DifficultyLevel);
            Assert.AreEqual(difficulty, room4.DifficultyLevel);
            Assert.AreEqual(difficulty, room5.DifficultyLevel);
            Assert.AreEqual(difficulty, room6.DifficultyLevel);
            
        }
        
    }
}