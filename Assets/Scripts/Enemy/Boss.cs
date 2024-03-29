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
        foreach (Vector2 tilePosition in enemyActions[attackIndex].Tiles)
        {
            if (VisualizeAttack)
            {
                GameObject marker = Instantiate(debugMarkerPrefab, tilePosition, Quaternion.identity);
                Destroy(marker, 2f);
            }
            else
            {
                tile = TileGetter.GetTileFromCamera(tilePosition, out raycastHit);

                PerformAction(enemyActions[attackIndex]);
            }
        }
    }

    private void PerformAction(BossActionSetter action)
    {
        if (IsTileValid())
        {
            Hero hero = tile.GetOccupier().GetComponent<Hero>();

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
    [field: SerializeField] private List<Vector2> tiles;

    public EnemyAction EnemyAction { get => enemyAction; private set => enemyAction = value; }
    public List<Vector2> Tiles { get => tiles; private set => tiles = value; }

}

