using TMPro;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject arrowGameObject;

    public void OnButtonHoveringEnter()
    {
        float buttonTextAlpha = (buttonText.color.a * 3);

        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, buttonTextAlpha);

        arrowGameObject.SetActive(true);
    }

    public void OnButtonHoveringExit()
    {
        float buttonTextAlpha = buttonText.color.a / 3;

        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, buttonTextAlpha);

        arrowGameObject.SetActive(false);
    }
}
