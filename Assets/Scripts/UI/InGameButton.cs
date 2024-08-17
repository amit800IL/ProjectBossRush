using UnityEngine;
using UnityEngine.UI;

public class InGameButton : MonoBehaviour
{
    bool hasButtonBeenPressed = false;

    [SerializeField] private Button heroButton;
    [SerializeField] private Sprite clickedSprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;

    public void ButtonPressState()
    {
        if (heroButton.interactable)
        {
            if (!hasButtonBeenPressed)
            {
                heroButton.image.sprite = clickedSprite;

                hasButtonBeenPressed = true;
            }
            else
            {
                heroButton.image.sprite = normalSprite;

                hasButtonBeenPressed = false;
            }
        }
    }

    public void ButtonOnHoverStart()
    {
        if (heroButton.interactable)
        {
            heroButton.image.sprite = hoverSprite;
        }
    }

    public void ButtonOnHoverFinish()
    {
        if (heroButton.interactable)
        {
            heroButton.image.sprite = normalSprite;
        }
    }
}
