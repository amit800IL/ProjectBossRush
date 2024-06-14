
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//this class should become not monobehavior, and be used as a data structure, to be used in a struct with other values
public class AttackPlayer : EnemyAction
{
    [SerializeField] private Boss boss;
    public override void DoActionOnHero(Hero hero)
    {
        AttackHero(hero);
        hero.SlashParticle.Play();
    }
    public void AttackHero(Hero hero)
    {
        hero.TakeDamage((int)boss.Damage);
    }

    public override void DoActionOnTiles(List<Vector2Int> tiles, int actionPower)
    {
        Tile[,] grid = GridManager.Instance.Tiles;
        Tile tile;
        foreach (Vector2Int tilePosition in tiles)
        {
            tile = grid[tilePosition.x, tilePosition.y];
            if (tile != null)
            {
                if (tile.IsTileOccupied)
                {
                    Hero hero = (Hero)tile.GetOccupier();
                    Debug.Log("Found hero: " + hero.name + " on a tile");
                    hero.TakeDamage(actionPower);
                    hero.SlashParticle.Play();

                }
            }
        }

    }
}
