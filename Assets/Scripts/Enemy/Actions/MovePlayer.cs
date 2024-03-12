using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : EnemyAction
{
    [SerializeField] private LayerMask tileMask;
    [SerializeField] private LayerMask heroMask;
    [SerializeField] private List<Vector2> movePlayerDirections;

    public void MovePlayeInDirections(Hero hero)
    {
        Collider2D tile = Physics2D.OverlapPoint(hero.transform.position, tileMask);
        Collider2D heroOnTile = Physics2D.OverlapPoint(hero.transform.position, heroMask);

        float randomMovePlayer = Random.Range(0, movePlayerDirections.Count);

        if (tile != null && heroOnTile == null)
        {
            switch (randomMovePlayer)
            {
                case 0:
                    Vector2 moveToDirection1 = movePlayerDirections[0];
                    hero.transform.position = (Vector2)hero.transform.position - moveToDirection1;
                    break;
                case 1:

                    Vector2 moveToDirection2 = movePlayerDirections[1];
                    hero.transform.position = (Vector2)hero.transform.position - moveToDirection2;

                    break;
                case 2:

                    Vector2 moveToDirection3 = movePlayerDirections[2];
                    hero.transform.position = (Vector2)hero.transform.position - moveToDirection3;

                    break;
                case 3:

                    Vector2 moveToDirection4 = movePlayerDirections[3];
                    hero.transform.position = (Vector2)hero.transform.position - moveToDirection4;

                    break;
            }
        }
    }
}
