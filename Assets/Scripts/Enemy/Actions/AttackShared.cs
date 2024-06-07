using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShared : EnemyAction
{
    public override void DoActionOnHero(Hero hero)
    {
        throw new System.NotImplementedException();
    }

    public override void DoActionOnTiles(List<Vector2Int> tiles, int actionPower)
    {
        Tile[,] grid = GridManager.Instance.Tiles;
        Tile tile;
        int hitTargets = 0;
        foreach (Vector2Int tilePosition in tiles)
        {
            tile = grid[tilePosition.x, tilePosition.y];
            if (tile != null)
            {
                if (tile.IsTileOccupied)
                {
                    hitTargets++;
                }
            }
        }
        if (hitTargets == 0) //no targets hit
        {
            Debug.Log("No Targets hit, this shouldnt happen");
            return;
        }

        foreach (Vector2Int tilePosition in tiles)
        {
            tile = grid[tilePosition.x, tilePosition.y];
            if (tile != null)
            {
                if (tile.IsTileOccupied)
                {
                    Hero hero = (Hero)tile.GetOccupier();
                    Debug.Log("Found hero: " + hero.name + " on a tile");
                    hero.TakeDamage(actionPower / hitTargets);
                    hero.SlashParticle.Play();

                }
            }
        }

    }
}
