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
        PlayerController.OnHeroMarked += UndoHeroSelectionOnUI;
    }

    private void OnDisable()
    {
        PlayerController.OnHeroMarked -= UndoHeroSelectionOnUI;
    }

    public void AssignHero(Hero hero)
    {
        if (hero != null)
        {
            this.hero = hero;
            IntitiliazeHeroUI();
        }
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

    private void UndoHeroSelectionOnUI(Hero hero)
    {
        heroMovementAmountText.text = "0";

        heroDefenceText.text = "0";

        graphic.sprite = null;

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
