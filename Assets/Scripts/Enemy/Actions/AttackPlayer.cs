
using UnityEngine;
using UnityEditor;

//this class should become not monobehavior, and be used as a data structure, to be used in a struct with other values
public class AttackPlayer : EnemyAction
{
    [SerializeField] private Boss boss;
    public override void DoActionOnHero(Hero hero)
    {
        AttackHero(hero);
        hero.SlashParticle.Play();
    }
    public void AttackHero(Hero hero)
    {
        hero.TakeDamage((int)boss.Damage);
    }

    public override void DoActionOnTile(Tile tile)
    {
        Hero hero = (Hero)tile.GetOccupier();

        //bossAnimator.SetTrigger("Attack");

        if (hero != null)
        {
            Debug.Log("Found hero: " + hero.name + " on a tile");
            hero.TakeDamage((int)boss.Damage);
            hero.SlashParticle.Play();

            return;
        }

    }
}
