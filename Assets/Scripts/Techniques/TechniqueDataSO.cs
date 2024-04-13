using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TechniqueData", menuName = "ScriptableObject/TechniqueDataScriptable")]
public class TechniqueDataSO : ScriptableObject
{
    public string Name;
    public bool RequiresTargetHero;
    public SymbolTable Requirements;
    public int APCost;
    public int Cooldown;
    public Effect[] Effects;
}
