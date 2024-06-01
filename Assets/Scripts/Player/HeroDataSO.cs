using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/HeroData")]
public class HeroDataSO : ScriptableObject
{
    public string heroName;
    public Sprite headshotSprite;

    [Header("HP")]
    
    public int maxHP = 100;

    [Header("Movement Amount")]

    public int maxMovementAmount = 0;

    [Header("Damage")]

    public int damage = 0;

    [Header("Damage")]

    public int defense = 0;
}

