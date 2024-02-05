using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BossRush inputActions;
    private Vector2 inputPosition;
    [SerializeField] private Charachter markedCharachter;
    [SerializeField] PlayerResourceManager playerResourceManager;
    private ActionCard markedCard;

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();

        inputActions.Player.PlayerPress.performed += OnPlayerPressOnBoard;
        inputActions.Player.PlayerPress.canceled += OnPlayerPressOnBoard;
    }

    private void OnDisable()
    {
        inputActions.Player.PlayerPress.performed -= OnPlayerPressOnBoard;
        inputActions.Player.PlayerPress.canceled -= OnPlayerPressOnBoard;
    }

    private void OnPlayerPressOnBoard(InputAction.CallbackContext inputAction)
    {
        inputPosition = inputAction.ReadValue<Vector2>();
        

        if (inputAction.performed)
        {
            SelectCard();

            if (markedCharachter != null)
                MoveCharachter();
            else
                MarkCharachter();
        }
    }

    private void MoveCharachter()
    {
        if (markedCharachter != null)
        {
            Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

            if (raycast && raycast.collider.CompareTag("Tile"))
            {
                Debug.Log("Started Movemenet");

                Tile tile = raycast.collider.GetComponent<Tile>();
                raycast.point = tile.tilePosition;
                playerResourceManager.UseActionCard(markedCard);
                markedCharachter.MoveCharchterToPosition(raycast.point);

                markedCharachter = null;
                markedCard = null;

                Debug.Log("Ended Movemenet");
            }
        }
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

        if (raycast && raycast.collider.CompareTag("Charachter") && markedCharachter == null)
        {
            if (markedCard != null)
            {
                Debug.Log("Started Mark");

                markedCharachter = raycast.collider.GetComponent<Charachter>();

                Debug.Log("Ended Mark");
                Debug.Log(markedCharachter.name.ToString());
            }
        }
    }

    private void SelectCard()
    {
        Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

        if (raycast && raycast.collider.CompareTag("Card"))
        {
            markedCard = raycast.collider.GetComponent<ActionCard>();
        }
    }
}