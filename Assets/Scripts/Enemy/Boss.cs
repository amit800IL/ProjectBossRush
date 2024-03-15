using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Action<int> OnEnemyHealthChanged;
    public bool IsBossAlive { get; private set; } = true;
    public bool HasBossAttacked { get; private set; } = false;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private float HP = 0.0f;
    [SerializeField] private float damage = 0.0f;
    [SerializeField] private float defense = 0.0f;
    [SerializeField] private LayerMask charachterMask;
    [SerializeField] private GameObject debugMarkerPrefab;
    [SerializeField] private List<BossActionSetter> enemyActions;

    private int attackIndex = 0;

    private Tile currentTile;
    private RaycastHit2D raycastHit;

    public void BossRestart()
    {
        HasBossAttacked = false;
    }

    public void TakeDamage(float takenDamage)
    {
        HP -= takenDamage;

        Debug.Log("Boss HP is " + HP);
        OnEnemyHealthChanged.Invoke((int)HP);

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
                        if (raycastHit)
                        {
                            DoActionOnTile();
                        }
                    }
                }
            }
        }

        attackIndex++;
    }

    private void DoActionOnTile()
    {
        foreach (BossActionSetter action in enemyActions)
        {
            if (action == enemyActions[attackIndex])
            {
                foreach (Vector2 tile in action.Tiles)
                {
                    if (action.Tiles.Contains(tile))
                    {
                        PerformAction(action);
                        break;
                    }
                }
            }
        }

        HasBossAttacked = true;
    }

    private void PerformAction(BossActionSetter action)
    {
        Hero hero = raycastHit.collider.GetComponent<Hero>();

        action.EnemyAction.DoActionOnHero(hero);
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

