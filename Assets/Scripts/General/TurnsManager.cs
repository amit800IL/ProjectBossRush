using System;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{

    public static event Action OnRoundStart;
    public static event Action OnPlayerTurnStart;

    [SerializeField] private Boss boss;

    private bool isPlayerTurnActive = false;
    private bool isBossTurnActive = false;
    private float bossTurnDuration = 10f;
    private float bossTurnTimer = 0f;

    bool onlyVisualizeAction = true;

    private void Start()
    {
        StartPlayerTurn();
    }

    private void Update()
    {
        if (isBossTurnActive)
        {
            bossTurnTimer += Time.deltaTime;

            if (bossTurnTimer >= bossTurnDuration)
            {
                isBossTurnActive = false;
                StartPlayerTurn();
            }
        }
    }

    private void StartPlayerTurn()
    {
        OnRoundStart?.Invoke();
        bool visualizeAction = onlyVisualizeAction;

        if (boss.IsBossAlive)
        {
            if (boss.HasBossAttacked)
                boss.BossRestart();

            if (!boss.HasBossAttacked)
                boss.InteractWithTiles(visualizeAction);

            isPlayerTurnActive = true;
            OnPlayerTurnStart?.Invoke();
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

            isBossTurnActive = false;
            bossTurnTimer = 0f;
        }
    }
}