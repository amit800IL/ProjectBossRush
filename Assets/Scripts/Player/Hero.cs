using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected PlayerResourceManager manager;
    public SymbolTable SymbolTable { get; protected set; }
    [field: SerializeField] public float Damage { get; protected set; } = 0.0f;

    [SerializeField] protected float HP = 0.0f;

    [SerializeField] protected float Defense = 0.0f;

    [SerializeField] protected LayerMask tileMask;

    protected Collider2D overLappedPoint;
    public Tile CurrentTile { get; protected set; }

    public bool HasHeroMoved { get; protected set; } = false;

    protected virtual void Start()
    {
        SetTilePosition();
    }
    public void MoveHeroToPosition(Vector2 targetPositionInGrid)
    {
        if (IsHeroInMoveRange(targetPositionInGrid))
        {
            transform.position = targetPositionInGrid;

            if ((Vector2)transform.position == targetPositionInGrid)
            {
                HasHeroMoved = true;
                SetTilePosition();
            }
        }
    }

    public void ResetHeroMovement()
    {
        HasHeroMoved = false;
    }

    protected void SetTilePosition()
    {
        overLappedPoint = Physics2D.OverlapPoint(transform.position, tileMask);

        CurrentTile = overLappedPoint.GetComponent<Tile>();
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

    public abstract bool CanHeroAttack();
}




