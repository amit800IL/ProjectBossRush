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
        {
            attackingParticle.Play();
            boss.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Hero can't attack");
        }
    }
    public override void HeroDefend(Boss boss)
    {
        defendingParticle.Play();
        HP += Defense;

        Debug.Log("Defense HP " + HP);
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
