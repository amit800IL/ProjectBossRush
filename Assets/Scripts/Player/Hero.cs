using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public SymbolTable SymbolTable { get; protected set; }

    [SerializeField] protected float damage = 0.0f;

    [SerializeField] protected float HP = 0.0f;

    [SerializeField] protected float Defense = 0.0f;

    public Tile CurrentTile { get; protected set; }
    protected RaycastHit2D raycastHit;
    public bool HasHeroMoved { get; protected set; } = false;

    protected virtual void Start()
    {
        CurrentTile = TileGetter.GetTile(transform.position, out raycastHit);
        CurrentTile.OccupyTile(this.gameObject);
    }

    public void MoveHeroToPosition(Vector2 targetPositionInGrid)
    {
        if (IsHeroInMoveRange(targetPositionInGrid))
        {
            transform.position = targetPositionInGrid;

            if ((Vector2)transform.position == targetPositionInGrid && CurrentTile != null)
            {
                HasHeroMoved = true;
                CurrentTile = TileGetter.GetTile(targetPositionInGrid, out raycastHit);
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




