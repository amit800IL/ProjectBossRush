using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    [SerializeField] private PlayerResourceManager playerResourceManager; //maybe not needed
    private Hero[] heroList;
    [SerializeField] private Boss boss;

    private void Start()
    {
        PlayerResourceManager.OnTechniqueUsed += ActivateComboEffects;
    }

    private void OnDestroy()
    {
        PlayerResourceManager.OnTechniqueUsed -= ActivateComboEffects;
    }

    public void AttackBoss()
    {
        foreach (Hero hero in heroList)
        {
            hero.HeroAttackBoss(boss);
            hero.heroAnimator.SetTrigger("Attack");
        }
    }

    [ContextMenu("Combo")]
    public void ActivateComboEffects(Effect[] effects, Hero selectedHero)
    {
        foreach (Effect effect in effects)
        {
            switch (effect.Type)
            {
                case EffectType.DamageBoss:
                    boss.TakeDamage(effect.amount);
                    Debug.Log($"combo dealt {effect.amount} damage");
                    break;
                case EffectType.HealAll:
                    foreach (Hero hero in heroList)
                    {
                        hero.TakeDamage(-effect.amount);
                    }
                    break;
                case EffectType.BuffDefense1:
                    
                    break;
            }
        }
    }
}
