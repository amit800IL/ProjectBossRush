using UnityEngine;

public class QuickAttackSequence : MonoBehaviour
{
    [SerializeField] private HeroesManager heroesManager;
    private Hero initialHero;

    [ContextMenu("Trigger Sequence")]
    public void TriggerSequence()
    {
        for (int i = 0; i < heroesManager.heroList.Count; i++)
        {
            initialHero = heroesManager.heroList[i].GetComponentInParent<Mage>();

            if (initialHero is Mage)
            {
                break;
            }
        }

        if (initialHero != null)
        {
            MageProjectile projectile = initialHero.GetComponentInChildren<MageProjectile>();

            Hero secondHero = null;

            for (int i = 0; i < heroesManager.heroList.Count; i++)
            {
                secondHero = heroesManager.heroList[i].GetComponentInParent<Figher>();

                if (secondHero is Figher)
                {
                    break;
                }
            }

            if (projectile != null && secondHero != null)
            {
                projectile.MoveProjectile(secondHero.transform.position);
            }
        }
    }
}
