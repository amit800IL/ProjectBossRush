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
        PlayerController.OnHeroMarked += HeroMark;
    }

    private void OnDisable()
    {
        PlayerController.OnHeroMarked -= HeroMark;
    }

    public void OnHpLow(int HP)
    {
        if (HP <= LowHPThreshold && heroSpriteRenderer != null)
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
