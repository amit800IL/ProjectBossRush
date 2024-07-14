using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private Image graphic;
    [SerializeField] private Image defenceImage;
    [SerializeField] private Hero hero;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI heroMovementAmountText;
    [SerializeField] private SymbolUI symbolUI;

    private bool isPanelActive = false;

    private void Start()
    {
        //PlayerController.OnHeroMarked += AssignHero;
        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;

    }

    private void OnDisable()
    {
        //PlayerController.OnHeroMarked -= AssignHero;
        Hero.OnHeroHealthChanged -= HeroHealthChange;
        Hero.OnHeroDefenceChanged -= HeroDefenceChange;
    }

    public bool AssignHero(Hero hero)
    {
        if (this.hero == null && hero != null)
        {
            this.hero = hero;
            graphic.sprite = hero.HeroData.headshotSprite;
            return true;
            //    ShowSelectedHeroOnUI();
        }
        else
        {
            return false;
            //    UndoHeroSelectionOnUI();
        }
    }

    private void ShowSelectedHeroOnUI()
    {
        ShowHeroMovementAmount();
        graphic.sprite = hero.HeroData.headshotSprite;
        HeroHealthChange(hero);
        HeroDefenceChange(hero);
    }

    //private void UndoHeroSelectionOnUI()
    //{
    //    heroMovementAmountText.text = "0";

    //    heroDefenceText.text = "0";

    //    hpBar.gameObject.SetActive(false);

    //    graphic.sprite = null;
    //}
    private void HeroHealthChange(Hero h)
    {
        if (hero == h)
        {
            hpBar.fillAmount = (float)hero.HP / hero.HeroData.maxHP;
            Debug.Log(hero.HP);
            Debug.Log(hero.HeroData.maxHP);
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
            if (hero.tempHP > 0)
            {
                defenceImage.gameObject.SetActive(true);
            }
            else
            {
                defenceImage.gameObject.SetActive(false);
            }
        }
    }
}
