using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TechniqueData", menuName = "ScriptableObject/TechniqueDataScriptable")]
public class TechniqueDataSO : ScriptableObject
{
    public string Name;
    public SymbolTable Requirements;
    public Effect[] Effects;
}
