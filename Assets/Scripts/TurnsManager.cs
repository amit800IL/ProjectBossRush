using System.Collections;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private PlayerAction playerAction;
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
            playerAction.PlayerRestart();

            if (!boss.HasBossAttacked)
            {
                boss.SearchTiles();

                yield return new WaitUntil(() => playerAction.HasPlayerDoneAction);

                boss.AttackTile(boss.RandomTile.tilePosition);

                yield return new WaitUntil(() => boss.HasBossAttacked);
            }
        }

    }

}