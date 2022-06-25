using RedRats.Systems.Randomization;
using NUnit.Framework;
using Rogium.Gameplay.AssetRandomGenerator;
using UnityEngine.TestTools;

public class test_Randomizer_Region
{
    private IRandomizer randomizerRegion;
    
    
    [Test]
    public void generate_random_number_region()
    {
        randomizerRegion = new RandomizerRegion(4);
        
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
    
    [Test]
    public void generator_works_with_small_numbers()
    {
        randomizerRegion = new RandomizerRegion(1);
        
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