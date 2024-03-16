using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : EnemyAction
{
    [SerializeField] private List<Vector2> movePlayerDirections;
    public override void DoActionOnHero(Hero hero)
    {
        MovePlayeInDirections(hero);
    }

    public void MovePlayeInDirections(Hero hero)
    {
        int randomIndex = Random.Range(0, movePlayerDirections.Count);

        if (movePlayerDirections != null && movePlayerDirections.Count > 0)
        {
            Vector2 moveDirection = movePlayerDirections[randomIndex];
            hero.transform.position = (Vector2)hero.transform.position - moveDirection;
        }
    }
}
