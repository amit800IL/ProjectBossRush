using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [field: SerializeField] public Hero Hero { get; private set; }
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private Image graphic;
    [SerializeField] private Image defenceImage;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image defenceBar;
    //[SerializeField] private TextMeshProUGUI heroMovementAmountText;

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
        if (this.Hero == null && hero != null)
        {
            this.Hero = hero;
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

    //private void UndoHeroSelectionOnUI()
    //{
    //    heroMovementAmountText.text = "0";

    //    heroDefenceText.text = "0";

    //    hpBar.gameObject.SetActive(false);

    //    graphic.sprite = null;
    //}
    private void HeroHealthChange(Hero h)
    {
        if (Hero == h)
        {
            hpBar.fillAmount = (float)Hero.HP / Hero.HeroData.maxHP;
        }
    }

    //private void ShowHeroMovementAmount()
    //{
    //    if (Hero != null)
    //        heroMovementAmountText.text = Hero.HeroData.maxMovementAmount.ToString();
    //}

    private void HeroDefenceChange(Hero h)
    {
        if (Hero == h)
        {
            defenceBar.fillAmount = (float)Hero.tempHP / Hero.HeroData.maxHP;
        }
    }
}
