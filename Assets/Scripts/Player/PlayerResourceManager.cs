using System;
using TMPro;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static event Action<int> OnAPChanged;
    public static event Action<Effect[], Hero> OnTechniqueUsed;

    [SerializeField] SymbolTable symbolCharge = new();
    [SerializeField] private int maxAP;
    [SerializeField] private int AP;
    [SerializeField] private Technique[] techniques;
    [SerializeField] private Technique selectedTechnique;
    [SerializeField] private Hero selectedHero;

    [SerializeField] SymbolTable testTable = new SymbolTable(1);

    private void Start()
    {
        Technique.SelectTechnique += SetSelectedCombo;
        TurnsManager.OnPlayerTurnStart += RollCooldowns;
        TurnsManager.OnPlayerTurnStart += ResetAP;
        PlayerController.OnHeroMarked += SetSelectedHero;
    }

    private void OnDestroy()
    {
        Technique.SelectTechnique -= SetSelectedCombo;
        TurnsManager.OnPlayerTurnStart -= RollCooldowns;
        TurnsManager.OnPlayerTurnStart -= ResetAP;
        PlayerController.OnHeroMarked -= SetSelectedHero;
    }

    private void SetSelectedHero(Hero hero)
    {
        selectedHero = hero;
    }

    #region Combos

    private void SetSelectedCombo(Technique selected)
    {
        selectedTechnique = selected;
        UseTechnique();
    }

    public void RollCooldowns()
    {
        foreach (Technique technique in techniques)
        {
            technique.UpdateCooldown();
        }
    }

    [ContextMenu("Use technique")]
    public void UseTechnique()
    {
        if (selectedTechnique.IsReadyToUse())
        {
            if (selectedTechnique.GetAPCost() <= AP)
            {
                if (ContainsSymbols(selectedTechnique.GetRequirements()))
                {
                    UseSymbols(selectedTechnique.GetRequirements());
                    UseAP(selectedTechnique.GetAPCost());
                    OnTechniqueUsed.Invoke(selectedTechnique.GetTechEffects(), selectedHero);
                    selectedTechnique.StartCooldown();
                }
                else print($"not enough symbols {selectedTechnique.GetRequirements()} \n {symbolCharge}");
            }
            else print($"not enough AP {AP}/{selectedTechnique.GetAPCost()}");
        }
        else print("combo on cooldown");
    }

    #endregion

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
        return symbolCharge.Contains(toCheck);
    }

    public void UseSymbols(SymbolTable toUse)
    {
        symbolCharge.Remove(toUse);
    }
    #endregion

    #region AP

    //get maxAP as AP sum from party
    public void InitAP(int partySum)
    {
        maxAP = partySum;
        OnAPChanged?.Invoke(AP);
    }

    //Use to refill AP at the start of turn
    public void ResetAP()
    {
        AP = maxAP;
        OnAPChanged?.Invoke(AP);
    }

    public bool UseAP(int amount)
    {
        if (AP >= amount)
        {
            AP -= amount;
            OnAPChanged?.Invoke(AP);
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
