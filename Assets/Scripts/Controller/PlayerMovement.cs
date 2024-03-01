using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerAction
{
    protected override void OnPlayerPressOnBoard(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

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
            Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

            RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.TileMask);

            CharchterRaycastTileMovement(raycast);
        }
    }

    private void CharchterRaycastTileMovement(RaycastHit2D raycast)
    {
        bool isCharachterOnTile = IsCharachterOnTile();

        if (raycast && !isCharachterOnTile && (inputManager.TileMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            Tile tile = raycast.collider.GetComponent<Tile>();
            raycast.point = tile.tilePosition;
            markedCharachter.MoveHeroToPosition(raycast.point);
            ResetMarkProccess(raycast);
        }
    }
}
