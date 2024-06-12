using System;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static event Action<int> OnAPChanged;
    public static event Action<Effect[], Hero> OnTechniqueUsed;

    [Header("AP count")]

    [SerializeField] private int maxAP;
    [SerializeField] private int AP;

    [Header("Techniques")]

    [SerializeField] private Technique[] techniques;
    [SerializeField] private Technique selectedTechnique;

    [Header("Other")]

    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private SymbolUI symbolUI;

    private void Start()
    {
        Hero.OnHeroSymbolChanged += UpdateSymbolUI;
        Technique.SelectTechnique += SetSelectedCombo;
        TurnsManager.OnPlayerTurnStart += RollCooldowns;
        TurnsManager.OnPlayerTurnStart += ResetAP;
        PlayerController.OnHeroMarked += SetSelectedHero;

        foreach (Hero hero in heroesManager.heroList)
        {
            UpdateSymbolUI(hero);
        }
    }

    private void OnDestroy()
    {
        Hero.OnHeroSymbolChanged -= UpdateSymbolUI;
        Technique.SelectTechnique -= SetSelectedCombo;
        TurnsManager.OnPlayerTurnStart -= RollCooldowns;
        TurnsManager.OnPlayerTurnStart -= ResetAP;
        PlayerController.OnHeroMarked -= SetSelectedHero;
    }

    private void SetSelectedHero(Hero hero)
    {
        if (hero == null) return;

        UpdateSymbolUI(hero);
    }

    #region Combos

    private void SetSelectedCombo(Technique selected)
    {
        selectedTechnique = selected;

        foreach (Hero hero in heroesManager.heroList)
        {
            UseTechnique(hero);
        }
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
    public void UseTechnique(Hero hero)
    {
        if (hero == null) return;

        if (selectedTechnique.IsReadyToUse())
        {
            if (selectedTechnique.GetAPCost() <= AP)
            {
                if (hero.SymbolTable.Contains(selectedTechnique.GetRequirements()))
                {
                    UseSymbols(hero);
                    UseAP(selectedTechnique.GetAPCost());
                    OnTechniqueUsed.Invoke(selectedTechnique.GetTechEffects(), hero);
                    selectedTechnique.StartCooldown();
                    UpdateSymbolUI(hero);
                }
                else print($"not enough symbols {selectedTechnique.GetRequirements()} \n {hero.SymbolTable}");
            }
            else print($"not enough AP {AP}/{selectedTechnique.GetAPCost()}");
        }
        else print("combo on cooldown");
    }

    #endregion

    #region symbols
    public void AddSymbols(Hero hero, SymbolTable toAdd)
    {
        if (hero == null) return;

        hero.SymbolTable.Add(toAdd);
        UpdateSymbolUI(hero);

        Debug.Log("Symbol added for" + hero);
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
    public bool ContainsSymbols(Hero hero)
    {
        return hero.SymbolTable.Contains(hero.SymbolTable);
    }

    public void UseSymbols(Hero hero)
    {
        hero.SymbolTable.Remove(hero.SymbolTable);
        UpdateSymbolUI(hero);
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
