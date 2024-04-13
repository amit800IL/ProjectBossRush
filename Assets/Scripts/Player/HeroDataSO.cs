using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/HeroData")]
public class HeroDataSO : ScriptableObject
{
    public Sprite heroGraphicLook;

    public HeroesNames Name;

    public int HP = 0;

    public int damage = 0;

    public int Defense = 0;
}

public enum HeroesNames
{
    Berzeker,
    Mage,
    Ranger,
}
