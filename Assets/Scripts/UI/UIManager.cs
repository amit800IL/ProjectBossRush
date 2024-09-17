using System.Collections;
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
    [SerializeField] private List<ReviveHeroButton> reviveButtonsList;
    [SerializeField] private List<HeroUI> heroUIList;
    [SerializeField] private GameObject tacticalViewText;

    [Header("Boss UI")]

    [SerializeField] private Image bossHealthBar;

    [Header("AP UI")]

    [SerializeField] private List<Image> actionPoints = new List<Image>();
    private List<Image> showAP = new List<Image>();
    [SerializeField] private Sprite apSpriteOn;
    [SerializeField] private Sprite apSpriteOff;
    private Coroutine showAPCoroutine;
    private void Awake()
    {
        Boss.OnEnemyHealthChanged += BossHealthChange;
        TurnsManager.OnPlayerTurnStart += RoundNumberChange;
        TurnsManager.OnPlayerTurnStart += ResetShowAP;
        TurnsManager.OnPlayerTurnStart += NoticePlayerTurn;
        TurnsManager.OnBossTurnStart += NoticeBossTurn;
        HeroesManager.OnHeroesDeath += ShowLostScreen;
        Boss.OnBossDeath += ShowWinScreen;
        Hero.OnHeroSpawned += AssignHeroUI;
        Hero.OnHeroSpawned += AssignHeroRevive;
        PlayerController.OnTacticalViewToggled += ToggleTacticalStateText;
        PlayerResourceManager.OnAPChanged += ApUIChange;
        PlayerResourceManager.OnAPShow += ApUIShow;
        PlayerResourceManager.OnAPStopShow += ApUIStopShow;

        ResetShowAP();
    }

    private void OnDestroy()
    {
        Boss.OnEnemyHealthChanged -= BossHealthChange;
        PlayerResourceManager.OnAPChanged -= ApUIChange;
        PlayerResourceManager.OnAPStopShow -= ApUIStopShow;
        TurnsManager.OnPlayerTurnStart -= ResetShowAP;
        PlayerResourceManager.OnAPShow -= ApUIShow;
        TurnsManager.OnPlayerTurnStart -= RoundNumberChange;
        TurnsManager.OnPlayerTurnStart -= NoticePlayerTurn;
        TurnsManager.OnBossTurnStart -= NoticeBossTurn;
        HeroesManager.OnHeroesDeath -= ShowLostScreen;
        Boss.OnBossDeath -= ShowWinScreen;
        Hero.OnHeroSpawned -= AssignHeroUI;
        Hero.OnHeroSpawned -= AssignHeroRevive;
        PlayerController.OnTacticalViewToggled -= ToggleTacticalStateText;
    }

    private void ResetShowAP()
    {
        showAP.Clear();

        showAP.AddRange(actionPoints);
    }

    private void ApUIChange(int ap)
    {
        if (showAPCoroutine != null)
        {
            StopCoroutine(showAPCoroutine);
        }

        for (int i = 0; i < actionPoints.Count; i++)
        {
            if (i >= ap)
            {
                actionPoints[i].sprite = apSpriteOff;
                showAP.Remove(actionPoints[i]);
            }
            else
            {
                actionPoints[i].sprite = apSpriteOn;
            }
        }
    }

    public void ApUIShow(int ap)
    {
        if (showAPCoroutine != null)
        {
            StopCoroutine(showAPCoroutine);
        }

        showAPCoroutine = StartCoroutine(APShow(ap));
    }

    private IEnumerator APShow(int ap)
    {
        float timerMax = 0.1f;

        bool shouldShowAP = true;

        while (shouldShowAP)
        {
            for (int i = 0; i < showAP.Count; i++)
            {
                if (i >= ap)
                {
                    showAP[i].sprite = apSpriteOn;
                }
            }

            yield return ChageAPColor(timerMax, ap);
        }
    }

    private IEnumerator ChageAPColor(float timerMax, float ap)
    {
        float timeLapse = 0f;

        while (timeLapse < timerMax)
        {
            timeLapse += Time.deltaTime;
            float progress = timeLapse / timerMax;

            for (int i = 0; i < showAP.Count; i++)
            {
                if (i >= ap)
                {
                    Color currentColor = showAP[i].color;
                    showAP[i].color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(0f, 1f, progress));
                }

                yield return null;
            }

            yield return null;
        }

        timeLapse = 0f;

        while (timeLapse < timerMax)
        {
            timeLapse += Time.deltaTime;
            float progress = timeLapse / timerMax;

            for (int i = 0; i < showAP.Count; i++)
            {
                if (i >= ap)
                {
                    Color currentColor = showAP[i].color;
                    showAP[i].color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(1f, 0f, progress));
                }

                yield return null;
            }

            yield return null;
        }
    }

    public void ApUIStopShow(int ap)
    {
        if (showAPCoroutine != null)
        {
            StopCoroutine(showAPCoroutine);
        }

        for (int i = 0; i < actionPoints.Count; i++)
        {
            if (i < ap)
            {
                Color apColor = actionPoints[i].color;
                apColor.a = 1f;
                actionPoints[i].color = apColor;
                actionPoints[i].sprite = apSpriteOn;
            }
        }
    }

    public void AssignHeroRevive(Hero hero)
    {
        for (int i = 0; i < reviveButtonsList.Count; i++)
        {
            if (reviveButtonsList[i].AssignHero(hero))
            {
                return;
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
        roundUI.text = "Round " + roundNumber.ToString();
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
