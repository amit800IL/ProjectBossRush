using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static event Action<Hero> OnHeroMarked;

    [Header("General variables")]
    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;

    [Header("Input system")]
    private BossRush inputActions;
    private Vector2 inputPosition;
    private bool isButtonHeld = false;
    private bool isInputAllowed = false;

    [Header("Raycast mark flags")]

    private RaycastHit raycastHit;
    private bool isheroMarked = false;
    private bool isheroHovered = false;

    [Header("Game Objects")]

    [SerializeField] private Hero markedHero;
    [SerializeField] private Hero hoveredHero;
    private Tile markedTile;
    [SerializeField] private Boss boss;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask heroMask;

    private void Awake()
    {
        TurnsManager.OnPlayerTurnStart += AllowInput;
        TurnsManager.OnBossTurnStart += ForbidInput;        
    }

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();
        inputActions.Player.PlayerPress.performed += HeroMark;
        inputActions.Player.PlayerCancel.performed += OnHeroUnMarked;
        inputActions.Player.PlayerTactical.started += OnCharachterHovered;
        inputActions.Player.PlayerTactical.canceled += OnCharachterReleased;
        inputActions.UI.Point.performed += HoverCharachter;
    }

    private void OnDisable()
    {
        inputActions.Player.PlayerPress.performed -= HeroMark;
        inputActions.Player.PlayerCancel.performed -= OnHeroUnMarked;
        inputActions.Player.PlayerTactical.started -= OnCharachterHovered;
        inputActions.Player.PlayerTactical.canceled -= OnCharachterReleased;
        inputActions.UI.Point.performed -= HoverCharachter;
    }

    private void OnDestroy()
    {
        TurnsManager.OnPlayerTurnStart -= AllowInput;
        TurnsManager.OnBossTurnStart -= ForbidInput;        
    }

    private void HeroMark(InputAction.CallbackContext inputAction)
    {
        if (!isInputAllowed) return;

        inputPosition = Mouse.current.position.ReadValue();

        if (inputActions != null && inputPosition != null)
        {
            TileGetter.GetTileFromCamera(inputPosition, mainCamera, out raycastHit);

            if (inputAction.performed)
            {
                if (isheroMarked)
                    MoveHeroToTile(inputPosition);
                else
                    MarkHero(inputPosition);
            }
        }
    }

    private void OnHeroUnMarked(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

        if (inputActions != null && inputPosition != null)
        {
            if (inputAction.performed)
            {
                if (isheroMarked)
                    ResetMarkProccess();
            }
        }
    }

    private void OnCharachterHovered(InputAction.CallbackContext inputAction)
    {
        isButtonHeld = true;

        if (isheroHovered)
        {
            TacticalViewPressed();
        }
    }

    private void OnCharachterReleased(InputAction.CallbackContext inputAction)
    {
        isButtonHeld = false;
        TacticalViewReleased();
    }


    private void HoverCharachter(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

        if (inputAction.performed)
        {
            HoverHero(inputPosition);
        }
    }

    private void MoveHeroToTile(Vector3 pressPosition)
    {
        if (markedHero != null && isheroMarked)
        {
            markedTile = TileGetter.GetTileFromCamera(pressPosition, mainCamera, out raycastHit);

            int APCost = 1;

            if (CanHeroUnlockMovement(APCost))
            {
                markedHero.UnlockHeroMovement();
            }

            int movementAmount = (int)HeroMovementAmount();

            if (CanStepOnTile(movementAmount))
            {
                MoveHeroToTile(movementAmount, APCost);
            }
        }
    }

    private void MoveHeroToTile(int movementAmount, int APCost)
    {
        markedHero.HeroMovemetAmountReduction(movementAmount);

        Debug.Log("Hero " + gameObject.name + "Movement cost : " + movementAmount);

        markedHero.MoveHeroToPosition(markedTile);

        if (markedHero.IsHeroOnNewPosition)
        {
            playerResourceManager.UseAP(APCost);
        }

        ResetMarkProccess();
    }

    public float HeroMovementAmount()
    {
        if (markedHero == null || markedTile == null)
            return 0;

        float distanceX = Mathf.Abs(markedTile.tilePosition.x - markedHero.CurrentTile.tilePosition.x);
        float distanceY = Mathf.Abs(markedTile.tilePosition.y - markedHero.CurrentTile.tilePosition.y);
        float movementCost = distanceX + distanceY;

        return movementCost;
    }

    private bool CanStepOnTile(int movementCost)
    {
        return markedTile != null && !markedTile.IsTileOccupied && markedHero.HasHeroUnlockedMovement && markedHero.CanHeroMove(movementCost);
    }

    private bool CanHeroUnlockMovement(int movementAPCost)
    {
        return !markedHero.HasHeroUnlockedMovement && markedTile != null && !markedTile.IsTileOccupied && playerResourceManager.HasEnoughAP(movementAPCost);
    }

    private void ResetMarkProccess()
    {
        isheroMarked = false;
        markedHero.ResetHeroMovement();
        markedHero = null;
        OnHeroMarked?.Invoke(markedHero);
        markedTile = null;
    }

    private void MarkHero(Vector3 pressPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(pressPosition);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, heroMask);

        if (raycast)
        {
            isheroMarked = true;
            markedHero = raycastHit.collider.GetComponent<Hero>();
            OnHeroMarked?.Invoke(markedHero);
        }
    }

    public void HoverHero(Vector3 hoverPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(hoverPosition);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, heroMask);

        if (raycast)
        {
            isheroHovered = true;
            hoveredHero = raycastHit.collider.GetComponent<Hero>();

            if (isButtonHeld)
            {
                TacticalViewPressed();
            }
        }
        else
        {
            TacticalViewReleased();
            UnHoverHero();
        }
    }

    private void UnHoverHero()
    {
        isheroHovered = false;
        hoveredHero = null;
    }
    public void PlayerAttack()
    {
        if (boss.IsBossAlive && playerResourceManager.UseAP(2))
        {
           heroesManager.StartCoroutine(heroesManager.CommandAttack());
        }
    }

    public void PlayerDefend()
    {
        if (boss.IsBossAlive && playerResourceManager.UseAP(1))
        {
            heroesManager.CommandDefend();
        }
    }
    public void ResetMarkProccessButton()
    {
        if ((isheroMarked && markedHero != null))
        {
            isheroMarked = false;
            markedHero = null;
            OnHeroMarked?.Invoke(markedHero);
        }
    }

    public void TacticalViewPressed()
    {
        GridManager.Instance.StartTacticalView(hoveredHero);
    }
    public void TacticalViewReleased()
    {
        GridManager.Instance.StopTacticalView();
    }

    private void AllowInput()
    {
        isInputAllowed = true;
    }

    private void ForbidInput()
    {
        isInputAllowed = false;
    }
}