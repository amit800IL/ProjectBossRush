using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> playerAction;
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
                Debug.Log("Searach tiles");
                boss.SearchTiles();
            }

            foreach (PlayerAction playerAction in playerAction)
            {
                playerAction.PlayerRestart();
                Debug.Log("Wait for player");
                yield return new WaitUntil(() => playerAction.HasPlayerDoneAction);
                Debug.Log("Player done");
            }

            if (!boss.HasBossAttacked)
            {
                boss.AttackTile(boss.RandomTile.tilePosition);
                Debug.Log("Attack tile");
                yield return new WaitUntil(() => boss.HasBossAttacked);
                Debug.Log("tile marked as attacked");
            }
        }

    }

}