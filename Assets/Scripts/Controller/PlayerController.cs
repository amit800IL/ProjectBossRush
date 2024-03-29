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

    private RaycastHit raycastHit;
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

        Vector3 pressPosition = mainCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, mainCamera.nearClipPlane));

        if (inputAction.performed)
        {
            if (heroMarked)
                MoveHeroToTile(pressPosition);
            else
                MarkHero(pressPosition);
        }
    }

    private void MoveHeroToTile(Vector3 pressPosition)
    {
        if (markedHero != null && heroMarked)
        {
            markedTile = TileGetter.GetTileFromCamera(pressPosition, out raycastHit);

            if (CanStepOnTile())
            {
                if (markedHero.CurrentTile != null)
                {
                    markedHero.CurrentTile.ClearTile();
                }

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

    private void MarkHero(Vector3 pressPosition)
    {
        Vector3 rayDirection = Quaternion.Euler(14, 0, 0) * Vector3.forward;

        Ray ray = new Ray(pressPosition, rayDirection);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, heroMask);

        Debug.DrawRay(pressPosition, rayDirection * 10, Color.blue, 5f);

        Debug.Log(raycast);

        if (raycast)
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