using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveHeroButton : MonoBehaviour
{
    private Hero hero;
    [SerializeField] private GameObject reviveButtonPanel;
    [SerializeField] private Button reviveButton;
    [SerializeField] private TextMeshProUGUI heroNameText;

    //private void Start()
    //{
    //    ActivateButton();
    //}

    private void OnEnable()
    {
        ActivateButton();
    }

    private void ActivateButton()
    {
        if (!this.hero.HeroIsAlive)
        {
            reviveButton.interactable = true;
        }
    }

    public bool AssignHero(Hero hero)
    {
        if (this.hero == null && hero != null)
        {
            this.hero = hero;
            reviveButton.interactable = false;
            heroNameText.text = hero.HeroData.heroName;
            return true;
        }

        return false;
    }

    public void Revive()
    {
        if (hero != null && !hero.HeroIsAlive)
        {
            reviveButton.interactable = false;
            reviveButtonPanel.SetActive(false);
            hero.gameObject.SetActive(true);
            hero.HeroSpawn();
            hero.RestartHeroOnRevive();
        }
    }
}
