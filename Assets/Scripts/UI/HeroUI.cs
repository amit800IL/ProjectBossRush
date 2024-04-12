using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heroHPText;
    [SerializeField] private TextMeshProUGUI heroDefenceText;
    [SerializeField] private Transform canvasFollowTarget;
    [SerializeField] private Canvas heroUICanvas;
    [SerializeField] private Vector3 offset;


    private void Start()
    {
        heroUICanvas.worldCamera = Camera.main;

        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;

    }
    void Update()
    {
        transform.position = canvasFollowTarget.position + offset;
    }

    private void HeroHealthChange(int heroHealth)
    {
        heroHPText.text = "HP: " + heroHealth.ToString();
    }

    private void HeroDefenceChange(int heroDefence)
    {
        heroDefenceText.text = "Defence: " + heroDefence.ToString();
    }
}
