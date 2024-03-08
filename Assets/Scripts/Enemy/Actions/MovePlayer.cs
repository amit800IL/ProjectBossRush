using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : EnemyAction
{
    [SerializeField] private List<Vector2> movePlayerDirections;

    public void MovePlayeInDirections(Hero hero)
    {
        float randomMovePlayer = Random.Range(0, movePlayerDirections.Count);

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