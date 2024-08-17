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
    [SerializeField] private RoundNotice turnNotice;
    [SerializeField] private GameObject raycastBlockPanel;
    [SerializeField] private List<HeroUI> heroUIList;
    [SerializeField] private GameObject tacticalViewText;

    [Header("Boss UI")]

    [SerializeField] private Image bossHealthBar;

    [Header("AP UI")]

    [SerializeField] private List<Image> actionPoints = new List<Image>();
    [SerializeField] private Sprite apSpriteOn;
    [SerializeField] private Sprite apSpriteOff;
    private void Awake()
    {
        Boss.OnEnemyHealthChanged += BossHealthChange;
        TurnsManager.OnPlayerTurnStart += RoundNumberChange;
        TurnsManager.OnPlayerTurnStart += NoticePlayerTurn;
        TurnsManager.OnBossTurnStart += NoticeBossTurn;
        HeroesManager.OnHeroesDeath += ShowLostScreen;
        Boss.OnBossDeath += ShowWinScreen;
        Hero.OnHeroSpawned += AssignHeroUI;
        PlayerController.OnTacticalViewToggled += ToggleTacticalStateText;
        PlayerResourceManager.OnAPChanged += ApUIChange;
        PlayerResourceManager.OnAPShow += ApUIShow;
        PlayerResourceManager.OnAPStopShow += ApUIStopShow;
    }

    private void OnDestroy()
    {
        Boss.OnEnemyHealthChanged -= BossHealthChange;
        PlayerResourceManager.OnAPChanged -= ApUIChange;
        PlayerResourceManager.OnAPStopShow -= ApUIStopShow;
        PlayerResourceManager.OnAPShow -= ApUIShow;
        TurnsManager.OnPlayerTurnStart -= RoundNumberChange;
        TurnsManager.OnPlayerTurnStart -= NoticePlayerTurn;
        TurnsManager.OnBossTurnStart -= NoticeBossTurn;
        HeroesManager.OnHeroesDeath -= ShowLostScreen;
        Boss.OnBossDeath -= ShowWinScreen;
        Hero.OnHeroSpawned -= AssignHeroUI;
        PlayerController.OnTacticalViewToggled -= ToggleTacticalStateText;
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

    public void ApUIShow(int ap)
    {
        for (int i = 0; i < actionPoints.Count; i++)
        {
            if (i >= ap)
            {
                actionPoints[i].sprite = apSpriteOff;
            }
        }

    }
    public void ApUIStopShow(int ap)
    {
        for (int i = 0; i < actionPoints.Count; i++)
        {
            if (i < ap)
            {
                actionPoints[i].sprite = apSpriteOn;
            }
        }
    }
    public void AssignHeroUI(Hero hero)
    {
        for (int i = 0; i < heroUIList.Count; i++)
        {
            if (heroUIList[i].AssignHero(hero))
            {
                return;
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

    private void NoticePlayerTurn()
    {
        turnNotice.ActivateNotice("Player Turn");
        raycastBlockPanel.SetActive(false);
    }

    private void NoticeBossTurn()
    {
        turnNotice.ActivateNotice("Boss Turn");
        raycastBlockPanel.SetActive(true);
    }

    private void ToggleTacticalStateText(bool state)
    {
        tacticalViewText.SetActive(state);
    }
}
