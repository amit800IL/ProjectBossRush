using UnityEngine;

public class HeroSpriteChange : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private SpriteRenderer heroSpriteRenderer;
    [SerializeField] private Material heroMaterial;
    [SerializeField] private Material lowHpMaterial;

    private void Start()
    {
        Hero.OnHeroHealthChanged += OnHpLow;
    }

    private void OnHpLow(int lowHp)
    {
        lowHp = 20;

        if (hero.HP <= lowHp && heroSpriteRenderer != null)
        {
            heroSpriteRenderer.material = lowHpMaterial;
        }
        else
        {
            heroSpriteRenderer.material = heroMaterial;
        }
    }
}
