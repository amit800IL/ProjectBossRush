using UnityEngine;

public abstract class PlayerAction : MonoBehaviour
{
    public bool HasPlayerDoneAction { get; protected set; } = false;

    [Header("Input system")]
    protected InputManager inputManager;
    protected Vector2 inputPosition;

    [Header("Raycast mark flags")]

    protected bool heroMarked = false;
    protected bool cardMarked = false;

    [Header("Game Objects")]

    protected Hero markedHero;

    protected virtual void Start()
    {
        inputManager = InputManager.Instance;
    }

    protected void MarkCharachter()
    {
        Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D raycast = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.HeroMask);

        if (raycast && (inputManager.HeroMask.value & (1 << raycast.collider.gameObject.layer)) != 0)
        {
            heroMarked = true;
            markedHero = raycast.collider.GetComponent<Hero>();
        }
    }

    protected bool IsCharachterOnTile()
    {
        Vector2 pressPosition = inputManager.MainCamera.ScreenToWorldPoint(inputPosition);

        RaycastHit2D charachterRaycastCheck = Physics2D.Raycast(pressPosition, Vector2.zero, Mathf.Infinity, inputManager.HeroMask);

        return charachterRaycastCheck;
    }

    protected abstract void ResetMarkProccess(RaycastHit2D raycast);

    public void PlayerRestart()
    {
        HasPlayerDoneAction = false;
    }
}
