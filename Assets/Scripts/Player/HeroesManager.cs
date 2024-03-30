using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    [SerializeField] private PlayerResourceManager playerResourceManager; //maybe not needed
    private Hero[] heroList;
    [SerializeField] private Boss boss;

    private void Start()
    {
        //I know this line should not exist, its just for testing, it will change soon
        heroList = FindObjectsOfType<Hero>();
        PlayerResourceManager.OnTechniqueUsed += UseCombo;
    }

    private void OnDestroy()
    {
        PlayerResourceManager.OnTechniqueUsed -= UseCombo;
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
    public void UseCombo(Effect[] effects)
    {
        foreach (Effect effect in effects)
        {
            switch (effect.Type)
            {
                case EffectType.Damage:
                    boss.TakeDamage(effect.amount);
                    Debug.Log($"combo dealt {effect.amount} damage");
                    break;
                case EffectType.Heal:
                    break;
            }
        }
    }
}
