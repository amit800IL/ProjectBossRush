using System;
using UnityEngine;
public class Mage : Hero
{
    protected override void Start()
    {
        base.Start();
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Mage);
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
        return (
            tile.IsTileOfType(TileType.MediumRange) ||
            tile.IsTileOfType(TileType.LongRange) && !tile.IsTileOfType(TileType.Flank)
        );
    }

    public override bool DefendPosCondition(Tile tile)
    {
        return (
            tile.IsTileOfType(TileType.MediumRange) && !tile.IsTileOfType(TileType.Flank) ||
            tile.IsTileOfType(TileType.LongRange) && tile.IsTileOfType(TileType.Flank)
        );
    }
}
