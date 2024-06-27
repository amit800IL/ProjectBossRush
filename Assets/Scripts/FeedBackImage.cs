using System.Collections;
using UnityEngine;

public class FeedBackImage : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private SpriteRenderer floatingImage;
    private Vector3 initialFeedbackImagePosition;

    private Coroutine feedbackImageCoroutine;

    private void Start()
    {
        initialFeedbackImagePosition = floatingImage.transform.position;
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
            if (feedbackImageCoroutine != null)
            {
                StopCoroutine(feedbackImageCoroutine);
            }

            feedbackImageCoroutine = StartCoroutine(FeedbackImageFloating());
        }
    }

    private IEnumerator FeedbackImageFloating()
    {
        floatingImage.gameObject.SetActive(true);

        Vector3 floatingPosition = new Vector3(0, 1, 0);

        float timerMax = 3f;
        float timeLapse = 0f;

        Color currentColor = floatingImage.color;
        currentColor.a = 1f;
        floatingImage.color = currentColor;

        Color targetTransparency = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

        while (timeLapse < timerMax)
        {
            timeLapse += Time.deltaTime;
            float progress = timeLapse / timerMax;
            floatingImage.transform.position = Vector3.Lerp(initialFeedbackImagePosition, initialFeedbackImagePosition + floatingPosition, progress);
            floatingImage.color = Color.Lerp(currentColor, targetTransparency, progress);
            yield return null;
        }

        floatingImage.gameObject.SetActive(false);
    }
}
