using UnityEngine;

public class Figher : Hero
{
    protected override void Start()
    {
        base.Start();
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
    }
    public override bool CanHeroAttack()
    {
        if (overLappedPoint && CurrentTile.IsTileOfType(TileType.CloseRange))
        {
            return true;
        }

        return false;
    }
}
