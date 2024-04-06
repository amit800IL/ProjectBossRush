using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Hero
{
    private void Start()
    {
        movementAmount = 2;
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Ranger);
    }
    public override void HeroAttackBoss(Boss boss)
    {
        if (CanHeroAttack())
            boss.TakeDamage(damage);
        else
            Debug.Log("Hero can't attack");
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
