using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/HeroData")]
public class HeroDataSO : ScriptableObject
{
    public string heroName;
    
    public int maxHP = 100;

    public int maxMovementAmount = 0;

    public int damage = 0;

    public int defense = 0;
}

