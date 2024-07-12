using System.Collections;
using UnityEngine;

public class Ranger : Hero
{
    protected override void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Ranger);
        base.Start();
    }
    public override bool CanHeroAttack(Boss boss)
    {
        return AttackPosCondition(currentTile);
    }

    protected override IEnumerator ActivateAttackVfx(Boss boss)
    {
        yield return base.ActivateAttackVfx(boss);
    }

    //private void SetArrowTargetPosition(Vector3 targerPosition)
    //{
    //    ParticleSystem.MainModule main = arrowLifeTimeObject.main;

    //    float distance = Vector3.Distance(arrowLifeTimeObject.transform.position, targerPosition);

    //    ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = arrowLifeTimeObject.velocityOverLifetime;

    //    velocityOverLifetime.enabled = true;

    //    ParticleSystem.MinMaxCurve xCurve = new ParticleSystem.MinMaxCurve(arrowLifeTimeObject.velocityOverLifetime.x.constant);
    //    ParticleSystem.MinMaxCurve yCurve = new ParticleSystem.MinMaxCurve(-distance / main.startLifetime.constant);
    //    ParticleSystem.MinMaxCurve zCurve = new ParticleSystem.MinMaxCurve(arrowLifeTimeObject.velocityOverLifetime.z.constant);

    //    velocityOverLifetime.x = xCurve;
    //    velocityOverLifetime.y = yCurve;
    //    velocityOverLifetime.z = zCurve;
    //}

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
