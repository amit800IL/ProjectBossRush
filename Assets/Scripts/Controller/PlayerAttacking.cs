using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : MonoBehaviour
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
            if (charachterMarked)
                PlayerAttack();
            else
                MarkCharachter();
        }
    }

    private void PlayerAttack()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D hit = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity);

        Debug.Log("shoot");

        if (hit.collider != null)
        {
            Debug.Log("hit");
            ActionCard card = hit.collider.GetComponent<ActionCard>();
            playerResourceManager.UseActionCard(card);
        }
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.charachterMask);

        if (raycast && (inputManager.charachterMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            charachterMarked = true;
            markedCharachter = raycast.collider.GetComponent<Hero>();
        }
    }
}
