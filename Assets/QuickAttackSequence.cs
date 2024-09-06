using System.Collections;
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
            initialHero = heroesManager.heroList[i].GetComponentInParent<Ranger>();

            if (initialHero is Ranger)
            {
                break;
            }
        }

        if (initialHero != null)
        {
            Figher secondHero = null;

            for (int i = 0; i < heroesManager.heroList.Count; i++)
            {
                secondHero = heroesManager.heroList[i].GetComponentInParent<Figher>();

                if (secondHero is Figher)
                {
                    break;
                }
            }

            if (secondHero != null)
            {
                StartCoroutine(Electrify(secondHero));
            }
        }
    }

    private IEnumerator Electrify(Figher secondHero)
    {
        initialHero.attackVFX.SetVector3("Pos4", secondHero.transform.position);
        yield return initialHero.ActivateAttackVfx();
        yield return secondHero.QuickAttackSequence();
    }
}
