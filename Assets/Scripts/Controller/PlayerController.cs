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
    [SerializeField] private HeroUI heroUI;

    [Header("Input system")]
    private BossRush inputActions;
    private Vector2 inputPosition;
    private bool isButtonHeld = false;

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



    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();
        inputActions.Player.PlayerPress.performed += OnPlayerMove;
        inputActions.Player.PlayerTactical.started += OnCharachterHovered;
        inputActions.Player.PlayerTactical.canceled += OnCharachterReleased;
        inputActions.UI.Point.performed += HoverCharachter;
    }
    private void OnDisable()
    {
        inputActions.Player.PlayerPress.performed -= OnPlayerMove;
        inputActions.Player.PlayerTactical.started -= OnCharachterHovered;
        inputActions.Player.PlayerTactical.canceled -= OnCharachterReleased;
        inputActions.UI.Point.performed -= HoverCharachter;
    }

    private void OnPlayerMove(InputAction.CallbackContext inputAction)
    {
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

            int movementAPCost = 1;

            if (CanHeroUnlockMovement() && playerResourceManager.HasEnoughAP(movementAPCost))
            {
                markedHero.UnlockHeroMovement();
            }

            float movementCost = HeroMovementCost();

            if (CanStepOnTile() && markedHero.CanHeroMove((int)movementCost) && movementCost > 0)
            {
                markedHero.HeroMovemetAmountReduction((int)movementCost);

                Debug.Log("Hero " + gameObject.name + "Movement cost : " + movementCost);

                markedHero.MoveHeroToPosition(markedTile);

                if (markedHero.IsHeroOnNewPosition)
                {
                    playerResourceManager.UseAP(movementAPCost);
                }

                ResetMarkProccess();
            }
        }
    }

    public float HeroMovementCost()
    {
        if (markedHero == null || markedTile == null)
            return 0;

        float distanceX = Mathf.Abs(markedTile.tilePosition.x - markedHero.CurrentTile.tilePosition.x);
        float distanceY = Mathf.Abs(markedTile.tilePosition.y - markedHero.CurrentTile.tilePosition.y);
        float movementCost = distanceX + distanceY;

        return movementCost;
    }

    private bool CanStepOnTile()
    {
        return markedTile != null && !markedTile.IsTileOccupied && markedHero.HasHeroMoved;
    }

    private bool CanHeroUnlockMovement()
    {
        return !markedHero.HasHeroMoved && markedTile != null && !markedTile.IsTileOccupied;
    }

    private void ResetMarkProccess()
    {
        isheroMarked = false;
        markedHero.ResetHeroMovement();
        markedHero = null;
        heroUI.AssignHero(markedHero);
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
            heroUI.AssignHero(markedHero);
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
            heroesManager.CommandAttack();
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
            heroUI.AssignHero(markedHero);
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
}