using UnityEngine;
public class Mage : Hero
{
    [SerializeField] private ParticleSystem fireBall;

    [SerializeField] private ParticleSystem[] fireBallLifetimeObjects;

    protected override void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Mage);
        base.Start();
    }
    public override bool CanHeroAttack(Boss boss)
    {
        fireBall.Play();

        SetFireballTargetPosition(boss.transform.position);

        return AttackPosCondition(currentTile);
    }

    private void SetFireballTargetPosition(Vector3 targerPosition)
    {
        foreach (ParticleSystem particePart in fireBallLifetimeObjects)
        {
            ParticleSystem.MainModule main = particePart.main;

            float distance = Vector3.Distance(particePart.transform.position, targerPosition);

            ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = particePart.velocityOverLifetime;

            velocityOverLifetime.enabled = true;

            ParticleSystem.MinMaxCurve xCurve = new ParticleSystem.MinMaxCurve(distance / main.startLifetime.constant);
            ParticleSystem.MinMaxCurve yCurve = new ParticleSystem.MinMaxCurve(particePart.velocityOverLifetime.y.constant);
            ParticleSystem.MinMaxCurve zCurve = new ParticleSystem.MinMaxCurve(particePart.velocityOverLifetime.z.constant);

            velocityOverLifetime.x = xCurve;
            velocityOverLifetime.y = yCurve;
            velocityOverLifetime.z = zCurve;
        }
    }

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
