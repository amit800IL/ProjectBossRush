using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private Image cardBG;

    private void Start()
    {
        nameText.text = techData.Name;
        numText.text = techData.Requirements.ToString();
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
        UpdateCooldownGraphic();
    }

    public bool IsReadyToUse() => cooldown == 0;

    //run at the start of every turn on all techniques
    public void UpdateCooldown()
    {
        if (cooldown > 0)
        {
            cooldown--;
            UpdateCooldownGraphic();
        }
    }

    private void UpdateCooldownGraphic()
    {
        cardBG.fillAmount = (techData.Cooldown - cooldown) / techData.Cooldown;
    }
}
