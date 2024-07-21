using UnityEngine;
public class Mage : Hero
{
    [SerializeField] private Transform objectTransform;

    protected override void Awake()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Mage);
        base.Awake();
    }
    public override bool CanHeroAttack(Boss boss)
    {
        attackVFX.SetVector3("Position Start", objectTransform.position);
        attackVFX.SetVector3("Position End", boss.transform.position);

        return AttackPosCondition(currentTile);
    }

    // Kremer, please Remove this method if it is not used
    //private void SetFireballTargetPosition(Vector3 targerPosition)
    //{
    //    foreach (ParticleSystem particePart in fireBallLifetimeObjects)
    //    {
    //        ParticleSystem.MainModule main = particePart.main;

    //        float distance = Vector3.Distance(particePart.transform.position, targerPosition);

    //        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = particePart.velocityOverLifetime;

    //        velocityOverLifetime.enabled = true;

    //        ParticleSystem.MinMaxCurve xCurve = new ParticleSystem.MinMaxCurve(distance / main.startLifetime.constant);
    //        ParticleSystem.MinMaxCurve yCurve = new ParticleSystem.MinMaxCurve(particePart.velocityOverLifetime.y.constant);
    //        ParticleSystem.MinMaxCurve zCurve = new ParticleSystem.MinMaxCurve(particePart.velocityOverLifetime.z.constant);

    //        velocityOverLifetime.x = xCurve;
    //        velocityOverLifetime.y = yCurve;
    //        velocityOverLifetime.z = zCurve;
    //    }
    //}

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
