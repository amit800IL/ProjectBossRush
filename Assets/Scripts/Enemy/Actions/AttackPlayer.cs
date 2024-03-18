
using UnityEngine;
using UnityEditor;

public class AttackPlayer : EnemyAction
{
    public override void DoActionOnHero(Hero hero)
    {
        AttackHero(hero);
    }
    public void AttackHero(Hero hero)
    {
        hero.HealthDown();
    }

}
