using UnityEngine;

public class MovePlayer : EnemyAction
{
    public override void DoActionOnHero(Hero hero)
    {
        MovePlayeInDirections(hero);
    }

    public void MovePlayeInDirections(Hero hero)
    {
        Tile[,] tiles = GridManager.Instance.Tiles;

        int randomRow = Random.Range(0, tiles.GetLength(0));
        int randomColumm = Random.Range(0, tiles.GetLength(1));

        if (tiles != null)
        {
            for (int i = 0; i < randomRow; i++)
            {
                for (int j = 0; j < randomColumm; j++)
                {
                    hero.MoveHeroToPosition(tiles[i,j]);

                    tiles[i, j].OccupyTile(hero);
                }
            }
        }
    }
}

