using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerCharactersTest
{
    private Ranger rangerScript;
    private GameObject rangerGameObject;
    private Figher fighterScript;
    private GameObject fighterGameObject;
    private Mage mageScript;
    private GameObject mageGameObject;
    [SetUp]
    public void Setup()
    {
        rangerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Heroes/Ranger"));
        mageScript = rangerGameObject.GetComponent<Mage>();
        fighterGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Heroes/Fighter"));
        rangerScript = rangerGameObject.GetComponent<Ranger>();
        mageScript = rangerGameObject.GetComponent<Mage>();
        fighterScript = fighterGameObject.GetComponent<Figher>();

    }
    [Test]
    public void HeroesExists()
    {
        //Assert.IsNotNull(rangerGameObject);
        Assert.IsNotNull(fighterGameObject);
        Assert.IsNotNull(mageGameObject);
    }
    [Test]
    public void HeroesScriptsAreNotNull()
    {
       // Assert.IsNotNull(rangerScript);
        Assert.IsNotNull(fighterScript);
        Assert.IsNotNull(mageScript);
    }
    [Test]
    public void HeroesStartingWithMaxHealth()
    {
        Assert.AreEqual(rangerScript.HP, rangerScript.HeroData.maxHP);
        Assert.AreEqual(fighterScript.HP, fighterScript.HeroData.maxHP);
        Assert.AreEqual(mageScript.HP, mageScript.HeroData.maxHP);
    }
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerCharactersTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerCharactersTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
