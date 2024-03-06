using UnityEngine;

public class Figher : Hero
{
    private void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
    }
    public override bool CanHeroAttack()
    {
        Collider2D overLappedPoint = Physics2D.OverlapPoint(transform.position, tileMask);

        tile = overLappedPoint.GetComponent<Tile>();

        if (overLappedPoint && tile.IsTileOfType(TileType.CloseRange))
        {
            return true;
        }

        return false;
    }
}
