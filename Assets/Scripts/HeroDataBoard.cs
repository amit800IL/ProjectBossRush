using TMPro;
using UnityEngine;

public class HeroDataBoard : MonoBehaviour
{
    [SerializeField] private HeroUI heroUI;
    [SerializeField] private TextMeshProUGUI heroMovementText;

    private void Start()
    {
        ShowHeroMovementAmount();
    }


    private void ShowHeroMovementAmount()
    {
        if (heroUI.Hero != null)
            heroMovementText.text = heroUI.Hero.HeroData.maxMovementAmount.ToString();
    }
}
