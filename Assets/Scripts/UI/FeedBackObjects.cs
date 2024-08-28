using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedBackObjects : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private Transform initialFloatingTextPosition;
    [SerializeField] private Transform initialArrowPosition;

    [Header("Defence Feedback")]

    [SerializeField] private TextMeshProUGUI defenceFloatingText;
    private Coroutine defenceUpFeedbackCoroutine;

    [Header("Damage Feedback")]

    [SerializeField] private TextMeshProUGUI damageFloatingText;
    private Coroutine damageFeedbackCoroutine;
    private float previousHealth;

    [Header("Mark Arrow")]
    [SerializeField] private Image arrowSprite;
    private Coroutine arrowSpriteCoroutine;

    private void Start()
    {
        arrowSprite.transform.position = initialArrowPosition.position;
        damageFloatingText.transform.position = initialFloatingTextPosition.position;
        defenceFloatingText.transform.position = initialFloatingTextPosition.position;
        previousHealth = hero.HP;

        PlayerController.OnHeroMarked += OnHeroMark;
        Hero.OnHeroWalk += OnHeroWalk;
        Hero.OnHeroHealthChanged += OnPlayerHit;
        Hero.OnHeroDefend += OnPlayerDefenceUp;
    }

    private void OnDisable()
    {
        PlayerController.OnHeroMarked -= OnHeroMark;
        Hero.OnHeroHealthChanged -= OnPlayerHit;
        Hero.OnHeroWalk -= OnHeroWalk;
        Hero.OnHeroDefend -= OnPlayerDefenceUp;
    }

    private void OnHeroMark(Hero hero)
    {
        ResetArrowCoroutine();

        if (this.hero == hero)
        {
            arrowSpriteCoroutine = StartCoroutine(FloatingArrow(arrowSprite.transform.position));
        }
        else
        {
            ResetArrowCoroutine();
        }
    }

    private void ResetArrowCoroutine()
    {
        if (arrowSpriteCoroutine != null)
        {
            arrowSprite.gameObject.SetActive(false);
            StopCoroutine(arrowSpriteCoroutine);
        }
    }

    private void OnHeroWalk(Hero hero)
    {
        if (this.hero == hero)
        {
            ResetArrowCoroutine();
        }
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

            damageFeedbackCoroutine = StartCoroutine(FloatingFeedbackNumber(damageFloatingText, subtractionSymbol, damage, initialFloatingTextPosition.position));
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

            defenceUpFeedbackCoroutine = StartCoroutine(FloatingFeedbackNumber(defenceFloatingText, additionSymbol, defenceValue, initialFloatingTextPosition.position));
        }
    }

    private IEnumerator FloatingFeedbackNumber(TextMeshProUGUI feedBackText, string symbol, float feedBackNumber, Vector3 originalObjectPosition)
    {
        feedBackText.text = symbol + feedBackNumber.ToString();

        if (feedBackNumber <= 0)
        {
            feedBackText.text = " ";
        }

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

    private IEnumerator FloatingArrow(Vector3 originalObjectPosition)
    {
        arrowSprite.gameObject.SetActive(true);

        bool isArrowFloating = true;

        while (isArrowFloating)
        {
            Vector3 floatingPosition = new Vector3(0, 0.2f, 0);

            float timerMax = 1f;
            float timeLapse = 0f;

            while (timeLapse < timerMax)
            {
                timeLapse += Time.deltaTime;
                float progress = timeLapse / timerMax;
                arrowSprite.transform.position = Vector3.Lerp(originalObjectPosition, originalObjectPosition + floatingPosition, progress);
                yield return null;
            }

            //arrowSprite.transform.position = originalObjectPosition;

            timeLapse = 0f;

            while (timeLapse < timerMax)
            {
                timeLapse += Time.deltaTime;
                float progress = timeLapse / timerMax;
                arrowSprite.transform.position = Vector3.Lerp(originalObjectPosition, originalObjectPosition - floatingPosition, progress);
                yield return null;
            }

            //arrowSprite.transform.position = originalObjectPosition;

            yield return null;
        }
    }
}
