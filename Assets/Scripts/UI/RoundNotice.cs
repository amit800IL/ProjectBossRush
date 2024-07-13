using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundNotice : MonoBehaviour
{
    [SerializeField] private List<Graphic> graphics;
    [SerializeField] private TextMeshProUGUI noticeText;
    [SerializeField] private float fullOpacityDuration = 1;

    [SerializeField] float opacityIncrement = .02f;

    private void Start()
    {
        for (int i = 0; i < graphics.Count; i++)
        {
            graphics[i].color *= new Color(1, 1, 1, 0);
        }
    }

    public void ActivateNotice(string expression)
    {
        noticeText.text = expression;
        StartCoroutine(nameof(FadeIn));
    }

    private void SetColorOpacity(bool increase)
    {
        Color color = new(0, 0, 0, opacityIncrement);
        for (int i = 0; i < graphics.Count; i++)
        {
            if (increase) graphics[i].color += color;
            else graphics[i].color -= color;
        }
    }

    private IEnumerator FadeIn()
    {
        while (graphics[0].color.a < 1)
        {
            SetColorOpacity(increase: true);
            yield return null;
        }
        yield return new WaitForSeconds(fullOpacityDuration);
        StartCoroutine(nameof(FadeOut));
    }

    private IEnumerator FadeOut()
    {
        while (graphics[0].color.a > 0)
        {
            SetColorOpacity(increase: false);
            yield return null;
        }
    }
}
