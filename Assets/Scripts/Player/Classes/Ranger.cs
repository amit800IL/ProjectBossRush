using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Hero
{
    protected override void Start()
    {
        base.Start();
    }
    public override bool CanHeroAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void HeroAttackBoss(Boss boss)
    {
        throw new System.NotImplementedException();
    }
}
