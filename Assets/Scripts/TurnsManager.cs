using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] private Boss boss;

    private bool isPlayerTurnActive = false;

    private void Start()
    {
        StartPlayerTurn();
    }
    private void StartPlayerTurn()
    {
        if (boss.IsBossAlive)
        {
            if (boss.HasBossAttacked)
                boss.BossRestart();

            if (!boss.HasBossAttacked)
                boss.VisualizeBossActions();

            isPlayerTurnActive = true;
        }
    }
    public void EndPlayerTurn()
    {
        if (boss.IsBossAlive)
        {
            isPlayerTurnActive = false;

            if (!boss.HasBossAttacked)
                boss.AttackTile();

            if (boss.HasBossAttacked)
                StartPlayerTurn();
        }
    }
}