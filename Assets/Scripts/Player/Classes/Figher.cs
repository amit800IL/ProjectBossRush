using UnityEngine;

public class Figher : Hero
{
    protected override void Start()
    {
        base.Start();
        maxMovementAmount = 3;
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);

      
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

    public override bool CanHeroAttack()
    {
        if (CurrentTile != null && CurrentTile.IsTileOfType(TileType.CloseRange))
        {
            return true;
        }
        return false;
    }

    public override bool CanHeroDefend()
    {
        return !CurrentTile.IsTileOfType(TileType.LongRange);
    }
}
