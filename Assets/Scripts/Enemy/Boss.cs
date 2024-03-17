using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("General Variables")]

    public static Action<int> OnEnemyHealthChanged;
    public bool IsBossAlive { get; private set; } = true;
    public bool HasBossAttacked { get; private set; } = false;

    private int attackIndex = 0;

    [Header("Boss Attributes")]

    [SerializeField] private float HP = 0.0f;
    [SerializeField] private float damage = 0.0f;
    [SerializeField] private float defense = 0.0f;

    [Header("Attacking actions")]

    [SerializeField] private GameObject debugMarkerPrefab;
    [SerializeField] private List<BossActionSetter> enemyActions;
    private Tile tile;
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
                foreach (Vector2 tilePosition in action.Tiles)
                {
                    if (action.Tiles.Contains(tilePosition))
                    {
                        tile = TileGetter.GetTile(tilePosition, out raycastHit);
                        PerformAction(action);
                    }
                }
            }
        }

        attackIndex++;
        HasBossAttacked = true;
    }

    private void PerformAction(BossActionSetter action)
    {
        if (IsTileValid())
        {
            Hero hero = tile.GetOccupier().GetComponent<Hero>();

            if (hero != null)
            {
                Debug.Log("Found hero: " + hero.name + " on tile : " + tile.gameObject.name);
                action.EnemyAction.DoActionOnHero(hero);
                return;
            }
        }

        Debug.LogWarning("No hero found on the tile : " + tile.name);
    }

    private bool IsTileValid()
    {
       return raycastHit && tile != null && tile.IsTileOccupied;
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

