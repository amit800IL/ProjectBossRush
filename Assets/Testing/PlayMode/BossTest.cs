using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BossTest
{
    // A Test behaves as an ordinary method
    private Boss bossScript;
    GameObject bossGameObject;
    [SetUp]
    public void Setup()
    {
       bossGameObject = new GameObject("Boss");
        bossScript = bossGameObject.AddComponent<Boss>();
        //bossScript.bossAnimator = bossGameObject.AddComponent<Animator>();
        
    }
    [Test]
    public void BossTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }
    [Test]
    public void BossExists()
    {
        Assert.IsNotNull(bossGameObject);
    }
    [Test]
    public void BossScriptExists()
    {
        Assert.IsNotNull(bossScript);
    }
    [Test]
    public void BossStartingWithMaxHealth()
    {
        Assert.AreEqual(bossScript.HP, bossScript.maxHP);
    }
    [Test]
    public void BossGettingRightAmountOfDamage()
    {
        float damage = 10f;
        float health = bossScript.HP;
        Debug.Log($"Initial HP: {health}");
        float hpAfterDamage = health - damage;
        Debug.Log($"HP after damage: {hpAfterDamage}");
        bossScript.TakeDamage(damage);
        float hpAfterDamageMethod = bossScript.HP;
        Debug.Log($"HP after damage method: {hpAfterDamageMethod}");
        Assert.AreEqual(hpAfterDamage, hpAfterDamageMethod );
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BossTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
