using UnityEngine;

public class HeroSpriteChange : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private SpriteRenderer heroSpriteRenderer;
    [SerializeField] private Material heroMaterial;
    [SerializeField] private Material lowHpMaterial;
    private int LowHPThreshold = 20;
    private Material previousMaterial;

    //private void Start()
    //{
    //    Hero.OnHeroHealthChanged += OnHpLow;
    //    Hero.OnHeroInjured += OnHpLow;
    //    SetMaterial(heroMaterial);
    //}

    //private void OnDisable()
    //{
    //    Hero.OnHeroHealthChanged -= OnHpLow;
    //    Hero.OnHeroInjured -= OnHpLow;
    //}

    //public void OnHpLow(Hero hero)
    //{
    //    if (hero == this.hero && hero.HP <= LowHPThreshold && hero.HP > 0 && heroSpriteRenderer != null)
    //    {
    //        SetMaterial(lowHpMaterial);
    //    }
    //    else
    //    {
    //        SetMaterial(heroMaterial);
    //    }
    //}

    //public void SetMaterial(Material material)
    //{
    //    if (heroSpriteRenderer.material != material)
    //    {
    //        heroSpriteRenderer.material = material;
    //        previousMaterial = material;
    //    }
    //}

    public void SetMaterialToPrevious()
    {
        //heroSpriteRenderer.material = previousMaterial;

        //OnHpLow(hero);
    }

    public void SetMaterialToNormal()
    {
        //heroSpriteRenderer.material = heroMaterial;
    }
}
