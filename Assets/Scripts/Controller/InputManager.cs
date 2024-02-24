using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public BossRush InputActions { get; private set; }
    [field: SerializeField] public LayerMask charachterMask { get; private set; }
    [field: SerializeField] public LayerMask tileMask { get; private set; }

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
