using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Technique : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<Technique> SelectTechnique;
    public static event Action CooldownUpdated;

    [field: SerializeField] public TechniqueDataSO TechData { get; private set; }
    [SerializeField] private int cooldown;

    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI nameText;
    //[SerializeField] private Image cardBG;
    [SerializeField] private Button activationButton;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private TextMeshProUGUI numText;

    private void Start()
    {
        nameText.text = TechData.Name;
        numText.text = $"AP {TechData.APCost}\n";
        numText.text += TechData.Requirements.ToString();

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.SetActive(true);
        //transform.localPosition = new(transform.localPosition.x, 270, transform.localPosition.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
        //transform.localPosition = new(transform.localPosition.x, 0, transform.localPosition.z);
    }
}
