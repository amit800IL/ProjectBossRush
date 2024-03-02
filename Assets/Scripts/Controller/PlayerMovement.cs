using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerAction
{
    protected override void Start()
    {
        base.Start();
        inputManager.InputActions.Player.PlayerPress.performed += OnPlayerMove;
    }

    private void OnDisable()
    {
        inputManager.InputActions.Player.PlayerPress.performed -= OnPlayerMove;
    }

    private void OnPlayerMove(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

        if (inputAction.performed)
        {
            if (heroMarked)
                MoveCharachter();
            else
                MarkCharachter();
        }
    }
    private void CharchterRaycastTileMovement(RaycastHit2D raycast)
    {
        bool isCharachterOnTile = IsCharachterOnTile();

        if (raycast && !isCharachterOnTile && (inputManager.TileMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            Tile tile = raycast.collider.GetComponent<Tile>();
            raycast.point = tile.tilePosition;
            markedHero.MoveHeroToPosition(raycast.point);
            ResetMarkProccess(raycast);
        }

    }

    private void MoveCharachter()
    {
        if (markedHero != null && heroMarked)
        {
            Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.TileMask);

            CharchterRaycastTileMovement(raycast);
        }
    }

    protected override void ResetMarkProccess(RaycastHit2D raycast)
    {
        if ((Vector2)markedHero.transform.position == raycast.point)
        {
            heroMarked = false;
            markedHero = null;
            cardMarked = false;
            HasPlayerDoneAction = true;
        }
    }

}
