using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Technique : MonoBehaviour
{
    public static event Action<Technique> SelectTechnique;
    [SerializeField] private TechniqueDataSO techData;

    [Header("Debug")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI numText;

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

    public Effect[] GetTechData()
    {
        return techData.Effects;
    }
}
