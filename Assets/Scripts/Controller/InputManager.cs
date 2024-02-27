using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public BossRush InputActions { get; private set; }
    [field: SerializeField] public LayerMask HeroMask { get; private set; }
    [field: SerializeField] public LayerMask TileMask { get; private set; }
    [field: SerializeField] public LayerMask BossMask { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        InputActions = new BossRush();
        InputActions.Enable();
    }


}
