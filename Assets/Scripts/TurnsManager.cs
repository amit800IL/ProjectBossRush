using System.Collections;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Boss boss;

    private Coroutine combatTurnsCoroutine;
    private void Start()
    {
        combatTurnsCoroutine = StartCoroutine(CombatTurns());
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
                boss.SearchTiles();
            }

            controller.PlayerRestart();
            yield return new WaitUntil(() => controller.HasPlayerDoneAction);

            if (!boss.HasBossAttacked)
            {
                boss.AttackTile(boss.RandomTile.tilePosition);
                yield return new WaitUntil(() => boss.HasBossAttacked);
            }
        }

    }

}