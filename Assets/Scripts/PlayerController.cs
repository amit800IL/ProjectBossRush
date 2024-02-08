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
    private ActionCard markedCard;

    [Header("LayerMask")]

    private LayerMask charchterMask;
    private LayerMask tileMask;
    private LayerMask cardMask;

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();

        inputActions.Player.PlayerPress.performed += OnPlayerPressOnBoard;
        inputActions.Player.PlayerPress.canceled += OnPlayerPressOnBoard;

        charchterMask = LayerMask.GetMask("Charachter");
        tileMask = LayerMask.GetMask("Tile");
        cardMask = LayerMask.GetMask("Card");

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
            if (!cardMarked) 
                SelectCard();

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

            int movementAmount = playerResourceManager.GetMovementAmount();

            if (raycast && raycast.collider.IsTouchingLayers(tileMask) && movementAmount > 0)
            {
                Tile tile = raycast.collider.GetComponent<Tile>();
                raycast.point = tile.tilePosition;
                markedCharachter.MoveCharchterToPosition(raycast.point);
                playerResourceManager.UseActionCard(markedCard);

                charachterMarked = false;
                cardMarked = false;
                markedCharachter = null;
            }
            else
            {
                Debug.Log("You can't move anymore");
            }
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
    private void SelectCard()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

        if (raycast && raycast.collider.CompareTag("Card"))
        {
            cardMarked = true;
            markedCard = raycast.collider.GetComponent<ActionCard>();
        }
    }
}
