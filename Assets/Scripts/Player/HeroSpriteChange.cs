using UnityEngine;

public class HeroSpriteChange : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private SpriteRenderer heroSpriteRenderer;
    [SerializeField] private Material heroMaterial;
    [SerializeField] private Material lowHpMaterial;
    private int LowHPThreshold = 20;

    private void Start()
    {
        Hero.OnHeroHealthChanged += OnHpLow;
    }

    private void OnDisable()
    {
        Hero.OnHeroHealthChanged -= OnHpLow;
    }

    private void OnHpLow(int HeroHp)
    {
        if (HeroHp <= LowHPThreshold && heroSpriteRenderer != null)
        {
            heroSpriteRenderer.material = lowHpMaterial;
        }
        else
        {
            heroSpriteRenderer.material = heroMaterial;
        }
    }
}
