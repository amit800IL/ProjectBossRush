using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    public abstract void DoActionOnHero(Hero hero);

    public abstract void DoActionOnTiles(List<Vector2Int> tiles, int actionPower);

}
