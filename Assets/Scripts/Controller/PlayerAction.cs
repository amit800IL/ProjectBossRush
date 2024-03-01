using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerAction : MonoBehaviour
{
    [Header("Input system")]
    protected InputManager inputManager;
    protected Vector2 inputPosition;

    [Header("Raycast mark flags")]

    protected bool charachterMarked = false;
    protected bool cardMarked = false;

    [Header("Game Objects")]

    protected Hero markedCharachter;

    protected void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.InputActions.Player.PlayerPress.performed += OnPlayerPressOnBoard;
    }

    protected void OnDisable()
    {
        inputManager.InputActions.Player.PlayerPress.performed -= OnPlayerPressOnBoard;
    }

    protected abstract void OnPlayerPressOnBoard(InputAction.CallbackContext inputAction);
    protected void MarkCharachter()
    {
        Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.HeroMask);

        if (raycast && (inputManager.HeroMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            charachterMarked = true;
            markedCharachter = raycast.collider.GetComponent<Hero>();
        }
    }

    protected bool IsCharachterOnTile()
    {
        Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D charachterRaycastCheck = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.HeroMask);

        return charachterRaycastCheck;
    }

    protected virtual void ResetMarkProccess(RaycastHit2D raycast)
    {
        if ((Vector2)markedCharachter.transform.position == raycast.point)
        {
            charachterMarked = false;
            markedCharachter = null;
            cardMarked = false;
        }
    }
}
