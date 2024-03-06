using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figher : Hero
{
    private void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
    }
}
