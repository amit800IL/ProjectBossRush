using TMPro;
using UnityEngine;

public class HeroDataBoard : MonoBehaviour
{
    [SerializeField] private HeroUI heroUI;
    [SerializeField] private TextMeshProUGUI heatlhText;
    [SerializeField] private TextMeshProUGUI defenceText;
    [SerializeField] private TextMeshProUGUI heroMovementText;

    private void Start()
    {
        ShowHeroMovementAmount();
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


    private void ShowHeroMovementAmount()
    {
        if (heroUI.Hero != null)
            heroMovementText.text = heroUI.Hero.HeroData.maxMovementAmount.ToString();
    }


}
