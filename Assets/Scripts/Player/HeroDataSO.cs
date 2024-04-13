using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/HeroData")]
public class HeroDataSO : ScriptableObject
{
    public Sprite heroSpritePresentation;

    public string heroName;
    
    public int maxHP = 100;

    public int HP = 0;

    public int damage = 0;

    public int Defense = 0;
}

