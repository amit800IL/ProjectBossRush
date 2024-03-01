using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : PlayerAction
{
    protected override void OnPlayerPressOnBoard(InputAction.CallbackContext inputAction)
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
        Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycastHit = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.BossMask);

        if (raycastHit)
        {
            Debug.Log("shoot & hit");
            ActionCard card = raycastHit.collider.GetComponent<ActionCard>();
            inputManager.PlayerResourceManager.UseActionCard(card);
        }
    }

}
