using System.Collections;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Boss boss;

    private Coroutine combatTurnsCoroutine;

    private bool isPlayerTurnActive = false;

    private void Start()
    {
        combatTurnsCoroutine = StartCoroutine(CombatTurns());
    }
    public void EndTurn()
    {
        isPlayerTurnActive = false;
        Debug.Log(isPlayerTurnActive);
    }
    private void StartTurn()
    {
        isPlayerTurnActive = true;
        Debug.Log(isPlayerTurnActive);
    }

    private void OnDisable()
    {
        if (combatTurnsCoroutine != null)
        {
            StopCoroutine(combatTurnsCoroutine);

            combatTurnsCoroutine = null;
        }
    }

    private IEnumerator CombatTurns()
    {
        while (boss.IsBossAlive)
        {
            boss.BossRestart();

            yield return new WaitUntil(() => !boss.HasBossAttacked);

            if (!boss.HasBossAttacked)
            {
                boss.VisualizeBossActions();
            }

            StartTurn();

            yield return new WaitUntil(() => !isPlayerTurnActive);

            if (!boss.HasBossAttacked)
            {
                boss.AttackTile();
                yield return new WaitUntil(() => boss.HasBossAttacked);
            }
        }

    }
}