using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private Image graphic;
    [SerializeField] private Hero hero;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI heroDefenceText;
    [SerializeField] private TextMeshProUGUI heroMovementAmountText;

    private bool isPanelActive = false;

    public void AssignHero(Hero hero)
    {
        this.hero = hero;
        IntitiliazeHeroUI();
    }

    private void IntitiliazeHeroUI()
    {
        ShowHeroMovementAmount();
        graphic.sprite = hero.HeroData.headshotSprite;

        HeroHealthChange(hero);
        HeroDefenceChange(hero);

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

    private void HeroHealthChange(Hero h)
    {
        if (hero == h)
        {
            hpBar.fillAmount = (float)hero.HP / hero.HeroData.maxHP;

        }
    }

    private void ShowHeroMovementAmount()
    {
        heroMovementAmountText.text = hero.HeroData.maxMovementAmount.ToString();
    }

    private void HeroDefenceChange(Hero h)
    {
        if (hero == h)
        {
            heroDefenceText.text = hero.tempHP.ToString();

        }
    }
}
