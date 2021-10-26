using System.Collections;
using NUnit.Framework;
using Rogium.Gameplay.AssetRandomGenerator;
using UnityEditor;
using UnityEngine.TestTools;

public class test_RRG
{
    private const int id = 0;
    private const int collectionSize = 13;
    private const int memorySize = 5;
    private RandomizerOverseer randomizer;

    [SetUp]
    public void Setup()
    {
        randomizer = new RandomizerOverseer();
    }
    
    
    [Test]
    public void can_register_ids_correctly()
    {
        randomizer.Register(id, collectionSize, memorySize);
        
        Assert.AreEqual(1, randomizer.IDs.Count);
        Assert.AreEqual(id, randomizer.IDs[0].ID);
        Assert.AreEqual(collectionSize, randomizer.IDs[0].CollectionLength);
        Assert.AreEqual(memorySize, randomizer.IDs[0].MemorySize);
    }

    [Test]
    public void generator_can_generate_random_numbers()
    {
        randomizer.Register(id, collectionSize, memorySize);
        int randomValue = randomizer.GetNext(id);
        
        Assert.IsInstanceOf(typeof(int), randomValue);
    }

}