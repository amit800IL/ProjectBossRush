using System.Collections;
using TMPro;
using UnityEngine;

public class FeedBackText : MonoBehaviour
{
    [SerializeField] private Hero hero;

    [Header("Defence Feedback")]

    [SerializeField] private TextMeshProUGUI defenceFloatingText;
    [SerializeField] private Transform initialDefenceFloatingTextPosition;
    private Coroutine defenceUpFeedbackCoroutine;

    [Header("Damage Feedback")]

    [SerializeField] private TextMeshProUGUI damageFloatingText;
    private Vector3 initialDamageFloatingTextPosition;
    private Coroutine damageFeedbackCoroutine;
    private float previousHealth;

    private void Start()
    {
        initialDamageFloatingTextPosition = damageFloatingText.transform.position;
        defenceFloatingText.transform.position = initialDefenceFloatingTextPosition.position;
        previousHealth = hero.HP;
        Hero.OnHeroHealthChanged += OnPlayerHit;
        Hero.OnHeroDefend += OnPlayerDefenceUp;
    }

    private void OnDisable()
    {
        Hero.OnHeroHealthChanged -= OnPlayerHit;
        Hero.OnHeroDefend -= OnPlayerDefenceUp;
    }

    private void OnPlayerHit(Hero hero)
    {
        if (this.hero == hero)
        {
            float damage = previousHealth - hero.HP;
            previousHealth = hero.HP;
            string subtractionSymbol = "-";

            if (damageFeedbackCoroutine != null)
            {
                StopCoroutine(damageFeedbackCoroutine);
            }

            damageFeedbackCoroutine = StartCoroutine(FloatingFeedbackNumber(damageFloatingText, subtractionSymbol, damage, initialDamageFloatingTextPosition));
        }
    }

    private void OnPlayerDefenceUp(Hero hero)
    {
        if (this.hero == hero)
        {
            float defenceValue = hero.HeroData.defense;
            string additionSymbol = "+";

            if (defenceUpFeedbackCoroutine != null)
            {
                StopCoroutine(defenceUpFeedbackCoroutine);
            }

            defenceUpFeedbackCoroutine = StartCoroutine(FloatingFeedbackNumber(defenceFloatingText, additionSymbol, defenceValue, initialDefenceFloatingTextPosition.position));
        }
    }

    private IEnumerator FloatingFeedbackNumber(TextMeshProUGUI feedBackText, string symbol, float feedBackNumber, Vector3 originalObjectPosition)
    {
        feedBackText.text = symbol + feedBackNumber.ToString();

        feedBackText.gameObject.SetActive(true);

        Vector3 floatingPosition = new Vector3(0, 1, 0);

        float timerMax = 3f;
        float timeLapse = 0f;

        Color currentColor = feedBackText.color;
        currentColor.a = 1f;
        feedBackText.color = currentColor;

        Color targetTransparency = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

        while (timeLapse < timerMax)
        {
            timeLapse += Time.deltaTime;
            float progress = timeLapse / timerMax;
            feedBackText.transform.position = Vector3.Lerp(originalObjectPosition, originalObjectPosition + floatingPosition, progress);
            feedBackText.color = Color.Lerp(currentColor, targetTransparency, progress);
            yield return null;
        }

        feedBackText.gameObject.SetActive(false);
    }
}
