using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [field: SerializeField] public Hero Hero { get; private set; }
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
        HeroHealthChange(Hero);
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
        if (this.Hero == null && hero != null)
        {
            this.Hero = hero;
            graphic.sprite = hero.HeroData.headshotSprite;
            ShowHeroSymbolUI(hero);
            ShowHeroMovementAmount();
            return true;
        }

        return false;
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

    private void ShowHeroSymbolUI(Hero h)
    {
        if (h == Hero)
            symbolUI.UpdateUI(h.SymbolTable.ToShortString());
    }

    private void ShowHeroMovementAmount()
    {
        if (Hero != null)
            heroMovementText.text = "Movement: " + Hero.HeroData.maxMovementAmount.ToString();
    }
}
