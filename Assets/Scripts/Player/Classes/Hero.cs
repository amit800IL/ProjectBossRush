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

    [field: Header("Tile and raycast")]

    protected Tile currentTile;
    public Tile CurrentTile { get => currentTile; set => currentTile = value; }

    protected RaycastHit raycastHit;

    protected virtual void Start()
    {
        //CurrentTile = TileGetter.GetTileAtPosition(this.gameObject, out currentTile);
        //CurrentTile.OccupyTile(this.gameObject);
    }

    public void MoveHeroToPosition(Tile targetTile)
    {
        currentTile = targetTile;
        transform.position = targetTile.OccupantContainer.position;
        heroAnimator.SetTrigger("Walk");

        if (transform.position == targetTile.OccupantContainer.position && CurrentTile != null)
        {
            HasHeroMoved = true;
            targetTile.OccupyTile(this.gameObject);
        }
    }

    public void ResetHeroMovement()
    {
        HasHeroMoved = false;
    }

    //private bool IsHeroInMoveRange(Vector3 newPosition)
    //{
    //    return OnOneTileRange(newPosition) && !OnDiagonalDirection(newPosition);
    //}

    //private bool OnOneTileRange(Vector3 newPosition)
    //{
    //    return Mathf.Abs(newPosition.x - transform.position.x) <= 1f &&
    //           Mathf.Abs(newPosition.y - transform.position.y) <= 1f &&
    //           Mathf.Abs(newPosition.z - transform.position.z) <= 1f;
    //}

    //private bool OnDiagonalDirection(Vector3 newPosition)
    //{
    //    return Mathf.Abs(newPosition.x - transform.position.x) == 1f &&
    //           Mathf.Abs(newPosition.y - transform.position.y) == 1f &&
    //           Mathf.Abs(newPosition.z - transform.position.z) == 1f;
    //}

    public void HealthDown()
    {
        HP -= 10f;
        Debug.Log("Hero " + name + " has been attacked" + ", Health : " + HP);

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




