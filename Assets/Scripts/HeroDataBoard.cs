using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDataBoard : MonoBehaviour
{
    [SerializeField] private HeroUI heroUI;
    [SerializeField] private TextMeshProUGUI heatlhText;
    [SerializeField] private TextMeshProUGUI defenceText;

    private void Start()
    {
        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;

    }

    private void OnDisable()
    {
        Hero.OnHeroHealthChanged -= HeroHealthChange;
        Hero.OnHeroDefenceChanged -= HeroDefenceChange;
    }

    private void HeroHealthChange(Hero h)
    {
        if (heroUI.Hero == h)
        {
            heatlhText.text = "HP: " + heroUI.Hero.HP.ToString();
        }
    }

    private void HeroDefenceChange(Hero h)
    {
        if (heroUI.Hero == h)
        {
            defenceText.text = "Defence: " + heroUI.Hero.tempHP.ToString();
        }
    }

}
