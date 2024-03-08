using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool HasPlayerDoneAction { get; protected set; } = false;

    [Header("General variables")]
    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;

    [Header("Input system")]
    private BossRush inputActions;
    private Vector2 inputPosition;

    [Header("Raycast mark flags")]

    private bool heroMarked = false;
    private bool cardMarked = false;

    [Header("Game Objects")]

    private Hero markedHero;
    [SerializeField] private Boss boss;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask heroMask;
    [SerializeField] private LayerMask tileMask;
    [SerializeField] private LayerMask bossMask;

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
    private void CharchterRaycastTileMovement(RaycastHit2D raycast)
    {
        bool isCharachterOnTile = IsCharachterOnTile();

        if (raycast && !isCharachterOnTile && (tileMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            Tile tile = raycast.collider.GetComponent<Tile>();
            raycast.point = tile.tilePosition;
            markedHero.MoveHeroToPosition(raycast.point);

            if (markedHero.HasHeroMoved)
            {
                if (playerResourceManager.UseAP(1))
                {
                    ResetMarkProccess();
                }
            }
        }

    }

    private void MoveCharachter()
    {
        if (markedHero != null && heroMarked)
        {
            Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, tileMask);

            CharchterRaycastTileMovement(raycast);
        }
    }

    private void ResetMarkProccess()
    {
        heroMarked = false;
        markedHero.ResetHeroMovement();
        markedHero = null;
        cardMarked = false;
        HasPlayerDoneAction = true;
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, heroMask);

        if (raycast && (heroMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            heroMarked = true;
            markedHero = raycast.collider.GetComponent<Hero>();
        }
    }

    private bool IsCharachterOnTile()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D charachterRaycastCheck = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, heroMask);

        return charachterRaycastCheck;
    }

    public void PlayerRestart()
    {
        HasPlayerDoneAction = false;
    }

    public void PlayerAttack()
    {
        if (boss.IsBossAlive && playerResourceManager.UseAP(1))
        {
            heroesManager.AttackBoss();

            HasPlayerDoneAction = true;
        }
    }

}
