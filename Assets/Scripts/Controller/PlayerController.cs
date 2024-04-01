using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action OnHeroMarked;

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

        TileGetter.GetTileFromCamera(inputPosition, mainCamera, out raycastHit);

        if (inputAction.performed)
        {
            if (heroMarked)
                MoveHeroToTile(inputPosition);
            else
                MarkHero(inputPosition);
        }
    }

    private void MoveHeroToTile(Vector3 pressPosition)
    {
        if (markedHero != null && heroMarked)
        {
            markedTile = TileGetter.GetTileFromCamera(pressPosition, mainCamera, out raycastHit);

            if (CanStepOnTile())
            {
                if (markedHero.CurrentTile != null)
                {
                    markedHero.CurrentTile.ClearTile();
                }

                markedHero.MoveHeroToPosition(markedTile);
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
        Ray ray = Camera.main.ScreenPointToRay(pressPosition);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, heroMask);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 5f);

        Debug.Log("Player has been hit : " + raycast);

        if (raycast)
        {
            heroMarked = true;
            markedHero = raycastHit.collider.GetComponent<Hero>();
            OnHeroMarked.Invoke();
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