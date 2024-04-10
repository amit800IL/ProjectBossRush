using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heroHPText;
    [SerializeField] private TextMeshProUGUI heroDefenceText;

    private void Start()
    {
        Hero.OnHeroHealthChanged += HeroHealthChange;
        Hero.OnHeroDefenceChanged += HeroDefenceChange;
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
