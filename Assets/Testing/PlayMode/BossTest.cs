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
        bossGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Boss/BossItself/Boss"));
        bossScript = bossGameObject.GetComponent<Boss>();
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
    public void BossStartingWithMaxHealth() //1
    {
        Assert.AreEqual(bossScript.HP, bossScript.maxHP);
    }
    [Test]
    public void BossGettingRightAmountOfDamage() //2
    {
        float damage = 10f;
        float health = bossScript.HP;
        float hpAfterDamage = health - damage;
        bossScript.TakeDamage(damage);
        float hpAfterDamageMethod = bossScript.HP;
        Assert.AreEqual(hpAfterDamage, hpAfterDamageMethod);
    }
    [Test]
    public void BossDoesNotTakeNegativeDamage() //3
    {
        float damage = -10f;
        float health = bossScript.HP;
        bossScript.TakeDamage(damage);
        Assert.Greater(health, bossScript.HP);
    }
    [Test]
    public void BossHealthDoesNotGoBelow0() //4
    {
        float damage = bossScript.HP + 10f;
        float health = bossScript.HP;
        bossScript.TakeDamage(damage);
        Assert.LessOrEqual(0f, bossScript.HP);
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
