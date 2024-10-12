using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Technique : MonoBehaviour
{
    public static event Action<Technique> SelectTechnique;
    public static event Action CooldownUpdated;

    public bool HasComboBeenUsed { get; set; } = false;

    [field: SerializeField] public TechniqueDataSO TechData { get; private set; }
    [SerializeField] private int cooldown;

    [Header("Debug")]
    [SerializeField] private HeroesManager heroesManager;
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

        activationButton.interactable = true;

        //needs to move to a manager
        PlayerController.OnHeroMarked += UpdateUsability;
        TurnsManager.OnPlayerTurnStart += ShowCooldown;
        TurnsManager.OnPlayerTurnStart += UpdateUsability;
        Hero.OnHeroDeath += UpdateQuickAttackUsability;
        //SelectTechnique += UpdateUsability;
        //SelectTechnique += ShowCooldown;

        //UpdateUsability();
    }

    private void OnDestroy()
    {
        PlayerController.OnHeroMarked -= UpdateUsability;
        TurnsManager.OnPlayerTurnStart -= ShowCooldown;
        TurnsManager.OnPlayerTurnStart -= UpdateUsability;
        Hero.OnHeroDeath -= UpdateQuickAttackUsability;
        //SelectTechnique -= UpdateUsability;
        //SelectTechnique -= ShowCooldown;
    }

    private void ShowCooldown()
    {
        coolDownText.text = cooldown != 0 ? cooldown.ToString() : " ";
    }

    private void UpdateUsability()
    {
        if (TechData.Name == "Revive" || TechData.Name == "QuickAttack")
        {
            if (heroesManager.heroList.All(hero => hero.HeroIsAlive))
            {
                activationButton.interactable = false;
                return;
            }
        }

        if (cooldown < 1 && !TechData.RequiresTargetHero)
        {
            activationButton.interactable = true;
        }
        else
        {
            activationButton.interactable = false;
        }

    }

    private void UpdateQuickAttackUsability(Hero hero)
    {
        if (hero == null) return;

        if (TechData.name == "QuickAttack" && ((hero is Figher && !hero.HeroIsAlive) || (hero is Ranger && !hero.HeroIsAlive)))
        {
            activationButton.interactable = false;
        }

        if (TechData.Name == "Revive" && !hero.HeroIsAlive)
        {
            activationButton.interactable = true;
        }
    }

    private void UpdateUsability(Hero hero)
    {
        if (TechData.RequiresTargetHero && cooldown < 1)
        {
            if (hero == null || TechData.Name == "Heal" && hero.HP >= hero.HeroData.maxHP)
            {
                activationButton.interactable = false;
                return;
            }
            else
            {
                activationButton.interactable = true;
            }
        }
    }


    private void UpdateUsability(Technique technique)
    {
        if (technique.cooldown <= 1 && !technique.TechData.RequiresTargetHero)
        {
            technique.activationButton.interactable = true;
        }
        else
        {
            technique.activationButton.interactable = false;
        }
    }

    private void ShowCooldown(Technique technique)
    {
        technique.UpdateCooldown();

        technique.coolDownText.text = technique.cooldown.ToString();

        if (technique.cooldown <= 1 && technique.activationButton.interactable == true)
        {
            technique.coolDownText.text = " ";
        }

        if (technique.TechData.RequiresTargetHero && technique.cooldown <= 1)
        {
            technique.coolDownText.text = " ";
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
        ShowCooldown();
    }

    public bool IsReadyToUse() => cooldown < 1;

    //run at the start of every turn on all techniques

    //public void UpdateCooldownOnStart()
    //{
    //    cooldown = TechData.Cooldown;
    //}
    public void UpdateCooldown()
    {
        if (cooldown > 0)
        {
            cooldown--;
        }
    }
}
