using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    [SerializeField] private List<Hero> heroList;
    [SerializeField] private Boss boss;
    public void AttackBoss()
    {
        foreach (Hero hero in heroList)
        {
            hero.HeroAttackBoss(boss);
        }
    }
}
