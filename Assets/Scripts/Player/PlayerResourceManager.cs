using System;
using TMPro;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static event Action<int> OnAPChanged;

    [SerializeField] SymbolTable symbolCharge = new();
    [SerializeField] private int maxAP;
    [SerializeField] private int AP;

    SymbolTable testTable = new SymbolTable(1);

    private void Start()
    {
        //InitAP();
    }

    #region symbols
    public void AddSymbols(SymbolTable toAdd)
    {
        symbolCharge.Add(toAdd);
    }

    [ContextMenu("add test table")]
    public void AddTestTable()
    {
        AddSymbols(testTable);
    }

    [ContextMenu("print table")]
    public void PrintSymbols()
    {
        symbolCharge.PrintTable();
    }

    [ContextMenu("check contains")]
    public void CheckContains()
    {
        print(symbolCharge.Contains(testTable));
    }

    //public bool UseSymbols(int type, int amount)
    //{
    //    if(symbolCharge[type]>=amount)
    //    {
    //        symbolCharge[type]-=amount;
    //        return true;
    //    }
    //    return false;
    //}
    #endregion

    #region AP
    private void InitAP()
    {
        //get maxAP as AP sum from party
        //temp
        maxAP = 4;
    }

    public void ResetAP()
    {
        AP = maxAP;
        OnAPChanged.Invoke(AP);
    }

    public bool UseAP(int amount)
    {
        if (AP >= amount)
        {
            AP -= amount;
            OnAPChanged?.Invoke(AP);
            Debug.Log(AP);
            return true;
        }
        return false;
    }

    public void ModifyAP(int amount)
    {
        maxAP = amount;
    }

    #endregion
}
