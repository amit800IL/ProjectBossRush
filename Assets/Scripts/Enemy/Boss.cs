using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("General Variables")]

    public static Action<Boss> OnEnemyHealthChanged;
    public static event Action OnBossAttack;
    public static event Action OnBossInjured;
    public static event Action OnBossDeath;
    public bool IsBossAlive { get; private set; } = true;
    public bool HasBossAttacked { get; private set; } = false;

    [SerializeField] private int attackIndex = 0;

    [SerializeField] public Animator bossAnimator;

    [SerializeField] private Camera mainCamera;

    [field: Header("Boss Attributes")]

    [field: SerializeField] public int maxHP { get; private set; } = 100;
    public int HP { get; private set; } = 0;
    [field: SerializeField] public float Damage { get; private set; } = 0.0f;

    [SerializeField] private float Defense = 0.0f;

    [Header("Attacking actions")]

    [SerializeField] private BossTargeting bossTargeting; //better to assign at Start
    private List<Vector2Int> targetTiles;
    [SerializeField] private GameObject debugMarkerPrefab;
    [SerializeField] private TextMeshPro attackText;
    [SerializeField] private List<BossActionSetter> enemyActions;
    private RaycastHit raycastHit;
    public List<GameObject> currentAttackMarker = new();

    private void Start()
    {
        HP = maxHP;
    }

    public void BossRestart()
    {
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

        OnBossInjured?.Invoke();

        if (HP <= 0)
        {
            StartCoroutine(DeactivateBossTimer());
        }
    }

    private IEnumerator DeactivateBossTimer()
    {
        bossAnimator.SetTrigger("Death");
        IsBossAlive = false;
        yield return new WaitForSeconds(5f);
        OnBossDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void InteractWithTiles(bool VisualizeAttack) //instead of taking a parameter, bool should be dependant per attack
    {
        Tile[,] tiles = GridManager.Instance.Tiles;
        bool targetHero = enemyActions[attackIndex].Target.TargetHeroItself;
        BossActionSetter currentAction = enemyActions[attackIndex];

        if (VisualizeAttack)
        {
            if (targetHero)
            {
                bossTargeting.MarkTargetedHeroes(currentAction);
            }
            else
            {
                targetTiles = ReadBossAction(attackIndex);
                foreach (Vector2Int tilePosition in targetTiles)
                {
                    Tile tile = tiles[tilePosition.x, tilePosition.y];
                    currentAttackMarker.Add(Instantiate(debugMarkerPrefab, tile.OccupantContainer.position - new Vector3(0f, 0.9f, 0f), debugMarkerPrefab.transform.rotation));
                }
            }
            attackText.text = $"{currentAction.ActionName}\n{currentAction.Power} Damage";
        }
        else
        {
            if (targetHero)
            {
                targetTiles = ReadBossAction(attackIndex);
                bossTargeting.UnMarkTargetedHeroes();
            }

            currentAction.EnemyAction.DoActionOnTiles(targetTiles, currentAction.Power);

            if (currentAction.BossVFX != null) currentAction.BossVFX.SetActive(true);

            if (currentAction.HitVFX != null)
            {
                Vector2Int hitPos = bossTargeting.GetCenters()[0];
                currentAction.HitVFX.transform.position = tiles[hitPos.x, hitPos.y].transform.position;
                currentAction.HitVFX.SetActive(true);
            }

            OnBossAttack?.Invoke();

            foreach (var item in currentAttackMarker)
            {
                Destroy(item);
            }
            currentAttackMarker.Clear();
            HasBossAttacked = true;
        }
    }

    public void PlayActionAnimation()
    {
        bossAnimator.SetTrigger(enemyActions[attackIndex].ActionName);
    }

    private void PerformAction(BossActionSetter action, Tile tile)
    {
        if (tile != null)
        {
            //action.EnemyAction.DoActionOnTiles(tile, action.Power);
        }

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
    [SerializeField] private string actionName;
    [field: SerializeField] private EnemyAction enemyAction;
    [SerializeField] private int power;
    [SerializeField] private TargetInfo target;
    [field: SerializeField] private List<Vector2Int> tiles;
    [SerializeField] private GameObject targetMarker;
    [SerializeField] private GameObject bossVFX;
    [SerializeField] private GameObject hitVFX;

    public string ActionName { get => actionName; private set => actionName = value; }
    public EnemyAction EnemyAction { get => enemyAction; private set => enemyAction = value; }
    public int Power { get => power; private set => power = value; }
    public TargetInfo Target { get => target; private set => target = value; }
    public List<Vector2Int> Tiles { get => tiles; private set => tiles = value; }
    public GameObject TargetMarker { get => targetMarker; private set => targetMarker = value; }
    public GameObject BossVFX { get => bossVFX; private set => bossVFX = value; }
    public GameObject HitVFX { get => hitVFX; private set => hitVFX = value; }
}

