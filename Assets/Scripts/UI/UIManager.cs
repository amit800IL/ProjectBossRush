using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("General UI")]
    private int roundNumber = 1;
    [SerializeField] private TextMeshProUGUI roundUI;
    [SerializeField] private HeroUI[] heroUI;
    private int assignedheroes = 0;

    [Header("Boss UI")]

    [SerializeField] private TextMeshProUGUI bossHealthText;

    [Header("AP UI")]

    [SerializeField] private List<Image> actionPoints = new List<Image>();
    [SerializeField] private Sprite apSpriteOn;
    [SerializeField] private Sprite apSpriteOff;

    private void Awake()
    {
        Hero.OnHeroSpawned += AssignHeroToUI;
        Boss.OnEnemyHealthChanged += BossHealthChange;
        PlayerResourceManager.OnAPChanged += ApUIChange;
        TurnsManager.OnPlayerTurnStart += RoundNumberChange;
    }

    private void OnDestroy()
    {
        Hero.OnHeroSpawned -= AssignHeroToUI;
        Boss.OnEnemyHealthChanged -= BossHealthChange;
        PlayerResourceManager.OnAPChanged -= ApUIChange;
        TurnsManager.OnPlayerTurnStart -= RoundNumberChange;
    }

    private void AssignHeroToUI(Hero hero)
    {
        heroUI[assignedheroes].AssignHero(hero);
        assignedheroes++;
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

    private void BossHealthChange(int bossHealth)
    {
        bossHealthText.text = "Boss HP : " + bossHealth.ToString();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
