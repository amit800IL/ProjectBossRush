using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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
    [SerializeField] private List<BossActionSetter> enemyActions;

    private Dictionary<EnemyAction, EnemyActions> enemyActiosList;

    private int attackIndex = 0;

    public void BossRestart()
    {
        HasBossAttacked = false;
    }

    public void TakeDamage(float takenDamage)
    {
        HP -= takenDamage;

        Debug.Log("Enemy Attacked, hp now is: " + HP);

        if (HP <= 0)
        {
            IsBossAlive = false;
            gameObject.SetActive(false);
        }
    }

    public void VisualizeBossActions()
    {
        foreach (BossActionSetter action in enemyActions)
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
        foreach (BossActionSetter action in enemyActions)
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
                            DoActionOnTile(overLappedPoint);
                        }
                    }
                }
            }
        }

        attackIndex++;
    }

    private void DoActionOnTile(Collider2D overLappedPoint)
    {
        foreach (BossActionSetter action in enemyActions)
        {
            if (action == enemyActions[attackIndex])
            {
                foreach (Vector2 tile in action.Tiles)
                {
                    if (action.Tiles.Contains(tile))
                    {
                        PerformAction(action, overLappedPoint);
                        break;
                    }
                }
            }
        }

        HasBossAttacked = true;
    }

    private void PerformAction(BossActionSetter action, Collider2D overLappedPoint)
    {
        switch (action.EnemyAction)
        {
            case MovePlayer:
                MovePlayer moveAction = action.EnemyAction.GetComponent<MovePlayer>();
                Hero hero = overLappedPoint.GetComponent<Hero>();
                moveAction.MovePlayeInDirections(hero);
                break;
            case EnemyAction:
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
public class BossActionSetter
{
    [field: SerializeField] private EnemyAction enemyAction;
    [field: SerializeField] private List<Vector2> tiles;

    public EnemyAction EnemyAction { get => enemyAction; private set => enemyAction = value; }
    public List<Vector2> Tiles { get => tiles; private set => tiles = value; }

}

