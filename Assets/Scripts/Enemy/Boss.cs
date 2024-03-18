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
                tile = TileGetter.GetTile(tilePosition, out raycastHit);

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
                Debug.Log("Found hero: " + hero.name + " on tile : " + tile.gameObject.name);
                action.EnemyAction.DoActionOnHero(hero);

                attackIndex++;
                HasBossAttacked = true;

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

[System.Serializable]
public class BossActionSetter
{
    [field: SerializeField] private EnemyAction enemyAction;
    [field: SerializeField] private List<Vector2> tiles;

    public EnemyAction EnemyAction { get => enemyAction; private set => enemyAction = value; }
    public List<Vector2> Tiles { get => tiles; private set => tiles = value; }

}

