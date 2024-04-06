using UnityEngine;
public class Mage : Hero
{
    private void Start()
    {
        movementAmount = 10;
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Mage);
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
        if (CurrentTile != null && (CurrentTile.IsTileOfType(TileType.CloseRange) || CurrentTile.IsTileOfType(TileType.MediumRange)))
        {
            return true;
        }
        return false;
    }
}
