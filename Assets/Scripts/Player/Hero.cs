using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected PlayerResourceManager manager;
    public SymbolTable SymbolTable { get; protected set; }
    [field: SerializeField] public float Damage { get; protected set; } = 0.0f;

    [SerializeField] protected float HP = 0.0f;

    [SerializeField] protected float Defense = 0.0f;

    protected RaycastHit2D raycastHit;

    protected Tile CurrentTile;
    public bool HasHeroMoved { get; protected set; } = false;

    public void MoveHeroToPosition(Vector2 targetPositionInGrid)
    {
        if (IsHeroInMoveRange(targetPositionInGrid))
        {
            transform.position = targetPositionInGrid;

            if ((Vector2)transform.position == targetPositionInGrid && CurrentTile != null)
            {
                HasHeroMoved = true;
            }
        }
    }

    public void ResetHeroMovement()
    {
        HasHeroMoved = false;
    }

    private bool IsHeroInMoveRange(Vector2 newPosition)
    {
        return OnOneTileRange(newPosition) && !OnDiagonalDirection(newPosition);
    }

    private bool OnOneTileRange(Vector2 newPosition)
    {
        return Mathf.Abs(newPosition.x - transform.position.x) <= 1f && Mathf.Abs(newPosition.y - transform.position.y) <= 1f;
    }

    private bool OnDiagonalDirection(Vector2 newPosition)
    {
        return Mathf.Abs(newPosition.x - transform.position.x) == 1f && Mathf.Abs(newPosition.y - transform.position.y) == 1f;
    }

    public void HealthDown()
    {
        HP -= 10f;
        Debug.Log(gameObject.name + "Health : " + HP);

        if (HP == 0)
        {
            HP = 100f;

            gameObject.SetActive(false);
        }
    }

    public abstract void HeroAttackBoss(Boss boss);
    public abstract bool CanHeroAttack();
}




