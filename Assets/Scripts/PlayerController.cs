using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Variables")]

    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;

    [Header("Input system")]

    private BossRush inputActions;
    private Vector2 inputPosition;

    [Header("Raycast mark flags")]

    private bool charachterMarked = false;
    private bool cardMarked = false;

    [Header("Game Objects")]

    private Charachter markedCharachter;
    private ActionCard actionCard;

    [Header("LayerMask")]

    private LayerMask charchterMask;
    private LayerMask tileMask;

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();

        inputActions.Player.PlayerPress.performed += OnPlayerPressOnBoard;
        inputActions.Player.PlayerPress.canceled += OnPlayerPressOnBoard;

        charchterMask = LayerMask.GetMask("Charachter");
        tileMask = LayerMask.GetMask("Tile");

        mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        inputActions.Player.PlayerPress.performed -= OnPlayerPressOnBoard;
        inputActions.Player.PlayerPress.canceled -= OnPlayerPressOnBoard;
    }

    private void OnPlayerPressOnBoard(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

        if (inputAction.performed)
        {
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
        if (cardMarked)
        {
            Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, charchterMask);

            if (raycast && raycast.collider.IsTouchingLayers(charchterMask))
            {
                charachterMarked = true;
                markedCharachter = raycast.collider.GetComponent<Charachter>();

                Debug.Log("Charchter selected: " + markedCharachter.gameObject.name);
            }

        }
    }
    public void SelectCard()
    {
        if (!cardMarked)
        {
            cardMarked = true;
            actionCard = FindObjectOfType<ActionCard>();
        }
    }

    private void CharchterRaycastTileMovement(RaycastHit2D raycast)
    {
        int movementAmount = playerResourceManager.GetMovementAmount();

        if (raycast && raycast.collider.IsTouchingLayers(tileMask) && movementAmount > 0)
        {
            Tile tile = raycast.collider.GetComponent<Tile>();
            raycast.point = tile.tilePosition;
            markedCharachter.MoveCharchterToPosition(raycast.point);
            ResetMarkProccess(raycast);
        }
        else
        {
            Debug.Log("You can't move anymore");
        }
    }

    private void ResetMarkProccess(RaycastHit2D raycast)
    {
        if ((Vector2)markedCharachter.transform.position == raycast.point)
        {
            playerResourceManager.UseActionCard(actionCard);
            charachterMarked = false;
            cardMarked = false;
            markedCharachter = null;
            actionCard = null;
        }
    }
}