using UnityEngine;

public class Figher : Hero
{
    private void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
    }
    public override bool CanHeroAttack()
    {
        if (raycastHit && CurrentTile.IsTileOfType(TileType.CloseRange))
        {
            return true;
        }

        return false;
    }
}
