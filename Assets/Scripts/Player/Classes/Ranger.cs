using UnityEngine;

public class Ranger : Hero
{
    private void Start()
    {
        maxMovementAmount = 3;
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Ranger);
    }
    public override bool HeroAttackBoss(Boss boss)
    {
        if (CanHeroAttack())
        {
            attackingParticle.Play();
            boss.TakeDamage(damage);
            return true;
        }
        else
        {
            Debug.Log("Hero can't attack");
            return false;
        }
    }
    public override void HeroDefend(Boss boss)
    {
        defendingParticle.Play();
        HP += 20;

        Debug.Log("Defense HP " + HP);
    }
    public override bool CanHeroAttack()
    {
        if (CurrentTile != null && CurrentTile.IsTileOfType(TileType.LongRange))
        {
            return true;
        }
        return false;
    }

}
