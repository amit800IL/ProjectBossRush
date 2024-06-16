using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("General UI")]
    private int roundNumber = 0;
    [SerializeField] private Canvas winScreen;
    [SerializeField] private Canvas lostScreen;
    [SerializeField] private TextMeshProUGUI roundUI;
    [SerializeField] private HeroUI heroUI;

    [Header("Boss UI")]

    [SerializeField] private Image bossHealthBar;

    [Header("AP UI")]

    [SerializeField] private List<Image> actionPoints = new List<Image>();
    [SerializeField] private Sprite apSpriteOn;
    [SerializeField] private Sprite apSpriteOff;

    private void Awake()
    {
        Boss.OnEnemyHealthChanged += BossHealthChange;
        PlayerResourceManager.OnAPChanged += ApUIChange;
        TurnsManager.OnPlayerTurnStart += RoundNumberChange;
        HeroesManager.OnHeroesDeath += ShowLostScreen;
        Boss.OnBossDeath += ShowWinScreen;
    }

    private void OnDestroy()
    {
        Boss.OnEnemyHealthChanged -= BossHealthChange;
        PlayerResourceManager.OnAPChanged -= ApUIChange;
        TurnsManager.OnPlayerTurnStart -= RoundNumberChange;
        HeroesManager.OnHeroesDeath -= ShowLostScreen;
        Boss.OnBossDeath -= ShowWinScreen;
    }

    private void ApUIChange(int ap)
    {
        for (int i = 0; i < actionPoints.Count; i++)
        {
            if (i >= ap)
            {
                actionPoints[i].sprite = apSpriteOff;
            }
            else
            {
                actionPoints[i].sprite = apSpriteOn;
            }
        }
    }

    private void RoundNumberChange()
    {
        roundNumber++;
        roundUI.text = "Round: " + roundNumber.ToString();
    }

    private void BossHealthChange(Boss boss)
    {
        bossHealthBar.fillAmount = (float)boss.HP / boss.maxHP;
    }

    private void ShowWinScreen()
    {
        winScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ShowLostScreen()
    {
        lostScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartScene()
    {
        if (winScreen.gameObject.activeInHierarchy)
        {
            winScreen.gameObject.SetActive(false);
        }

        if (lostScreen.gameObject.activeInHierarchy)
        {
            lostScreen.gameObject.SetActive(false);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
