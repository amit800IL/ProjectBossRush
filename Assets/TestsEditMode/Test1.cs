using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class Test1
{
    //private Boss boss;
    // A Test behaves as an ordinary method
    [Test]
    public void Test1SimplePasses()
    {
        // Use the Assert class to test conditions
    }

    [Test]
    public void isTakingDamage()
    {
       // boss = new Boss();
       // boss.TakeDamage(10);
      //  Assert.AreEqual(90, boss.HP);
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Test1WithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
