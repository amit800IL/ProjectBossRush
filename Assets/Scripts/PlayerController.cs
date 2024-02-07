using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BossRush inputActions;
    private Vector2 inputPosition;
    private bool charachterMarked = false;
    private bool cardMarked = false;
    private Charachter markedCharachter;
    private ActionCard markedCard;
    [SerializeField] PlayerResourceManager playerResourceManager;

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
            Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

            if (raycast && raycast.collider.CompareTag("Tile"))
            {
                Tile tile = raycast.collider.GetComponent<Tile>();
                raycast.point = tile.tilePosition;
                markedCharachter.MoveCharchterToPosition(raycast.point);
                playerResourceManager.UseActionCard(markedCard);

                Debug.Log(markedCharachter.transform.position);

                if ((Vector2)markedCharachter.transform.position == raycast.point)
                {
                    charachterMarked = false;
                    cardMarked = false;
                    markedCharachter = null;
                }
            }
        }
    }

    private void MarkCharachter()
    {
        if (cardMarked)
        {
            Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

            RaycastHit2D[] charchtersToMark = Physics2D.RaycastAll(pressPosition, Vector2.zero);

            foreach (RaycastHit2D raycast in charchtersToMark)
            {
                if (raycast && raycast.collider.CompareTag("Charachter"))
                {
                    charachterMarked = true;
                    markedCharachter = raycast.collider.GetComponent<Charachter>();

                    Debug.Log("Charchter selected: " + markedCharachter.gameObject.name);
                    break;
                }
            }
        }
    }

    private void SelectCard()
    {
        Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

        if (raycast && raycast.collider.CompareTag("Card"))
        {
            cardMarked = true;
            markedCard = raycast.collider.GetComponent<ActionCard>();
        }
    }
}
