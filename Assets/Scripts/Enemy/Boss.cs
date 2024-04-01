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

    [SerializeField] private Animator bossAnimator;

    [SerializeField] private Camera mainCamera;

    [Header("Boss Attributes")]

    [SerializeField] private float HP = 0.0f;
    [SerializeField] private float damage = 0.0f;
    [SerializeField] private float defense = 0.0f;

    [Header("Attacking actions")]

    [SerializeField] private GameObject debugMarkerPrefab;
    [SerializeField] private List<BossActionSetter> enemyActions;
    private Tile tile;
    private RaycastHit raycastHit;

    public void BossRestart()
    {
        if (HasBossAttacked)
            attackIndex++;

        HasBossAttacked = false;
    }

    public void TakeDamage(float takenDamage)
    {
        HP -= takenDamage;

        Debug.Log("Boss HP is " + HP);
        OnEnemyHealthChanged.Invoke((int)HP);

        bossAnimator.SetTrigger("Injured");

        if (HP <= 0)
        {
            IsBossAlive = false;
            gameObject.SetActive(false);
        }
    }

    public void InteractWithTiles(bool VisualizeAttack)
    {
        Tile[,] tiles = GridManager.Instance.Tiles;

        foreach (Vector2Int tilePosition in enemyActions[attackIndex].Tiles)
        {
            if (VisualizeAttack)
            {
                Tile tile = tiles[tilePosition.x,tilePosition.y];

                GameObject marker = Instantiate(debugMarkerPrefab, tile.OccupantContainer.position, debugMarkerPrefab.transform.rotation);

                Destroy(marker, 2f);
            }
            else
            {

                PerformAction(enemyActions[attackIndex]);
            }
        }
    }

    private void PerformAction(BossActionSetter action)
    {
        if (IsTileValid())
        {
            Hero hero = (Hero)tile.GetOccupier();

            bossAnimator.SetTrigger("Attack");

            if (hero != null)
            {
                Debug.Log("Found hero: " + hero.name + " on a tile");
                action.EnemyAction.DoActionOnHero(hero);

                HasBossAttacked = true;

                return;
            }
        }

        Debug.LogWarning("No hero found on the checked tile");
    }

    private bool IsTileValid()
    {
        return raycastHit.collider != null && tile != null && tile.IsTileOccupied;
    }
}

[System.Serializable]
public class BossActionSetter
{
    [field: SerializeField] private EnemyAction enemyAction;
    [field: SerializeField] private List<Vector2Int> tiles;

    public EnemyAction EnemyAction { get => enemyAction; private set => enemyAction = value; }
    public List<Vector2Int> Tiles { get => tiles; private set => tiles = value; }

}

