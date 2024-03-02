using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : PlayerAction
{
    [SerializeField] private Boss boss;

    protected override void Start()
    {
        base.Start();
        inputManager.InputActions.Player.PlayerPress.performed += OnPlayerAttack;
    }

    private void OnDisable()
    {
        inputManager.InputActions.Player.PlayerPress.performed -= OnPlayerAttack;
    }

    private void OnPlayerAttack(InputAction.CallbackContext inputAction)
    {
        inputPosition = Mouse.current.position.ReadValue();

        if (inputAction.performed)
        {
            if (heroMarked)
                PlayerAttack();
            else
                MarkCharachter();
        }
    }

    private void PlayerAttack()
    {
        Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycastHit = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.BossMask);

        if (markedHero != null && heroMarked && raycastHit)
        {
            //ActionCard card = raycastHit.collider.GetComponent<ActionCard>();
            //inputManager.PlayerResourceManager.UseActionCard(card);

            boss.TakeDamage(10);

            ResetMarkProccess(raycastHit);
        }
    }

    protected override void ResetMarkProccess(RaycastHit2D raycast)
    {
        heroMarked = false;
        markedHero = null;
        cardMarked = false;
        HasPlayerDoneAction = true;
    }
}
