using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool IsBossAlive { get; private set; } = true;
    public bool HasBossAttacked { get; private set; } = false;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private float HP = 0.0f;
    [SerializeField] private float damage = 0.0f;
    [SerializeField] private float defense = 0.0f;
    [SerializeField] private LayerMask charachterMask;
    [SerializeField] private GameObject debugMarkerPrefab;
    [SerializeField] private List<BossAction> enemyActions;


    private int attackIndex = 0;

    public void BossRestart()
    {
        HasBossAttacked = false;
    }

    public void TakeDamage(float takenDamage)
    {
        HP -= takenDamage;

        if (HP <= 0)
        {
            IsBossAlive = false;
            gameObject.SetActive(false);
        }
    }

    public void VisualizeBossActions()
    {
        foreach (BossAction action in enemyActions)
        {
            if (action == enemyActions[attackIndex])
            {
                foreach (Vector2 markerPosition in action.Tiles)
                {
                    GameObject marker = Instantiate(debugMarkerPrefab, markerPosition, Quaternion.identity);
                    Destroy(marker, 2f);
                }
            }
        }
    }

    public void AttackTile()
    {
        foreach (BossAction action in enemyActions)
        {
            if (action == enemyActions[attackIndex])
            {
                foreach (Vector2 tile in action.Tiles)
                {
                    if (action.Tiles.Contains(tile))
                    {
                        Collider2D overLappedPoint = Physics2D.OverlapPoint(tile, charachterMask);

                        if (overLappedPoint != null)
                        {
                            DoActionOnTile(tile, overLappedPoint);
                        }
                    }
                }
            }
        }

        attackIndex++;
    }

    private void DoActionOnTile(Vector2 tilePosition, Collider2D overLappedPoint)
    {
        foreach (BossAction action in enemyActions)
        {
            if (action == enemyActions[attackIndex])
            {
                foreach (Vector2 tile in action.Tiles)
                {
                    if (action.Tiles.Contains(tile))
                    {
                        PerformAction(tilePosition, action, overLappedPoint);
                        break;
                    }
                }
            }
        }

        HasBossAttacked = true;
    }

    private void PerformAction(Vector2 tilePosition, BossAction action, Collider2D overLappedPoint)
    {
        switch (action.EnemyAction)
        {
            case EnemyActions.Attack:
                overLappedPoint.GetComponent<Hero>().HealthDown();
                break;
            case EnemyActions.MovePlayer:
                overLappedPoint.GetComponent<Hero>().MoveHeroToPosition(tilePosition - new Vector2(1, 0));
                break;
            case EnemyActions.DoNothing:
                Debug.Log("I have no strength in me");
                break;
        }
    }
}

public enum EnemyActions
{
    DoNothing,
    MovePlayer,
    Attack,
}

[System.Serializable]
public class BossAction
{
    [field: SerializeField] private EnemyActions enemyAction;
    [field: SerializeField] private List<Vector2> tiles;

    public EnemyActions EnemyAction { get => enemyAction; private set => enemyAction = value; }
    public List<Vector2> Tiles { get => tiles; private set => tiles = value; }
}

