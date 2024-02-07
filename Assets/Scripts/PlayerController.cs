using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BossRush inputActions;
    private Vector2 inputPosition;
    private bool charachterMarked = false;
    private bool cardMarked = false;
    private Charachter markedCharachter;
    //[SerializeField] PlayerResourceManager playerResourceManager;

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

            if (raycast && raycast.collider.gameObject.CompareTag("Tile"))
            {
                Tile tile = raycast.collider.gameObject.GetComponent<Tile>();
                raycast.point = tile.tilePosition;
                markedCharachter.MoveCharchterToPosition(raycast.point);

                charachterMarked = false;
                markedCharachter = null;
            }

        }
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        RaycastHit2D[] charchtersToMark = Physics2D.RaycastAll(pressPosition, Vector2.zero);

        foreach (RaycastHit2D raycast in charchtersToMark)
        {
            if (raycast && raycast.collider.gameObject.CompareTag("Charachter"))
            {
                charachterMarked = true;
                markedCharachter = raycast.collider.gameObject.GetComponent<Charachter>();
                break;
            }
        }

    }

    //private void SelectCard()
    //{
    //    Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

    //    RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

    //    if (raycast && raycast.collider.CompareTag("Card"))
    //    {
    //        markedCard = raycast.collider.GetComponent<ActionCard>();
    //    }
    //}
}
