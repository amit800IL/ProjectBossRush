using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private SymbolUI symbolUI;
    [SerializeField] private TextMeshProUGUI heatlhText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private TextMeshProUGUI defenceText;
    [SerializeField] private TextMeshProUGUI heroMovementText;
    [SerializeField] private Image graphic;
    [SerializeField] private Image defenceImage;
    [SerializeField] private Image hpBar;

    private bool isPanelActive = false;

    private void Start()
    {
        HeroHealthChange(hero);
        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;
    }

    private void OnDisable()
    {
        Hero.OnHeroHealthChanged -= HeroHealthChange;
        Hero.OnHeroDefenceChanged -= HeroDefenceChange;
    }

    public bool AssignHero(Hero hero)
    {
        if (this.hero == null && hero != null)
        {
            this.hero = hero;
            graphic.sprite = hero.HeroData.headshotSprite;
            ShowHeroSymbolUI(hero);
            ShowHeroMovementAmount();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HeroHealthChange(Hero h)
    {
        if (hero == h && hero != null)
        {
            hpBar.fillAmount = (float)hero.HP / hero.HeroData.maxHP;
            heatlhText.text = hero.HP.ToString();
            maxHealthText.text = hero.HeroData.maxHP.ToString();
        }
    }
    private void HeroDefenceChange(Hero h)
    {
        if (hero == h && hero != null && hero.tempHP > 0)
        {
            defenceImage.gameObject.SetActive(true);
            defenceText.text = hero.tempHP.ToString();
        }
        else if (hero == h && hero != null && hero.tempHP <= 0)
        {
            defenceImage.gameObject.SetActive(false);
        }
    }

    private void ShowHeroSymbolUI(Hero h)
    {
        if (h == hero)
            symbolUI.UpdateUI(h.SymbolTable.ToShortString());
    }

    private void ShowHeroMovementAmount()
    {
        if (hero != null)
            heroMovementText.text = "Movement: " + hero.HeroData.maxMovementAmount.ToString();
    }
}
