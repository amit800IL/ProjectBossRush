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
        mageGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Heroes/Magician"));
        fighterGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Heroes/Fighter"));
        rangerScript = rangerGameObject.GetComponent<Ranger>();
        mageScript = mageGameObject.GetComponent<Mage>();
        fighterScript = fighterGameObject.GetComponent<Figher>();

    }

    //Testing Heroes exist on game by checking they are not null

    [Test]
    public void HeroesExists()
    {
        Assert.IsNotNull(rangerGameObject);
        Assert.IsNotNull(fighterGameObject);
        Assert.IsNotNull(mageGameObject);
    }

    //Testing that hereoes script exist on their gameObjects by checking the scripts are not null

    [Test]
    public void HeroesScriptsAreNotNull() 
    {
        Assert.IsNotNull(rangerScript);
        Assert.IsNotNull(fighterScript);
        Assert.IsNotNull(mageScript);
    }

    //Testing that heroes start with their MaxHp, by checking that maxHP and hp are equal at start

    [Test]
    public void HeroesStartingWithMaxHealth()
    {

        Assert.AreEqual(rangerScript.HeroData.maxHP, rangerScript.HP);
        Assert.AreEqual(fighterScript.HeroData.maxHP, fighterScript.HP);
        Assert.AreEqual(mageScript.HeroData.maxHP, mageScript.HP);
    }


    // Testing that heroes get the currect amount of damage by setting the same number for hp
    //As the take damage method should return and compare them

    [Test]
    public void HeroGettingRightAmountOfDamage()
    {
        int damage = 10;
        int health = rangerScript.HP;
        int hpAfterDamage = health - damage;
        rangerScript.TakeDamage(damage);
        int hpAfterDamageMethod = rangerScript.HP;
        Assert.AreEqual(hpAfterDamage, hpAfterDamageMethod);

        damage = 10;
        health = mageScript.HP;
        hpAfterDamage = health - damage;
        mageScript.TakeDamage(damage);
        hpAfterDamageMethod = mageScript.HP;
        Assert.AreEqual(hpAfterDamage, hpAfterDamageMethod);

        damage = 10;
        health = fighterScript.HP;
        hpAfterDamage = health - damage;
        fighterScript.TakeDamage(damage);
        hpAfterDamageMethod = fighterScript.HP;
        Assert.AreEqual(hpAfterDamage, hpAfterDamageMethod);
    }

    // Testing that heroes cannot take negative amount of damage, checks if health do not go up by double negativity

    [Test]
    public void HeroDoesNotTakeNegativeDamage()
    {
        int damage = -10;
        int health = rangerScript.HP;
        rangerScript.TakeDamage(damage);
        Assert.Greater(health, rangerScript.HP);

        damage = -10;
        health = mageScript.HP;
        mageScript.TakeDamage(damage);
        Assert.Greater(health, mageScript.HP);

        damage = -10;
        health = fighterScript.HP;
        fighterScript.TakeDamage(damage);
        Assert.Greater(health, fighterScript.HP);
    }

    //Checking that heroes hp cannot go below 0 by checking that health is equal to 0

    [Test]
    public void HeroHealthDoesNotGoBelow0() 
    {
        int damage = rangerScript.HP + 10;
        int health = rangerScript.HP;
        rangerScript.TakeDamage(damage);
        Assert.LessOrEqual(0, rangerScript.HP);

        damage = mageScript.HP + 10;
        health = mageScript.HP;
        mageScript.TakeDamage(damage);
        Assert.LessOrEqual(0, mageScript.HP);

        damage = fighterScript.HP + 10;
        health = fighterScript.HP;
        fighterScript.TakeDamage(damage);
        Assert.LessOrEqual(0, fighterScript.HP);
    }
}
