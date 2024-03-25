using System;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    public static event Action OnPlayerTurnStart;

    [SerializeField] private Boss boss;

    private bool isPlayerTurnActive = false;

    bool onlyVisualizeAction = true;

    private void Start()
    {
        StartPlayerTurn();
    }
    private void StartPlayerTurn()
    {
        bool visualizeAction = onlyVisualizeAction;

        if (boss.IsBossAlive)
        {
            if (boss.HasBossAttacked)
                boss.BossRestart();

            if (!boss.HasBossAttacked)
                boss.InteractWithTiles(visualizeAction);

            isPlayerTurnActive = true;
            OnPlayerTurnStart.Invoke();
        }
    }  
    public void EndPlayerTurn()
    {
        bool attackTile = !onlyVisualizeAction;

        if (boss.IsBossAlive)
        {
            isPlayerTurnActive = false;

            if (!boss.HasBossAttacked)
                boss.InteractWithTiles(attackTile);

            if (boss.HasBossAttacked)
                StartPlayerTurn();

        }
    }
}