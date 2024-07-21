using UnityEngine;

public class Figher : Hero
{
    protected override void Awake()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
        base.Awake();
    }

    public override bool CanHeroAttack(Boss boss)
    {
        return AttackPosCondition(currentTile);
    }

    public override bool CanHeroDefend()
    {
        return DefendPosCondition(currentTile);
    }

    public override bool AttackPosCondition(Tile tile)
    {
        return tile != null && tile.IsTileOfType(TileType.CloseRange);
    }

    public override bool DefendPosCondition(Tile tile)
    {
        return !tile.IsTileOfType(TileType.LongRange);
    }
}
