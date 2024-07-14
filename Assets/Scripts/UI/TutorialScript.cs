using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialScript : MonoBehaviour
{
    public static event Action OnTutorialFinished;

    const string PAGE_NUMBER_TEXT = "Click anywhere to continue";

    [SerializeField] private List<GameObject> tutorialPages;
    [SerializeField] private bool playTutorial = true;
    [SerializeField] private TextMeshProUGUI pageNumberText;

    int currentPage = 0;

    private void OnEnable()
    {
        StartTutorial();
    }

    private void Start()
    {
        if (!playTutorial)
        {
            FinishTutorial();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutorialPages[currentPage].SetActive(false);
            if (currentPage < tutorialPages.Count - 1)
            {
                currentPage++;
                tutorialPages[currentPage].SetActive(true);
                UpdatePageNum();
            }
            else
            {
                FinishTutorial();
            }
        }
    }

    public void StartTutorial()
    {
        currentPage = 0;
        tutorialPages[0].SetActive(true);
        UpdatePageNum();
    }

    private void UpdatePageNum()
    {
        pageNumberText.text = $"{currentPage + 1}/{tutorialPages.Count}\n" + PAGE_NUMBER_TEXT;
    }

    private void FinishTutorial()
    {
        OnTutorialFinished?.Invoke();
        gameObject.SetActive(false);
    }
}
