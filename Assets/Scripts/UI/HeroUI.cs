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
    [SerializeField] private TextMeshProUGUI heroMovementAmountText;

    private bool isPanelActive = false;

    private void Start()
    {
        IntitiliazeHeroUI();
    }

    private void IntitiliazeHeroUI()
    {
        ShowHeroSprite();
        WriteHeroName();
        ShowHeroMovementAmount();

        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;

        HeroHealthChange(hero.HeroData.maxHP);
        HeroDefenceChange(hero.HeroData.defense);
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
        spritePresention.sprite = hero.HeroData.heroSpritePresentation;
    }
    private void WriteHeroName()
    {
        heroNameText.text = hero.HeroData.heroName;
    }

    private void HeroHealthChange(int heroHealth)
    {
        heroHPText.text = "HP: " + heroHealth.ToString();
    }

    private void ShowHeroMovementAmount()
    {
        heroMovementAmountText.text = "Movement: " + hero.HeroData.maxMovementAmount.ToString();
    }

    private void HeroDefenceChange(int heroDefence)
    {
        heroDefenceText.text = "Defence: " + heroDefence.ToString();
    }
}
