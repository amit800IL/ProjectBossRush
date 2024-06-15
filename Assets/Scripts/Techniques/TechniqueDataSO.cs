using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TechniqueData", menuName = "ScriptableObject/TechniqueDataScriptable")]
public class TechniqueDataSO : ScriptableObject
{
    public string Name;
    public bool RequiresTargetHero;
    public GameObject particleObject;

    [Header("Requirments")]

    public SymbolTable Requirements;

    [Header("Ap cost")]

    public int APCost;

    [Header("Cooldown time")]

    public int Cooldown;

    [Header("Effect")]

    public Effect[] Effects;

 
}
