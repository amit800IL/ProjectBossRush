using System.Collections.Generic;
using UnityEngine;

public class HeroSpriteChange : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private SpriteRenderer heroSpriteRenderer;
    [SerializeField] private Material heroMaterial;
    [SerializeField] private Material lowHpMaterial;
    [SerializeField] private Material markMaterial;
    private int LowHPThreshold = 20;
    private Material previousMaterial;

    private void Start()
    {
        PlayerController.OnHeroMarked += HeroMark;
        Hero.OnHeroHealthChanged += OnHpLow;
    }

    private void OnDisable()
    {
        PlayerController.OnHeroMarked -= HeroMark;
        Hero.OnHeroHealthChanged -= OnHpLow;
    }

    public void OnHpLow(Hero hero)
    {
        if (hero == this.hero && hero.HP <= LowHPThreshold && hero.HP > 0 && heroSpriteRenderer != null)
        {
            SetMaterial(lowHpMaterial);
        }
        else
        {
            SetMaterial(heroMaterial);
        }
    }

    private void HeroMark(Hero hero)
    {
        if (heroSpriteRenderer != null && this.hero == hero)
        {
            SetMaterial(markMaterial);
        }
        else
        {
            SetMaterial(heroMaterial);
        }

    }

    public void SetMaterial(Material material)
    {
        if (heroSpriteRenderer.material != material)
        {
            heroSpriteRenderer.material = material;
            previousMaterial = material;
        }
    }

    public void SetMaterialToPrevious()
    {
        heroSpriteRenderer.material = previousMaterial;
    }

    public void SetMaterialToNormal()
    {
        heroSpriteRenderer.material = heroMaterial;
    } 
}
