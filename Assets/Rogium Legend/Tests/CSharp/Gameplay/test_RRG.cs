using BoubakProductions.Systems.Randomization;
using NUnit.Framework;
using Rogium.Gameplay.AssetRandomGenerator;
using UnityEngine.TestTools;

public class test_RRG
{
    private IRandomizer randomizerRegion;
    
    [SetUp]
    public void Setup()
    {
        randomizerRegion = new RandomizerRegion(0, 4, 0f);
    }
    
    [Test]
    public void generate_random_number_region()
    {
        int randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        randomValue = randomizerRegion.GetNext();
        Assert.IsInstanceOf(typeof(int), randomValue);
    }

}