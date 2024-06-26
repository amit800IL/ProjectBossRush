//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class OldBoss : MonoBehaviour
//{
//    public bool IsBossAlive { get; private set; } = true;
//    public Tile RandomTile { get; private set; }
//    public bool HasBossAttacked { get; private set; } = false;

//    [SerializeField] private GridManager gridManager;
//    [SerializeField] private float HP = 0.0f;
//    [SerializeField] private float damage = 0.0f;
//    [SerializeField] private float defense = 0.0f;
//    [SerializeField] private int maxChosenTiles, minChosenTiles;
//    [SerializeField] private LayerMask charachterMask;
//    [SerializeField] private GameObject debugMarkerPrefab;

//    private Tile[,] tiles;
//    private List<EnemyActions> enemyActions;
//    private int totalRows, totalColumns;

//    private void Start()
//    {
//        tiles = gridManager.tilesGrid;
//        totalRows = tiles.GetLength(0);
//        totalColumns = tiles.GetLength(1);
//        enemyActions = Enum.GetValues(typeof(EnemyActions)).Cast<EnemyActions>().ToList();
//    }

//    public void BossRestart()
//    {
//        HasBossAttacked = false;
//    }

//    public void TakeDamage(float takenDamage)
//    {
//        HP -= takenDamage;

//        if (HP <= 0)
//        {
//            IsBossAlive = false;
//            gameObject.SetActive(false);
//        }
//    }

//    public void SearchTiles()
//    {
//        int randomRowCount = UnityEngine.Random.Range(minChosenTiles, Mathf.Min(maxChosenTiles, totalRows));
//        int randomColumnCount = UnityEngine.Random.Range(minChosenTiles, Mathf.Min(maxChosenTiles, totalColumns));

//        ChooseTiles(totalRows, totalColumns, randomRowCount, randomColumnCount);
//    }

//    private void ChooseTiles(int totalRows, int totalColums, int randomRowCount, int randomColumnCount)
//    {
//        for (int i = 0; i < randomRowCount; i++)
//        {
//            int randomRow = UnityEngine.Random.Range(0, totalRows);

//            for (int j = 0; j < randomColumnCount; j++)
//            {
//                int randomColumn = UnityEngine.Random.Range(0, totalColums);

//                RandomTile = tiles[randomRow, randomColumn];
//                //For debug Purposes
//                GameObject marker = Instantiate(debugMarkerPrefab, RandomTile.tilePosition, Quaternion.identity);

//                Destroy(marker, 2f);
//            }
//        }
//    }

//    public void AttackTile(Vector2 tilePosition)
//    {
//        Collider2D overLappedPoint = Physics2D.OverlapPoint(tilePosition, charachterMask);

//        if (overLappedPoint != null)
//        {
//            DoActionOnTile(tilePosition, overLappedPoint);
//        }

//        HasBossAttacked = true;
//    }

//    private void DoActionOnTile(Vector2 tilePosition, Collider2D overLappedPoint)
//    {
//        enemyActions = Shuffle(enemyActions);

//        EnemyActions chooseRandomAction = enemyActions[0];

//        switch (chooseRandomAction)
//        {
//            case EnemyActions.Attack:
//                overLappedPoint.GetComponent<Hero>().HealthDown();
//                break;
//            case EnemyActions.MovePlayer:
//                overLappedPoint.GetComponent<Hero>().MoveHeroToPosition(tilePosition - new Vector2(1, 0));
//                break;
//            case EnemyActions.DoNothing:
//                Debug.Log("I have no strength in me");
//                break;
//        }
//    }

//    private List<T> Shuffle<T>(List<T> list)
//    {
//        int n = list.Count;

//        while (n > 1)
//        {
//            n--;
//            int k = UnityEngine.Random.Range(0, n + 1);
//            T value = list[k];
//            list[k] = list[n];
//            list[n] = value;
//        }

//        Debug.Log("Shuffled List: " + string.Join(", ", list.Select(x => x.ToString()).ToArray()));

//        return list;
//    }
//}

//public enum EnemyActions
//{
//    DoNothing,
//    MovePlayer,
//    Attack,
//}

