using UnityEngine;

public class Ranger : Hero
{

    [SerializeField] private ParticleSystem arrowVFX;
    protected override void Start()
    {
        base.Start();
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Ranger);
    }
    public override bool HeroAttackBoss(Boss boss)
    {
        if (CanHeroAttack())
        {
            attackingParticle.Play();
            arrowVFX.Play();
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
        if (CurrentTile != null && CurrentTile.IsTileOfType(TileType.LongRange))
        {
            return true;
        }
        return false;
    }

    public override bool CanHeroDefend()
    {
        return CurrentTile.IsTileOfType(TileType.LongRange);
    }
}
