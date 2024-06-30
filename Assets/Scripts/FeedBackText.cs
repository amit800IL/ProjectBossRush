using System.Collections;
using TMPro;
using UnityEngine;

public class FeedBackText : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private TextMeshProUGUI floatingText;
    private Vector3 initialFeedbackImagePosition;
    private Coroutine feedbackImageCoroutine;
    private float previousHealth;

    private void Start()
    {
        initialFeedbackImagePosition = floatingText.transform.position;
        previousHealth = hero.HP;
        Hero.OnHeroHealthChanged += OnPlayerHit;
    }

    private void OnDisable()
    {
        Hero.OnHeroHealthChanged -= OnPlayerHit;
    }

    private void OnPlayerHit(Hero hero)
    {
        if (this.hero == hero)
        {
            float damage = previousHealth - hero.HP;
            previousHealth = hero.HP;

            if (feedbackImageCoroutine != null)
            {
                StopCoroutine(feedbackImageCoroutine);
            }

            feedbackImageCoroutine = StartCoroutine(FloatingEnemyDamageNumber(damage));
        }
    }

    private IEnumerator FloatingEnemyDamageNumber(float damage)
    {
        floatingText.text = "-" + damage.ToString();

        floatingText.gameObject.SetActive(true);

        Vector3 floatingPosition = new Vector3(0, 1, 0);

        float timerMax = 3f;
        float timeLapse = 0f;

        Color currentColor = floatingText.color;
        currentColor.a = 1f;
        floatingText.color = currentColor;

        Color targetTransparency = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

        while (timeLapse < timerMax)
        {
            timeLapse += Time.deltaTime;
            float progress = timeLapse / timerMax;
            floatingText.transform.position = Vector3.Lerp(initialFeedbackImagePosition, initialFeedbackImagePosition + floatingPosition, progress);
            floatingText.color = Color.Lerp(currentColor, targetTransparency, progress);
            yield return null;
        }

        floatingText.gameObject.SetActive(false);
    }
}
