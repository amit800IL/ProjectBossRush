using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private HeroesManager heroesManager;

    private void Start()
    {
        Hero.OnHeroAttack += ActivatePlayerAttackSound;
        Hero.OnHeroInjured += ActivateHeroInjuredSound;
    }

    private void OnDestroy()
    {
        Hero.OnHeroAttack -= ActivatePlayerAttackSound;
        Hero.OnHeroInjured -= ActivateHeroInjuredSound;
    }

    public void ActivatePlayerAttackSound(Hero hero)
    {
        foreach (Hero listedHero in heroesManager.heroList)
        {
            if (listedHero != hero || hero.AttackingAudio == null) continue;

            hero.SoundAudio(hero.AttackingAudio);
        }
    }

    public void ActivateHeroInjuredSound(Hero hero)
    {
        foreach (Hero listedHero in heroesManager.heroList)
        {
            if (listedHero != hero) continue;

            hero.SoundAudio(hero.HurtingAudio);
        }
    }
}
