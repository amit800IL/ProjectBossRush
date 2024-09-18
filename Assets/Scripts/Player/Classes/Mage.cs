using UnityEngine;
public class Mage : Hero
{
    [SerializeField] private MageProjectile mageProjectile;

    protected override void Start()
    {
        //SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Mage);
        base.Start();
    }
    public override bool CanHeroAttack(Boss boss)
    {
        if (AttackPosCondition(currentTile))
        {
            Vector3 positionOffset = new Vector3(0, 2, 0);
            mageProjectile.MoveProjectile(boss.transform.position + positionOffset);
        }
        return AttackPosCondition(currentTile);
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
