using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameButton : MonoBehaviour
{
    bool hasButtonBeenPressed = false;

    [SerializeField] private Button heroButton;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite hoverSprite;

    public void ButtonPressState()
    {
        if (!hasButtonBeenPressed)
        {
            heroButton.image.sprite = onSprite;

            hasButtonBeenPressed = true;
        }
        else
        {
            heroButton.image.sprite = offSprite;

            hasButtonBeenPressed = false;
        }
    }

    public void ButtonOnHoverStart()
    {
        heroButton.image.sprite = hoverSprite;
    }

    public void ButtonOnHoverFinish()
    {
        if (hasButtonBeenPressed)
        {
            heroButton.image.sprite = onSprite;
        }
        else
        {
            heroButton.image.sprite = offSprite;
        }
    }
}
