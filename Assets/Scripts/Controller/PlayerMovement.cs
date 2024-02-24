using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("General Variables")]

    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;
    private int movementAmount;

    [Header("Input system")]
    private InputManager inputManager;
    private Vector2 inputPosition;

    [Header("Raycast mark flags")]

    private bool charachterMarked = false;
    private bool cardMarked = false;

    [Header("Game Objects")]

    private Hero markedCharachter;

    [Header("LayerMask")]

    [SerializeField] private LayerMask charchterMask;
    [SerializeField] private LayerMask tileMask;

    private void Start()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main;

        inputManager.InputActions.Player.PlayerPress.performed += OnPlayerPressOnBoard;
        inputManager.InputActions.Player.PlayerPress.canceled += OnPlayerPressOnBoard;
    }

    private void OnDisable()
    {
        inputManager.InputActions.Player.PlayerPress.performed -= OnPlayerPressOnBoard;
        inputManager.InputActions.Player.PlayerPress.canceled -= OnPlayerPressOnBoard;
    }

    private void OnPlayerPressOnBoard(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

        if (inputAction.performed)
        {
            movementAmount = playerResourceManager.GetMovementAmount();

            if (charachterMarked)
                MoveCharachter();
            else
                MarkCharachter();
        }
    }

    private void MoveCharachter()
    {
        if (markedCharachter != null)
        {
            Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, tileMask);

            CharchterRaycastTileMovement(raycast);
        }
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, charchterMask);

        if (raycast && (charchterMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            charachterMarked = true;
            markedCharachter = raycast.collider.GetComponent<Hero>();
        }
    }

    private void CharchterRaycastTileMovement(RaycastHit2D raycast)
    {
        bool isCharachterOnTile = IsCharachterOnTile();

        if (raycast && !isCharachterOnTile && (tileMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            Tile tile = raycast.collider.GetComponent<Tile>();
            raycast.point = tile.tilePosition;
            markedCharachter.MoveHeroToPosition(raycast.point);
            ResetMarkProccess(raycast);
        }
    }

    private void ResetMarkProccess(RaycastHit2D raycast)
    {
        if ((Vector2)markedCharachter.transform.position == raycast.point)
        {
            playerResourceManager.UseMovementResource();
            charachterMarked = false;
            markedCharachter = null;
            cardMarked = false;
        }
    }

    private bool IsCharachterOnTile()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D charachterRaycastCheck = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, charchterMask);

        return charachterRaycastCheck;
    }
}
