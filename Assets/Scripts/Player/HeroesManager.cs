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
            switch (hero)
            {
                case Figher:
                    if (hero.CanHeroAttack())
                    {
                        Debug.Log(hero.gameObject.name + "Has fought boss");
                        boss.TakeDamage(hero.Damage);
                    }
                    else
                        Debug.Log("Hero not in position");
                    break;
            }
        }
    }
}
