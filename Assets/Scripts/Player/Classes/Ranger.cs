using UnityEngine;

public class Ranger : Hero
{

    [SerializeField] private ParticleSystem arrowVFX;

    [SerializeField] private ParticleSystem arrowLifeTimeObject;
    protected override void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Ranger);
        base.Start();
    }
    public override bool CanHeroAttack(Boss boss)
    {
        return AttackPosCondition(currentTile);
    }

    public override bool HeroAttackBoss(Boss boss)
    {
        if (CanHeroAttack(boss))
        {
            SetArrowTargetPosition(boss.transform.position);
            arrowVFX.Play();
        }

        return base.HeroAttackBoss(boss);
    }

    private void SetArrowTargetPosition(Vector3 targerPosition)
    {
        ParticleSystem.MainModule main = arrowLifeTimeObject.main;

        float distance = Vector3.Distance(arrowLifeTimeObject.transform.position, targerPosition);

        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = arrowLifeTimeObject.velocityOverLifetime;

        velocityOverLifetime.enabled = true;

        ParticleSystem.MinMaxCurve xCurve = new ParticleSystem.MinMaxCurve(arrowLifeTimeObject.velocityOverLifetime.x.constant);
        ParticleSystem.MinMaxCurve yCurve = new ParticleSystem.MinMaxCurve(-distance / main.startLifetime.constant);
        ParticleSystem.MinMaxCurve zCurve = new ParticleSystem.MinMaxCurve(arrowLifeTimeObject.velocityOverLifetime.z.constant);

        velocityOverLifetime.x = xCurve;
        velocityOverLifetime.y = yCurve;
        velocityOverLifetime.z = zCurve;
    }

    public override bool CanHeroDefend()
    {
        return DefendPosCondition(currentTile);
    }

    public override bool AttackPosCondition(Tile tile)
    {
        return tile != null && tile.IsTileOfType(TileType.LongRange);
    }

    public override bool DefendPosCondition(Tile tile)
    {
        return tile.IsTileOfType(TileType.LongRange);
    }
}
