using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] private Charachter charachter;
    private BossRush inputActions;
    private Vector2 inputPosition;

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
            MoveCharachter();
        }
    }

    private void MoveCharachter()
    {
        Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

        if (raycast && raycast.collider.CompareTag("Tile"))
        {
            charachter.MoveCharchterToPosition(raycast.point);
        }
    }
}
