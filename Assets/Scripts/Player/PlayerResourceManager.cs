using System;
using TMPro;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static event Action<int> OnAPChanged;

    [SerializeField] SymbolTable symbolCharge = new();
    [SerializeField] private int maxAP;
    [SerializeField] private int AP;
    [SerializeField] private Technique selectedTechnique;

    [SerializeField] SymbolTable testTable = new SymbolTable(1);

    private void Start()
    {
        Technique.SelectTechnique += SetSelectedTechnique;

        OnAPChanged?.Invoke(AP);
        //InitAP();
    }

    private void SetSelectedTechnique(Technique selected)
    {
        selectedTechnique = selected;
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

    [ContextMenu("check contains symbols")]
    public bool ContainsSymbols(SymbolTable toCheck)
    {
        //print(symbolCharge.Contains(testTable));
        return symbolCharge.Contains(testTable);
    }

    [ContextMenu("Use technique")]
    public void UseTechnique()
    {
        if (ContainsSymbols(selectedTechnique.GetRequirements()))
        {
            UseSymbols(selectedTechnique.GetRequirements());
            //implement use
            selectedTechnique.GetTechData();
        }
    }

    public void UseSymbols(SymbolTable toUse)
    {
        symbolCharge.Remove(toUse);
    }
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
