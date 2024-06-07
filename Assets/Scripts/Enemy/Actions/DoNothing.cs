using System.Collections.Generic;
using UnityEngine;

public class DoNothing : EnemyAction
{
    public override void DoActionOnHero(Hero hero)
    {
        Debug.Log("I Have no strength in me");
    }

    public override void DoActionOnTiles(List<Vector2Int> tiles, int actionPower)
    {
        throw new System.NotImplementedException();
    }
}
