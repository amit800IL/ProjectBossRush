using UnityEngine;

public class Figher : Hero
{
    [SerializeField] private BerzekerProjectile berzekerProjectile;
    protected override void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
        base.Start();
    }

    public override bool CanHeroAttack(Boss boss)
    {
        if (AttackPosCondition(currentTile))
        {
            berzekerProjectile.StartCoroutine(berzekerProjectile.MoveProjectile(boss.transform.position));
        }
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
