using UnityEngine;
public class Mage : Hero
{
    protected override void Start()
    {
        base.Start();
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
        if (CurrentTile != null && CurrentTile.IsTileOfType(TileType.CloseRange))
        {
            return true;
        }
        return false;
    }
}