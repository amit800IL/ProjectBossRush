using UnityEngine;
using UnityEngine.UI;

public class ReviveHeroButton : MonoBehaviour
{
    [SerializeField] private HeroUI heroUi;
    [SerializeField] private Button reviveButton;

    public void Revive()
    {
        if (heroUi.Hero != null && !heroUi.Hero.HeroIsAlive)
        {
            this.gameObject.SetActive(false);
            heroUi.Hero.gameObject.SetActive(true);
            heroUi.Hero.HeroSpawn();
            heroUi.Hero.RestartHeroOnRevive();
        }
    }
}
