using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTargeting : MonoBehaviour
{
    public enum Target
    {
        AllHeroes,
        RandomHero,
        Random3Column,
        Manual
    }

    [SerializeField] private HeroesManager heroesManager;

    public List<Vector2Int> GetTargetTile(Target target)
    {
        List<Vector2Int> toReturn = new();
        switch (target)
        {
            case Target.AllHeroes:
                int heroCount = heroesManager.heroList.Count;
                for (int i = 0; i < heroCount; i++)
                {
                    toReturn.Add(heroesManager.heroList[i].CurrentTile.tilePosition);
                }
                break;

            case Target.RandomHero:
                toReturn.Add(heroesManager.GetRandomHero().CurrentTile.tilePosition);
                break;

            case Target.Random3Column:
                int rnd = Random.Range(0, 4);
                for (int i = rnd; i < rnd + 3; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        toReturn.Add(new(i, j));
                    }
                }
                break;

            case Target.Manual:
                break;

        }
        return toReturn;
    }
}
