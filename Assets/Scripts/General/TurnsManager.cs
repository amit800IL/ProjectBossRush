using System;
using System.Collections;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{

    public static event Action OnRoundStart;
    public static event Action OnPlayerTurnStart;
    public static event Action OnBossTurnStart;

    [SerializeField] private Boss boss;
    [SerializeField] private float turnBuffer = 1;
    [SerializeField] private float bossTurnDuration = 2;

    private bool isPlayerTurnActive = false;

    private void Awake()
    {
        TutorialScript.OnTutorialFinished += StartPlayerTurn;
    }

    private void OnDestroy()
    {
        TutorialScript.OnTutorialFinished -= StartPlayerTurn;
    }

    private void StartPlayerTurn()
    {
        if (isPlayerTurnActive) return;

        OnRoundStart?.Invoke();

        if (boss.IsBossAlive)
        {
            if (boss.HasBossAttacked)
                boss.BossRestart();

            if (!boss.HasBossAttacked)
                boss.InteractWithTiles(true);

            isPlayerTurnActive = true;
            OnPlayerTurnStart?.Invoke();
        }
    }
    public void EndPlayerTurn()
    {
        StartCoroutine(BossTurn());
    }

    IEnumerator BossTurn()
    {
        OnBossTurnStart?.Invoke();
        yield return new WaitForSeconds(turnBuffer);

        if (boss.IsBossAlive)
        {
            isPlayerTurnActive = false;

            if (!boss.HasBossAttacked)
            {
                boss.PlayActionAnimation();
                yield return new WaitForSeconds(1);
                boss.InteractWithTiles(false);
            }

            if (boss.HasBossAttacked)
                yield return new WaitForSeconds(bossTurnDuration);
        }
        StartPlayerTurn();
    }
}