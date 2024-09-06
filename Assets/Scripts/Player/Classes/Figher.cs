using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figher : Hero
{
    [SerializeField] private BerzekerProjectile berzekerProjectile;
    [SerializeField] private List<GameObject> quickAttackObjets = new List<GameObject>();
    private Coroutine sequenceCoroutine;
    private bool isSequenceRunning = false;
    protected override void Start()
    {
        SymbolTable = new SymbolTable((int)SymbolTable.Symbols.Fighter);
        base.Start();
    }

    public override bool CanHeroAttack(Boss boss)
    {
        if (AttackPosCondition(currentTile))
        {
            Vector3 positionOffset = new Vector3(0, -2, 0);
            berzekerProjectile.MoveProjectile(boss.transform.position + positionOffset);
        }
        return AttackPosCondition(currentTile);
    }

    public override bool CanHeroDefend()
    {
        return DefendPosCondition(currentTile);
    }

    public override bool AttackPosCondition(Tile tile)
    {
        return tile != null && tile.IsTileOfType(TileType.CloseRange);
    }

    public override bool DefendPosCondition(Tile tile)
    {
        return !tile.IsTileOfType(TileType.LongRange);
    }

    public IEnumerator QuickAttackSequence()
    {
        quickAttackObjets[0].SetActive(true);

        Vector3 originalPosition = transform.position;
        GameObject object1 = quickAttackObjets[1];
        GameObject object2 = quickAttackObjets[2];
        GameObject object3 = quickAttackObjets[3];
        Boss boss = FindObjectOfType<Boss>();

        TurnOffAllObects();

        object1.SetActive(true);
        object2.SetActive(true);

        yield return new WaitForSeconds(1f);

        TurnOffAllObects();

        object2.SetActive(true);

        float timerMax = 0.2f;
        float timerStart = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = boss.transform.position + new Vector3(-3, 0, 0);

        while (timerStart < timerMax)
        {
            timerStart += Time.deltaTime;

            float progress = timerStart / timerMax;

            Vector3 positionOffset = new Vector3(-3, 0, 0);

            transform.position = Vector3.Lerp(startPosition, endPosition, progress);

            object2.transform.position = transform.position;

            yield return null; 
        }

        TurnOffAllObects();

        object3.transform.position = boss.transform.position;
        object3.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        heroAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.2f);

        boss.TakeDamage(HeroData.damage);

        yield return new WaitForSeconds(1f);

        TurnOffAllObects();

        transform.position = originalPosition;

        quickAttackObjets[0].SetActive(false);
    }

    private void TurnOffAllObects()
    {
        foreach (GameObject obj in quickAttackObjets)
        {
            if (obj != quickAttackObjets[0])
            {
                obj.SetActive(false);
            }
        }
    }
}
