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

    private void Awake()
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

        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        if (inputAction.performed)
        {
            if (heroMarked)
                MoveHeroToTile(pressPosition);
            else
                MarkHero(pressPosition);
        }
    }

    private void MoveHeroToTile(Vector2 pressPosition)
    {
        if (markedHero != null && heroMarked)
        {
            markedTile = TileGetter.GetTile(pressPosition, out raycastHit);

            if (CanStepOnTile())
            {
                markedHero.MoveHeroToPosition(markedTile.tilePosition);
                ResetMarkProccess();
            }
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

    private void ResetMarkProccess()
    {
        heroMarked = false;
        markedHero.ResetHeroMovement();
        markedHero = null;
        markedTile = null;
    }

    private void MarkHero(Vector2 pressPosition)
    {
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

    public void ResetMarkProccessButton()
    {
        if ((heroMarked && markedHero != null))
        {
            heroMarked = false;
            markedHero = null;
        }
    }
}
