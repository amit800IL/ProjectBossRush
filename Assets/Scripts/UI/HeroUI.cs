using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [field: SerializeField] public Hero Hero { get; private set; }
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private TextMeshProUGUI heatlhText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private TextMeshProUGUI defenceText;
    [SerializeField] private Image graphic;
    [SerializeField] private Image defenceImage;
    [SerializeField] private Image hpBar;
    //[SerializeField] private TextMeshProUGUI heroMovementAmountText;

    private bool isPanelActive = false;

    private void Start()
    {
        HeroHealthChange(Hero);
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
        }
        else
        {
            return false;
        }
    }

    private void HeroHealthChange(Hero h)
    {
        if (Hero == h && Hero != null)
        {
            hpBar.fillAmount = (float)Hero.HP / Hero.HeroData.maxHP;
            heatlhText.text = Hero.HP.ToString();
            maxHealthText.text = Hero.HeroData.maxHP.ToString();
        }
    }
    private void HeroDefenceChange(Hero h)
    {
        if (Hero == h && Hero != null && Hero.tempHP > 0)
        {
            defenceImage.gameObject.SetActive(true);
            defenceText.text = Hero.tempHP.ToString();
        }
        else if (Hero == h && Hero != null && Hero.tempHP <= 0)
        {
            defenceImage.gameObject.SetActive(false);
        }
    }

    //private void ShowHeroMovementAmount()
    //{
    //    if (Hero != null)
    //        heroMovementAmountText.text = Hero.HeroData.maxMovementAmount.ToString();
    //}
}
