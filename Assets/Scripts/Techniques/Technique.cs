using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Technique : MonoBehaviour
{
    public static event Action<Technique> SelectTechnique;
    public static event Action CooldownUpdated;

    [field: SerializeField] public TechniqueDataSO TechData { get; private set; }
    [SerializeField] private int cooldown;

    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI nameText;
    //[SerializeField] private Image cardBG;
    [SerializeField] private Button activationButton;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private TextMeshProUGUI coolDownText;

    private void Start()
    {
        nameText.text = TechData.Name;
        numText.text = $"AP {TechData.APCost}\n";
        numText.text += TechData.Requirements.ToString();

        activationButton.interactable = false;

        //needs to move to a manager
        PlayerController.OnHeroMarked += UpdateUsability;
        TurnsManager.OnPlayerTurnStart += UpdateUsability;
        TurnsManager.OnPlayerTurnStart += ShowCooldown;
        UpdateUsability(null);
        UpdateCooldownOnStart();
        UpdateUsability();
        ShowCooldown();
    }

    private void OnDestroy()
    {
        PlayerController.OnHeroMarked -= UpdateUsability;
        TurnsManager.OnPlayerTurnStart -= UpdateUsability;
        TurnsManager.OnPlayerTurnStart -= ShowCooldown;
    }

    private void ShowCooldown()
    {
        coolDownText.text = cooldown.ToString();
    }

    void UpdateUsability(Hero hero)
    {
        if (TechData.RequiresTargetHero)
            activationButton.interactable = (hero != null);
    }

    void UpdateUsability()
    {
        if (!TechData.RequiresTargetHero && cooldown == 0)
        {
            activationButton.interactable = true;
        }
        else
        {
            activationButton.interactable = false;
        }
    }

    public void Select()
    {
        SelectTechnique?.Invoke(this);
    }

    public SymbolTable GetRequirements()
    {
        return TechData.Requirements;
    }

    public int GetAPCost()
    {
        return TechData.APCost;
    }

    public Effect[] GetTechEffects()
    {
        return TechData.Effects;
    }

    public void StartCooldown()
    {
        cooldown = TechData.Cooldown;
    }

    public bool IsReadyToUse() => cooldown == 0;

    //run at the start of every turn on all techniques

    public void UpdateCooldownOnStart()
    {
        cooldown = TechData.Cooldown;
    }
    public void UpdateCooldown()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
        else
        {
            cooldown = TechData.Cooldown;
        }
    }
}
