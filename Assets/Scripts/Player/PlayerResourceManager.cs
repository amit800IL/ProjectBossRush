using System;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static event Action<int> OnAPChanged;
    public static event Action<Effect[], Hero> OnTechniqueUsed;

    [Header("AP count")]

    [SerializeField] private int maxAP;
    [SerializeField] private int AP;
    private SymbolTable playerSymbolTable = new();

    [Header("Techniques")]

    [SerializeField] private Technique[] techniques;
    [SerializeField] private Technique selectedTechnique;

    [Header("Other")]

    private Hero selectedHero;
    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private SymbolUI symbolUI;

    private void Start()
    {
        Technique.SelectTechnique += SetSelectedCombo;
        TurnsManager.OnPlayerTurnStart += RollCooldowns;
        TurnsManager.OnPlayerTurnStart += ResetAP;
        PlayerController.OnHeroMarked += SetSelectedHero;

        foreach (Hero hero in heroesManager.heroList)
        {
            UpdateSymbolUI(hero);
        }

        UpdateSymbolUI();
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

        if (hero != null)
        {
            UpdateSymbolUI(hero);
        }
        else
        {
            UpdateSymbolUI();
        }
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
            if (technique != null)
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
                if (playerSymbolTable.Contains(selectedTechnique.GetRequirements()))
                {
                    UseAP(selectedTechnique.GetAPCost());
                    OnTechniqueUsed.Invoke(selectedTechnique.GetTechEffects(), selectedHero);
                    selectedTechnique.StartCooldown();
                    UpdateSymbolUI();
                }
                else Debug.Log($"not enough symbols {selectedTechnique.GetRequirements()} \n {playerSymbolTable}");
            }
            else Debug.Log($"not enough AP {AP}/{selectedTechnique.GetAPCost()}");
        }
        else Debug.Log("combo on cooldown");
    }

    #endregion

    #region symbols
    public void AddSymbols(SymbolTable toAdd)
    {
        playerSymbolTable.Add(toAdd);
        UpdateSymbolUI();
    }

    //[ContextMenu("add test table")]
    //public void AddTestTable()
    //{
    //    AddSymbols(testTable);
    //}

    [ContextMenu("print table")]
    public void PrintSymbols()
    {
        foreach (Hero hero in heroesManager.heroList)
        {
            hero.SymbolTable.PrintTable();
        }
    }

    [ContextMenu("check contains symbols")]
    public bool ContainsSymbols()
    {
        return playerSymbolTable.Contains(playerSymbolTable);
    }

    public void UseSymbols()
    {
        playerSymbolTable.Remove(playerSymbolTable);
        UpdateSymbolUI();
    }

    private void UpdateSymbolUI()
    {
        symbolUI.UpdateUI(playerSymbolTable.ToShortString());
    }

    private void UpdateSymbolUI(Hero hero)
    {
        symbolUI.UpdateUI(hero.SymbolTable.ToShortString());
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
