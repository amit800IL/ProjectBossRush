using System;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    public static event Action<int> OnAPChanged;
    public static event Action<Effect[], Hero> OnTechniqueUsed;

    [Header("AP count")]

    [SerializeField] private int maxAP;
    [SerializeField] private int AP;
    [SerializeField] private SymbolTable playerSymbolTable = new SymbolTable();

    [Header("Techniques")]

    [SerializeField] private Technique[] techniques;
    [SerializeField] private Technique selectedTechnique;
    [SerializeField] private Transform vfxInstiniatePosition;

    [Header("Other")]

    private Hero selectedHero;
    [SerializeField] private SymbolUI generalSymbolUI;
    [SerializeField] private SymbolUI heroSymbolUI;

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

        if (hero != null)
        {
            UpdateSymbolUI(hero);
        }
        else
        {
            heroSymbolUI.ResetUI();
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
                    if (vfxInstiniatePosition != null && selectedTechnique.TechData.Name == "Fireball")
                    {
                        GameObject instanitiedParticle = Instantiate(selectedTechnique.TechData.particleObject, vfxInstiniatePosition.position, Quaternion.identity);
                        Destroy(instanitiedParticle, 5f);
                    }

                    UseSymbols(selectedTechnique.GetRequirements());
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
        playerSymbolTable.PrintTable();
    }

    [ContextMenu("check contains symbols")]
    public bool ContainsSymbols()
    {
        return playerSymbolTable.Contains(playerSymbolTable);
    }

    public void UseSymbols(SymbolTable toUse)
    {
        playerSymbolTable.Remove(toUse);
        UpdateSymbolUI();
    }

    private void UpdateSymbolUI()
    {
        generalSymbolUI.UpdateUI(playerSymbolTable.ToShortString());
    }

    private void UpdateSymbolUI(Hero hero)
    {
        heroSymbolUI.UpdateUI(hero.SymbolTable.ToShortString());
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
        if (HasEnoughAP(amount))
        {
            AP -= amount;
            OnAPChanged?.Invoke(AP);
            return true;
        }
        return false;
    }

    public bool HasEnoughAP(int amount)
    {
        return AP >= amount;
    }

    public void ModifyAP(int amount)
    {
        maxAP = amount;
    }

    #endregion
}


