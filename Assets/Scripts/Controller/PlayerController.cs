using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General variables")]
    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;

    [Header("Input system")]
    private BossRush inputActions;
    private Vector2 inputPosition;

    [Header("Raycast mark flags")]

    private RaycastHit2D raycastHit;
    private bool heroMarked = false;

    [Header("Game Objects")]

    private Hero markedHero;
    private Tile markedTile;
    [SerializeField] private Boss boss;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask heroMask;

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();
        inputActions.Player.PlayerPress.performed += OnPlayerMove;
    }

    private void OnDisable()
    {
        inputActions.Player.PlayerPress.performed -= OnPlayerMove;
    }

    private void OnPlayerMove(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

        if (inputAction.performed)
        {
            if (heroMarked)
                MoveCharachter();
            else
                MarkCharachter();
        }
    }
    private void CharchterRaycastTileMovement()
    {
        if (CanStepOnTile())
        {
            if (markedHero.CurrentTile != null)
            {
                markedHero.CurrentTile.ClearTile();
            }

            markedTile.OccupyTile(markedHero.gameObject);
            markedHero.MoveHeroToPosition(markedTile.tilePosition);
            ResetMarkProccess();
        }
    }

    private bool CanStepOnTile()
    {
        return TileChecks() && PlayerChecks();
    }

    private bool TileChecks()
    {
        return markedTile != null && !markedTile.IsTileOccupied;
    }

    private bool PlayerChecks()
    {
        return !markedHero.HasHeroMoved && playerResourceManager.UseAP(1);
    }

    private void MoveCharachter()
    {
        if (markedHero != null && heroMarked)
        {
            Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

            markedTile = TileGetter.GetTile(pressPosition, out raycastHit);

            CharchterRaycastTileMovement();
        }
    }

    private void ResetMarkProccess()
    {
        heroMarked = false;
        markedHero.ResetHeroMovement();
        markedHero = null;
        markedTile = null;
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        raycastHit = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, heroMask);

        if (raycastHit && (heroMask.value & (1 << raycastHit.collider.gameObject.layer)) != 0)
        {
            heroMarked = true;
            markedHero = raycastHit.collider.GetComponent<Hero>();
        }
    }

    public void PlayerAttack()
    {
        if (boss.IsBossAlive && playerResourceManager.UseAP(1))
        {
            heroesManager.AttackBoss();
        }
    }

}
