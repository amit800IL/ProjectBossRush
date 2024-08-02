using UnityEngine;

public class HeroSpriteChange : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private SpriteRenderer heroSpriteRenderer;
    [SerializeField] private Material heroMaterial;
    [SerializeField] private Material lowHpMaterial;
    [SerializeField] private Material markMaterial;
    private int LowHPThreshold = 20;

    private void Start()
    {
        Hero.OnHeroAttack += HeroAttack;
        Hero.OnHeroDefend += HeroDefend;
        PlayerController.OnHeroMarked += HeroMark;
        Hero.OnHeroHealthChanged += OnHpLow;
    }

    private void OnDisable()
    {
        Hero.OnHeroAttack -= HeroAttack;
        Hero.OnHeroDefend -= HeroDefend;
        PlayerController.OnHeroMarked -= HeroMark;
        Hero.OnHeroHealthChanged -= OnHpLow;
    }

    private void HeroAttack(Hero hero)
    {
        TurnOffShader(hero);
    }

    private void HeroDefend(Hero hero)
    {
        TurnOffShader(hero);
    }

    private void TurnOffShader(Hero hero)
    {
        if (heroSpriteRenderer != null && this.hero == hero)
        {
            heroSpriteRenderer.material = heroMaterial;
        }
    }

    public void OnHpLow(Hero hero)
    {
        if (hero == this.hero && hero.HP <= LowHPThreshold && hero.HP > 0 && heroSpriteRenderer != null)
        {
            heroSpriteRenderer.material = lowHpMaterial;
        }
        else
        {
            heroSpriteRenderer.material = heroMaterial;
        }
    }

    private void HeroMark(Hero hero)
    {
        if (heroSpriteRenderer != null && this.hero == hero)
        {
            heroSpriteRenderer.material = markMaterial;
        }
        else
        {
            heroSpriteRenderer.material = heroMaterial;
        }

    }
}
