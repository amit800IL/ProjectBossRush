using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : EnemyAction
{
    [SerializeField] private List<Vector2Int> movePlayerDirections;
    public override void DoActionOnHero(Hero hero)
    {
        MovePlayeInDirections(hero);
    }

    public void MovePlayeInDirections(Hero hero)
    {
        int randomIndex = Random.Range(0, movePlayerDirections.Count);

        if (movePlayerDirections != null && movePlayerDirections.Count > 0)
        {
            Vector3Int moveDirection = (Vector3Int)movePlayerDirections[randomIndex];
            moveDirection = new Vector3Int(moveDirection.x, 0, moveDirection.y);
            hero.transform.position = hero.transform.position - moveDirection;
        }
    }
}
