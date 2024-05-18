using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SymbolTable
{
    public const int SYMBOL_TYPE_COUNT = 6;

    public int[] table;

    //ctor for player
    public SymbolTable()
    {
        table = new int[SYMBOL_TYPE_COUNT];
    }

    //ctor for heroes
    public SymbolTable(int n)
    {
        table = new int[SYMBOL_TYPE_COUNT];
        table[n] = 1;
    }

    public void Add(SymbolTable toAdd)
    {
        for (int i = 0; i < SYMBOL_TYPE_COUNT; i++)
        {
            table[i] += toAdd.table[i];
        }
    }

    public void Remove(SymbolTable toRemove)
    {
        for (int i = 0; i < SYMBOL_TYPE_COUNT; i++)
        {
            table[i] -= toRemove.table[i];
        }
    }

    // return if this table contains the amount of symbols another table has
    public bool Contains(SymbolTable target)
    {
        for (int i = 0; i < SYMBOL_TYPE_COUNT; i++)
        {
            if (table[i] < target.table[i])
                return false;
        }
        return true;
    }

    // print table to log for testing
    public string PrintTable()
    {
        string output = "";
        for (int i = 0; i < SYMBOL_TYPE_COUNT; i++)
        {
            output += (Symbols)i;
            output += table[i] + "\n";
        }
        Debug.Log(output);
        return output;
    }

    public string ToShortString()
    {
        string output = "";
        for (int i = 0; i < SYMBOL_TYPE_COUNT; i++)
        {
            output += table[i];
        }
        return output;
    }

    public override string ToString()
    {
        string output = "";
        for (int i = 0; i < SYMBOL_TYPE_COUNT; i++)
        {
            output += (Symbols)i + " " + table[i] + "\n";
        }
        return output;
    }

    public enum Symbols
    {
        Fighter,
        Mage,
        Ranger,
        Rogue,
        Mender,
        Defense
    }
}
