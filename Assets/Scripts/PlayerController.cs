using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BossRush inputActions;
    private Vector2 inputPosition;
    private bool charachterMarked = false;
    private Charachter markedCharachter;
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
            //SelectCard();

            if (charachterMarked)
                MoveCharachter();
            else
                MarkCharachter();
        }
    }

    private void MoveCharachter()
    {
        if (charachterMarked && markedCharachter != null)
        {
            Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

            if (raycast && raycast.collider.CompareTag("Tile"))
            {
                Tile tile = raycast.collider.GetComponent<Tile>();
                raycast.point = tile.tilePosition;
                //playerResourceManager.UseActionCard(markedCard);
                markedCharachter.MoveCharchterToPosition(raycast.point);

                charachterMarked = false;
                markedCharachter = null;
            }
        }
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

        if (raycast && raycast.collider.CompareTag("Charachter") && markedCharachter == null)
        {
            charachterMarked = true;
            markedCharachter = raycast.collider.GetComponent<Charachter>();
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
