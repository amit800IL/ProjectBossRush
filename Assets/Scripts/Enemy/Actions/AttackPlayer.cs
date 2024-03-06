
using UnityEngine;
using UnityEditor;

public class AttackPlayer : EnemyAction
{
    public void AttackHero(Hero hero)
    {
        hero.HealthDown();

        Debug.Log("Player Attacked");
    }
}
