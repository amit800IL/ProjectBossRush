using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ActionPointsNumber;
    [SerializeField] private TextMeshProUGUI bossHealthText;

    private void Start()
    {
        PlayerResourceManager.OnAPChanged += ApTextChange;
        Boss.OnEnemyHealthChanged += BossHealthChange;
    }

    private void ApTextChange(int obj)
    {
        ActionPointsNumber.text = "AP : " + obj.ToString();
    }

    private void BossHealthChange(int bossHealth)
    {
        bossHealthText.text = "Boss HP : " + bossHealth.ToString();
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
