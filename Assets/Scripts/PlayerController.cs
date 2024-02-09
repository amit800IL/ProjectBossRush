using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("General Variables")]

    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;
    private int movementAmount;

    [Header("Input system")]

    private BossRush inputActions;
    private Vector2 inputPosition;

    [Header("Raycast mark flags")]

    private bool charachterMarked = false;
    private bool cardMarked = false;
    private bool hasCharachterMoved = false;

    [Header("Game Objects")]

    private Charachter markedCharachter;
    private ActionCard actionCard;

    [Header("LayerMask")]

    private LayerMask charchterMask;
    private LayerMask tileMask;
    private LayerMask cardLayer;

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();

        inputActions.Player.PlayerPress.performed += OnPlayerPressOnBoard;
        inputActions.Player.PlayerPress.canceled += OnPlayerPressOnBoard;

        charchterMask = LayerMask.GetMask("Charachter");
        tileMask = LayerMask.GetMask("Tile");
        cardLayer = LayerMask.GetMask("Card");

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
            movementAmount = playerResourceManager.GetMovementAmount();

            if (charachterMarked)
                MoveCharachter();
            else
                MarkCharachter();
        }
    }

    private void MoveCharachter()
    {
        if (markedCharachter != null && movementAmount > 0)
        {
            Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, tileMask);

            CharchterRaycastTileMovement(raycast);
        }
    }

    private void MarkCharachter()
    {
        if (movementAmount > 0)
        {
            Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, charchterMask);

            if (raycast && (charchterMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
            {
                charachterMarked = true;
                markedCharachter = raycast.collider.GetComponent<Charachter>();

                Debug.Log("Charchter selected: " + markedCharachter.gameObject.name);
            }

        }
    }
    public void SelectCard()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, cardLayer);

        if (raycast && (cardLayer.value & (1 << raycast.collider.gameObject.layer)) != 0 && !cardMarked)
        {
            actionCard = raycast.collider.GetComponent<ActionCard>();
            playerResourceManager.UseActionCard(actionCard);
            actionCard.gameObject.SetActive(false);
            cardMarked = true;
        }
    }

    private void CharchterRaycastTileMovement(RaycastHit2D raycast)
    {
        if (raycast && (tileMask.value & (1 << raycast.collider.gameObject.layer)) != 0 && movementAmount > 0)
        {
            Tile tile = raycast.collider.GetComponent<Tile>();
            raycast.point = tile.tilePosition;
            markedCharachter.MoveCharchterToPosition(raycast.point);
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
}
