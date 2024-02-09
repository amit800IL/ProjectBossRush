using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BossRush inputActions;
    private Vector2 inputPosition;
    private bool charachterMarked = false;
    private Charachter markedCharachter;

    PlayerResourceManager manager;
    [SerializeField] LayerMask cards;

    private void Start()
    {
        inputActions = new BossRush();
        inputActions.Enable();

        inputActions.Player.PlayerPress.performed += OnPlayerPressOnBoard;
        inputActions.Player.PlayerPress.canceled += OnPlayerPressOnBoard;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.transform.localPosition.y/2f));
            //Physics.Raycast(ray, out RaycastHit hit, 100);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            print("shoot");
            if (hit.collider != null)
            {
                print("hit");
                manager.UseActionCard(hit.collider.GetComponent<ActionCard>());
            }
        }
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
        if (charachterMarked && markedCharachter != null)
        {
            Vector2 pressPosition = Camera.main.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

            if (raycast && raycast.collider.CompareTag("Tile"))
            {
                Tile tile = raycast.collider.GetComponent<Tile>();
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

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero);

        if (raycast && raycast.collider.CompareTag("Charachter") && !charachterMarked)
        {
            charachterMarked = true;
            markedCharachter = raycast.collider.GetComponent<Charachter>();
        }
    }
}