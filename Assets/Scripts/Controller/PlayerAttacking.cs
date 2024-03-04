using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : MonoBehaviour
{
    [Header("General Variables")]
    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Camera mainCamera;
    private int attackingAmount;

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

        RaycastHit2D raycastHit = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.BossMask);

        if (raycastHit)
        {
            Debug.Log("shoot & hit");
            ActionCard card = raycastHit.collider.GetComponent<ActionCard>();
            //playerResourceManager.UseActionCard(card);
        }
    }

    private void MarkCharachter()
    {
        Vector2 pressPosition = mainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.HeroMask);

        if (raycast && (inputManager.HeroMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            charachterMarked = true;
            markedCharachter = raycast.collider.GetComponent<Hero>();
        }
    }
}
