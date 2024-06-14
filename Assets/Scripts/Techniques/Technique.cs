using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Technique : MonoBehaviour
{
    public static event Action<Technique> SelectTechnique;
    public static event Action CooldownUpdated;

    [SerializeField] private TechniqueDataSO techData;
    [SerializeField] private int cooldown;

    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI nameText;
    //[SerializeField] private TextMeshProUGUI numText;
    //[SerializeField] private Image cardBG;
    [SerializeField] private Button activationButton;

    private void Start()
    {
        nameText.text = techData.Name;
        //numText.text = techData.Requirements.ToString();

        //needs to move to a manager
        PlayerController.OnHeroMarked += UpdateUsability;
        UpdateUsability(null);
    }

    private void OnDestroy()
    {
        PlayerController.OnHeroMarked -= UpdateUsability;
    }

    void UpdateUsability(Hero hero)
    {
        if (techData.RequiresTargetHero)
            activationButton.interactable = (hero != null);
    }

    public void Select()
    {
        SelectTechnique?.Invoke(this);
    }

    public SymbolTable GetRequirements()
    {
        return techData.Requirements;
    }

    public int GetAPCost()
    {
        return techData.APCost;
    }

    public Effect[] GetTechEffects()
    {
        return techData.Effects;
    }

    public void StartCooldown()
    {
        cooldown = techData.Cooldown;
        if (cooldown > 0)
        {

            //UpdateCooldownGraphic();
        }
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
