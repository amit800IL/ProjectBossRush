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
    //[SerializeField] private TextMeshProUGUI numText;
    //[SerializeField] private Image cardBG;
    [SerializeField] private Button activationButton;

    private void Start()
    {
        nameText.text = TechData.Name;
        //numText.text = techData.Requirements.ToString();

        activationButton.interactable = false;

        //needs to move to a manager
        PlayerController.OnHeroMarked += UpdateUsability;
        TurnsManager.OnPlayerTurnStart += UpdateUsability;
        UpdateUsability(null);
    }

    private void OnDestroy()
    {
        PlayerController.OnHeroMarked -= UpdateUsability;
        TurnsManager.OnPlayerTurnStart -= UpdateUsability;
    }

    void UpdateUsability(Hero hero)
    {
        if (TechData.RequiresTargetHero)
            activationButton.interactable = (hero != null);
    }

    void UpdateUsability()
    {
        if (!TechData.RequiresTargetHero && IsReadyToUse())
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
        //if (cooldown > 0)
        //{

        //    UpdateCooldownGraphic();
        //}
    }

    public bool IsReadyToUse() => cooldown == 0;

    //run at the start of every turn on all techniques
    public void UpdateCooldown()
    {
        if (cooldown > 0)
        {
            cooldown--;
            //UpdateCooldownGraphic();
        }
    }

    //private void UpdateCooldownGraphic()
    //{
    //    cardBG.fillAmount = (techData.Cooldown - cooldown) / techData.Cooldown;
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    transform.localPosition = new(transform.localPosition.x, 270, transform.localPosition.z);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    transform.localPosition = new(transform.localPosition.x, 0, transform.localPosition.z);
    //}
}
