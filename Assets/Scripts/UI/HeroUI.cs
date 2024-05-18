using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private Hero hero;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI heroDefenceText;
    [SerializeField] private TextMeshProUGUI heroMovementAmountText;

    private bool isPanelActive = false;

    private void Start()
    {
        IntitiliazeHeroUI();
    }

    private void IntitiliazeHeroUI()
    {
        ShowHeroMovementAmount();

        HeroHealthChange(hero.HP);
        HeroDefenceChange(hero.HeroData.defense);

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

    private void HeroHealthChange(int heroHealth)
    {
        hpBar.fillAmount = (float)heroHealth / hero.HeroData.maxHP;
    }

    private void ShowHeroMovementAmount()
    {
        heroMovementAmountText.text = hero.HeroData.maxMovementAmount.ToString();
    }

    private void HeroDefenceChange(int heroDefence)
    {
        heroDefenceText.text = heroDefence.ToString();
    }
}
