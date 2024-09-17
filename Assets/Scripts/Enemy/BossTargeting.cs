using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTargeting : MonoBehaviour
{
    public enum Target
    {
        AllHeroes,
        RandomHero,
        RandomColumn,
        RandomRow,
        Manual
    }

    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private DracovidMeteorScript dracovidMeteorScript;
    [SerializeField] private Vector2Int gridSize;
    private List<Hero> targetedHeroes = new();
    private List<Vector2Int> centers = new();

    public void Init()
    {
        //Debug.LogWarning("gridSize needs to be taken from GridManager");
        gridSize = GridManager.Instance.GetGridSize();
        dracovidMeteorScript.SetTargets(new Transform[3] {
            heroesManager.heroList[0].transform,
            heroesManager.heroList[1].transform,
            heroesManager.heroList[2].transform});
    }

    public List<Vector2Int> GetTargetTile(TargetInfo targetInfo)
    {
        centers.Clear();
        List<Vector2Int> toReturn = new();
        int size = targetInfo.Size;
        int rnd;
        if (size < 1) size = 1;
        if (size > gridSize.x) size = gridSize.x;

        switch (targetInfo.Target)
        {
            case Target.AllHeroes:
                int heroCount = heroesManager.heroList.Count;
                for (int i = 0; i < heroCount; i++)
                {
                    //toReturn.Add(heroesManager.heroList[i].CurrentTile.tilePosition);
                    toReturn.AddRange(GetTilesAround(heroesManager.heroList[i].CurrentTile.tilePosition, size - 1));
                }
                break;

            case Target.RandomHero:
                if (targetInfo.TargetHeroItself)
                {
                    foreach (Hero hero in targetedHeroes)
                    {
                        toReturn.AddRange(GetTilesAround(hero.CurrentTile.tilePosition, size - 1));
                    }
                }
                else
                {
                    toReturn.AddRange(GetTilesAround(heroesManager.GetRandomHero().CurrentTile.tilePosition, size - 1));
                }
                break;

            case Target.RandomColumn:
                rnd = Random.Range(0, gridSize.x - size + 1);
                for (int i = rnd; i < rnd + size; i++)
                {
                    for (int j = 0; j < gridSize.y; j++)
                    {
                        toReturn.Add(new(i, j));
                    }
                }
                break;

            case Target.RandomRow:
                rnd = Random.Range(0, gridSize.y - size + 1);
                for (int i = rnd; i < rnd + size; i++)
                {
                    for (int j = 0; j < gridSize.x; j++)
                    {
                        toReturn.Add(new(j, i));
                    }
                }
                break;

            case Target.Manual:
                break;

        }
        return toReturn;
    }

    private List<Vector2Int> GetTilesAround(Vector2Int origin, int radius)
    {
        centers.Add(origin);
        List<Vector2Int> toReturn = new();
        for (int i = origin.x - radius; i <= origin.x + radius; i++)
        {
            if (i < 0) continue;
            if (i >= gridSize.x) continue;
            for (int j = origin.y - radius; j <= origin.y + radius; j++)
            {
                if (j < 0) continue;
                if (j >= gridSize.y) continue;
                toReturn.Add(new(i, j));
            }
        }
        return toReturn;
    }

    public void MarkTargetedHeroes(BossActionSetter bossAction)
    {
        targetedHeroes = GetTargetHeroes(bossAction.Target);
        foreach (Hero hero in targetedHeroes)
        {
            hero.ApplyTargetMarker(bossAction.TargetMarker, bossAction.Target.Size);
            print(hero.name);
        }
    }

    public void UnMarkTargetedHeroes()
    {
        foreach (Hero hero in targetedHeroes)
        {
            hero.RemoveTargetMarker();
        }
    }

    public List<Hero> GetTargetHeroes(TargetInfo targetInfo)
    {
        List<Hero> toReturn = new();
        switch (targetInfo.Target)
        {
            case Target.AllHeroes:
                toReturn.AddRange(heroesManager.heroList);
                break;
            case Target.RandomHero:
                toReturn.Add(heroesManager.GetRandomHero());
                break;
            default:
                break;
        }

        return toReturn;
    }

    public List<Vector2Int> GetCenters()
    {
        return centers;
    }
}

[System.Serializable]
public struct TargetInfo
{
    public BossTargeting.Target Target;
    public bool TargetHeroItself;
    public int Amount;
    public int Size;
}
