using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BossTest
{
    private Boss bossScript;
    GameObject bossGameObject;
    [SetUp]
    public void Setup()
    {
        bossGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Boss/BossItself/Boss"));
        bossScript = bossGameObject.GetComponent<Boss>();
    }

    //Testins that the boss gameObject exist on the scene

    [Test]
    public void BossExists()
    {
        Assert.IsNotNull(bossGameObject);
    }

    //Testing that the boss script exist on the scene

    [Test]
    public void BossScriptExists()
    {
        Assert.IsNotNull(bossScript);
    }

    //Testins the the boss starts with his defined max health by checking that health is equal to max health at start

    [Test]
    public void BossStartingWithMaxHealth()
    {
        Assert.AreEqual(bossScript.HP, bossScript.maxHP);
    }

    // Testing that the boss get the currect amount of damage by setting the same number for hp
    //As the take damage method should return and compare them

    [Test]
    public void BossGettingRightAmountOfDamage()
    {
        float damage = 10f;
        float health = bossScript.HP;
        float hpAfterDamage = health - damage;
        bossScript.TakeDamage(damage);
        float hpAfterDamageMethod = bossScript.HP;
        Assert.AreEqual(hpAfterDamage, hpAfterDamageMethod);
    }

    // Testing that the boss cannot take negative amount of damage, checks if health do not go up by double negativity calculation 

    [Test]
    public void BossDoesNotTakeNegativeDamage()
    {
        float damage = -10f;
        float health = bossScript.HP;
        bossScript.TakeDamage(damage);
        Assert.Greater(health, bossScript.HP);
    }

    //Checking that boss hp cannot go below 0 by checking that health is equal to 0

    [Test]
    public void BossHealthDoesNotGoBelow0()
    {
        float damage = bossScript.HP + 10f;
        float health = bossScript.HP;
        bossScript.TakeDamage(damage);
        Assert.LessOrEqual(0f, bossScript.HP);
    }
}
