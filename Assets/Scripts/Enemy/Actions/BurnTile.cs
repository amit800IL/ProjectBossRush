using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTile : EnemyAction
{
    [SerializeField] private Effect burn;
    [SerializeField] private ParticleSystem fireWall;
    public override void DoActionOnHero(Hero hero)
    {
        throw new System.NotImplementedException();
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
                tile.AddTileEffect(burn);

                if (tile.IsTileOccupied)
                {
                    Hero hero = (Hero)tile.GetOccupier();
                    hero.TakeDamage(actionPower);

                    hero.SlashParticle.Play();

                    fireWall.transform.position = tile.OccupantContainer.transform.position;

                    fireWall.Play();
                }
            }
        }

    }
}
