using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static event Action<Hero> OnHeroMarked;
    public static event Action<bool> OnTacticalViewToggled;

    [Header("General variables")]
    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] DirectionIndicator directionIndicatorPrefab;
    [SerializeField] int movementAPCost = 1;
    [SerializeField] float timeDelayBetweenSteps = .5f;

    [Header("Input system")]
    private BossRush inputActions;
    private Vector2 inputPosition;
    private bool isInputAllowed = false;
    private bool isTacticalViewOn = false;

    private bool isTracingHeroRoute = false;
    private List<Tile> route = new();

    [Header("Raycast mark flags")]

    private RaycastHit raycastHit;
    private bool isheroMarked = false;

    [Header("Game Objects")]

    [SerializeField] private Hero markedHero;
    [SerializeField] private Hero hoveredHero;
    private Tile hoveredTile;
    private Tile markedTile;
    [SerializeField] private Boss boss;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask heroMask;
    [SerializeField] private LayerMask tileMask;

    private DirectionIndicator[] DIndicators = new DirectionIndicator[4];

    private void Awake()
    {
        TurnsManager.OnPlayerTurnStart += AllowInput;
        TurnsManager.OnBossTurnStart += ForbidInput;
    }

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();
        inputActions.Player.PlayerPress.performed += OnPrimarySelectPressed;
        inputActions.Player.PlayerPress.canceled += OnPrimarySelectReleased;
        inputActions.Player.PlayerCancel.performed += OnSecondarySelectPressed;
        inputActions.Player.PlayerTactical.started += OnTacticalInputPressed;
        inputActions.Player.PlayerTactical.canceled += OnTacticalInputReleased;
        inputActions.UI.Point.performed += OnPointerMove;

        InitDirectionIndicators();
    }

    private void OnDisable()
    {
        inputActions.Player.PlayerPress.performed -= OnPrimarySelectPressed;
        inputActions.Player.PlayerPress.canceled -= OnPrimarySelectReleased;
        inputActions.Player.PlayerCancel.performed -= OnSecondarySelectPressed;
        inputActions.Player.PlayerTactical.started -= OnTacticalInputPressed;
        inputActions.Player.PlayerTactical.canceled -= OnTacticalInputReleased;
        inputActions.UI.Point.performed -= OnPointerMove;
    }

    private void OnDestroy()
    {
        TurnsManager.OnPlayerTurnStart -= AllowInput;
        TurnsManager.OnBossTurnStart -= ForbidInput;
    }

    private void OnPrimarySelectPressed(InputAction.CallbackContext inputAction)
    {
        if (!isInputAllowed) return;

        inputPosition = Mouse.current.position.ReadValue();

        if (inputActions != null && inputPosition != null)
        {
            //TileGetter.GetTileFromCamera(inputPosition, mainCamera, out raycastHit); line is obsolete

            //if (isheroMarked)
            //    MoveHeroToTile(inputPosition);
            //else
            MarkHero(inputPosition);
        }
    }

    private void OnPrimarySelectReleased(InputAction.CallbackContext inputAction)
    {
        if (isTracingHeroRoute)
        {
            Debug.Log(route.Count);
            if (ValidateRoute())
            {
                Debug.Log("route valid");
                StartCoroutine(nameof(MoveHeroOnRoute));
            }
            else
            {
                route.Clear();
            }
        }

        isTracingHeroRoute = false;
        hoveredTile = null;
    }

    private void OnSecondarySelectPressed(InputAction.CallbackContext inputAction)
    {
        if (isTracingHeroRoute)
        {
            Debug.Log("route cancelled");
            isTracingHeroRoute = false;
            HideAllIndicators();
            route.Clear();
        }
    }

    private void OnTacticalInputPressed(InputAction.CallbackContext inputAction)
    {
        isTacticalViewOn = !isTacticalViewOn;
        if (isTacticalViewOn)
        {
            StartTacticalView();
        }
        else
        {
            StopTacticalView();
        }

        OnTacticalViewToggled?.Invoke(isTacticalViewOn);
    }

    private void OnTacticalInputReleased(InputAction.CallbackContext inputAction)
    {
    }


    private void OnPointerMove(InputAction.CallbackContext inputAction)
    {
        if (isTracingHeroRoute)
        {
            GetHoveredTile();
        }
        else
        {
            GetHoveredHero();
        }
    }

    private bool ValidateRoute()
    {
        if (playerResourceManager.HasEnoughAP(1))
        {
            if (markedHero.CanHeroMove(route.Count))
            {
                return true;
            }
            else Debug.Log("route not valid - route too long");
        }
        else Debug.Log("route not valid - not enough AP");
        return false;
        //return playerResourceManager.HasEnoughAP(1) && markedHero.CanHeroMove(route.Count);
    }

    private IEnumerator MoveHeroOnRoute()
    {
        playerResourceManager.UseAP(movementAPCost);
        for (int i = 0; i < route.Count; i++)
        {
            MoveToTile(route[i]);
            if (i != route.Count - 1)
            {
                yield return new WaitForSeconds(timeDelayBetweenSteps);
            }
        }
        HideAllIndicators();
        route.Clear();
    }

    private void MoveToTile(Tile tile)
    {
        markedHero.MoveHeroToPosition(tile);
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
        if (markedHero != null)
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
            isTracingHeroRoute = true;
            markedHero = raycastHit.collider.GetComponent<Hero>();
            hoveredTile = markedHero.CurrentTile;
            OnHeroMarked?.Invoke(markedHero);
        }
        else ResetMarkProccess();
    }

    public void GetHoveredHero()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, heroMask);

        if (raycast)
        {
            if (isTacticalViewOn)
            {
                hoveredHero = raycastHit.collider.GetComponent<Hero>();
                StartTacticalView();

            }
        }
        else
        {
            hoveredHero = null;
            StopTacticalView();
        }
    }

    public void GetHoveredTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, tileMask);

        if (raycast)
        {
            Tile _newTile = raycastHit.collider.GetComponent<Tile>();
            if (_newTile != null && _newTile != hoveredTile)
            {
                if (hoveredTile == null)
                {
                    hoveredTile = _newTile;
                }
                else if (markedHero.CanHeroMove(route.Count + 1) && _newTile.IsTileNeighboring(hoveredTile) && !_newTile.IsTileOccupied)
                {
                    route.Add(_newTile);
                    Debug.Log(_newTile.tilePosition + " Added");
                    DisplayDirectionIndicators(DIndicators[route.Count - 1], hoveredTile, _newTile);
                    hoveredTile = _newTile;
                }
            }
        }
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
            OnHeroMarked?.Invoke(markedHero);
        }
    }

    public void StartTacticalView()
    {
        GridManager.Instance.StartTacticalView(hoveredHero);
    }

    public void StopTacticalView()
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

    #region Visual Indicators
    private void InitDirectionIndicators()
    {
        for (int i = 0; i < DIndicators.Length; i++)
        {
            DIndicators[i] = Instantiate(directionIndicatorPrefab);
        }
    }

    private void DisplayDirectionIndicators(DirectionIndicator Indicator, Tile fromTile, Tile toTile)
    {
        Indicator.transform.position = fromTile.transform.position;
        Indicator.transform.LookAt(toTile.transform);
        Indicator.gameObject.SetActive(true);

    }

    private void HideIndicator(int i)
    {
        DIndicators[i].gameObject.SetActive(false);
    }

    private void HideAllIndicators()
    {
        for (int i = 0; i < DIndicators.Length; i++)
        {
            HideIndicator(i);
        }
    }


    #endregion
}
