
using UnityEngine;
using UnityEditor;

public class AttackPlayer : EnemyAction
{
    [SerializeField] public int damage;
    public override void DoActionOnHero(Hero hero)
    {
        AttackHero(hero);
    }
    public void AttackHero(Hero hero)
    {
        hero.TakeDamage(damage);
    }

}
