
using UnityEngine;
using UnityEditor;

public class AttackPlayer : EnemyAction
{
    [SerializeField] private Boss boss;
    public override void DoActionOnHero(Hero hero)
    {
        AttackHero(hero);
    }
    public void AttackHero(Hero hero)
    {
        hero.TakeDamage((int)boss.Damage);
    }

}
