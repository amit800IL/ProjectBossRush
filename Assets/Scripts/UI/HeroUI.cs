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
    [SerializeField] private SymbolUI symbolUI;

    private bool isPanelActive = false;

    private void Start()
    {
        PlayerController.OnHeroMarked += AssignHero;
        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;
    }

    private void OnDisable()
    {
        PlayerController.OnHeroMarked -= AssignHero;
        Hero.OnHeroHealthChanged -= HeroHealthChange;
        Hero.OnHeroDefenceChanged -= HeroDefenceChange;
    }

    public void AssignHero(Hero hero)
    {
        if (hero != null)
        {
            this.hero = hero;
            ShowSelectedHeroOnUI();
        }
        else
        {
            UndoHeroSelectionOnUI();
        }
    }

    private void ShowSelectedHeroOnUI()
    {
        ShowHeroMovementAmount();
        graphic.sprite = hero.HeroData.headshotSprite;

        hpBar.gameObject.SetActive(true);

        HeroHealthChange(hero);
        HeroDefenceChange(hero);
    }

    private void UndoHeroSelectionOnUI()
    {
        heroMovementAmountText.text = "0";

        heroDefenceText.text = "0";

        hpBar.gameObject.SetActive(false);

        graphic.sprite = null;

        HeroHealthChange(hero);
        HeroDefenceChange(hero);
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
        if (hero != null)
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
