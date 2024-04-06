using UnityEngine;

public class Figher : Hero
{
    protected void Start()
    {
        movementAmount = 10;
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
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
        if (CurrentTile != null && CurrentTile.IsTileOfType(TileType.CloseRange))
        {
            return true;
        }
        return false;
    }

}
