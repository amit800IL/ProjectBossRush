using UnityEngine;
public class Mage : Hero
{
    protected override void Start()
    {
        base.Start();
        maxMovementAmount = 3;
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Mage);
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

        OnHeroDefenceChanged.Invoke((int)Defense);
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
