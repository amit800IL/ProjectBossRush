using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("General Variables")]

    public static Action<Boss> OnEnemyHealthChanged;
    public bool IsBossAlive { get; private set; } = true;
    public bool HasBossAttacked { get; private set; } = false;

    [SerializeField] private int attackIndex = 0;

    [SerializeField] private Animator bossAnimator;

    [SerializeField] private Camera mainCamera;

    [field: Header("Boss Attributes")]

    [field: SerializeField] public int maxHP { get; private set; } = 100;
    public int HP { get; private set; } = 0;
    [field: SerializeField] public float Damage { get; private set; } = 0.0f;

    [SerializeField] private float Defense = 0.0f;

    [Header("Attacking actions")]

    [SerializeField] private BossTargeting bossTargeting;
    private List<Vector2Int> targetTiles;
    [SerializeField] private GameObject debugMarkerPrefab;
    [SerializeField] private List<BossActionSetter> enemyActions;
    private RaycastHit raycastHit;

    private void Start()
    {
        HP = maxHP;
    }

    public void BossRestart()
    {
        print("bossrestart");
        if (HasBossAttacked)
            attackIndex++;
        if (attackIndex == enemyActions.Count)
            attackIndex = 0;
        HasBossAttacked = false;
    }

    public void TakeDamage(float takenDamage)
    {
        HP -= (int)takenDamage;

        Debug.Log("Boss HP is " + HP);
        OnEnemyHealthChanged.Invoke(this);

        bossAnimator.SetTrigger("Injured");

        if (HP <= 0)
        {
            IsBossAlive = false;
            gameObject.SetActive(false);
        }
    }

    public void InteractWithTiles(bool VisualizeAttack) //instead of taking a parameter, bool should be dependant per attack
    {
        Tile[,] tiles = GridManager.Instance.Tiles;
        bool targetHero = enemyActions[attackIndex].Target.TargetHeroItself;
        if (VisualizeAttack && targetHero)
        {
            print("hi");

            foreach (Hero hero in bossTargeting.GetTargetHeroes(enemyActions[attackIndex].Target))
            {
                hero.ApplyTargetMarker(enemyActions[attackIndex].TargetMarker);
            }
        }

        if (VisualizeAttack || targetHero) //not efficient
            targetTiles = ReadBossAction(attackIndex);

        foreach (Vector2Int tilePosition in targetTiles)
        {
            Tile tile = tiles[tilePosition.x, tilePosition.y];

            if (VisualizeAttack)
            {
                if (!targetHero)
                {
                    GameObject marker = Instantiate(debugMarkerPrefab, tile.OccupantContainer.position - new Vector3(0f, 0.9f, 0f), debugMarkerPrefab.transform.rotation);

                    Destroy(marker, 2f);
                }
            }
            else
            {
                PerformAction(enemyActions[attackIndex], tile);
                bossAnimator.SetTrigger("Attack");
            }
        }
        if (!VisualizeAttack)
            HasBossAttacked = true;
    }

    private void PerformAction(BossActionSetter action, Tile tile)
    {
        if (IsTileValid(tile))
        {
            action.EnemyAction.DoActionOnTile(tile);
            /*
            Hero hero = (Hero)tile.GetOccupier();

            //bossAnimator.SetTrigger("Attack");

            if (hero != null)
            {
                Debug.Log("Found hero: " + hero.name + " on a tile");
                action.EnemyAction.DoActionOnHero(hero);


                return;
            }
            */
        }

        //Debug.LogWarning("No hero found on the checked tile");
    }

    private bool IsTileValid(Tile tile)
    {
        return tile != null && tile.IsTileOccupied;
    }

    private List<Vector2Int> ReadBossAction(int index)
    {
        List<Vector2Int> toReturn;
        toReturn = bossTargeting.GetTargetTile(enemyActions[index].Target);
        if (toReturn.Count == 0)
            toReturn = enemyActions[index].Tiles;
        return toReturn;
    }
}

[System.Serializable]
public class BossActionSetter
{
    [field: SerializeField] private EnemyAction enemyAction;
    [SerializeField] private TargetInfo target;
    [field: SerializeField] private List<Vector2Int> tiles;
    [SerializeField] private GameObject targetMarker;

    public EnemyAction EnemyAction { get => enemyAction; private set => enemyAction = value; }
    public TargetInfo Target { get => target; private set => target = value; }
    public List<Vector2Int> Tiles { get => tiles; private set => tiles = value; }
    public GameObject TargetMarker { get => targetMarker; private set => targetMarker = value; }

}

