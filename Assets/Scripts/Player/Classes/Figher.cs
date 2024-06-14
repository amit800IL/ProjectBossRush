using UnityEngine;

public class Figher : Hero
{
    protected override void Start()
    {
        base.Start();
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);


    }
    public override bool HeroAttackBoss(Boss boss)
    {
        if (CanHeroAttack())
        {
            attackingParticle.Play();
            boss.TakeDamage(HeroData.damage);
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
        return AttackPosCondition(currentTile);
    }

    public override bool CanHeroDefend()
    {
        return DefendPosCondition(currentTile);
    }

    public override bool AttackPosCondition(Tile tile)
    {
        return tile != null && tile.IsTileOfType(TileType.CloseRange);
    }

    public override bool DefendPosCondition(Tile tile)
    {
        return !tile.IsTileOfType(TileType.LongRange);
    }
}
