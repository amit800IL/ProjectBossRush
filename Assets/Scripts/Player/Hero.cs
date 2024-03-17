using UnityEngine;

public abstract class Hero : MonoBehaviour
{

    [field: Header("General Variables")]
    [field: SerializeField] public Animator heroAnimator { get; protected set; }
    public bool HasHeroMoved { get; protected set; } = false;
    public SymbolTable SymbolTable { get; protected set; }

    [Header("Hero Attributes")]

    [SerializeField] protected float HP = 0.0f;

    [SerializeField] protected float damage = 0.0f;

    [SerializeField] protected float Defense = 0.0f;

    [Header("Tile and raycast")]

    protected Tile CurrentTile;
    protected RaycastHit2D raycastHit;

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
            heroAnimator.SetTrigger("Walk");

            if ((Vector2)transform.position == targetPositionInGrid && CurrentTile != null)
            {
                HasHeroMoved = true;
                CurrentTile = TileGetter.GetTile(targetPositionInGrid, out raycastHit);
                CurrentTile.OccupyTile(this.gameObject);
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

        heroAnimator.SetTrigger("Injured");

        if (HP == 0)
        {
            HP = 100f;

            gameObject.SetActive(false);
        }
    }

    public abstract void HeroAttackBoss(Boss boss);
    public abstract bool CanHeroAttack();
}




