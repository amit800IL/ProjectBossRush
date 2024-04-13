using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private Hero hero;
    [SerializeField] private Image spritePresention;
    [SerializeField] private TextMeshProUGUI heroNameText;
    [SerializeField] private TextMeshProUGUI heroHPText;
    [SerializeField] private TextMeshProUGUI heroDefenceText;

    private bool isPanelActive = false;

    private void Start()
    {
        ShowHeroSprite();
        WriteHeroName();

        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;
    }

    private void OnDestroy()
    {
        Hero.OnHeroHealthChanged -= HeroHealthChange;
        Hero.OnHeroDefenceChanged -= HeroDefenceChange;
    }

    public void ShowOrHidePanel()
    {
        if (!isPanelActive)
        {
            heroPanel.SetActive(true);

            isPanelActive = true;
        }
        else
        {
            heroPanel.SetActive(false);

            isPanelActive = false;
        }
    }

    private void ShowHeroSprite()
    {
        spritePresention.sprite = hero.HeroData.heroGraphicLook;
    }
    private void WriteHeroName()
    {
        heroNameText.text = hero.HeroData.Name;
    }

    private void HeroHealthChange(int heroHealth)
    {
        heroHealth = hero.HeroData.HP;
        hero.HeroData.HP = heroHealth;

        heroHPText.text = "HP : " + heroHealth.ToString();
    }

    private void HeroDefenceChange(int heroDefence)
    {
        heroDefence = hero.HeroData.Defense;
        hero.HeroData.Defense = heroDefence;

        heroDefenceText.text = "Defence : " + heroDefence.ToString();
    }
}
