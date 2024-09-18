using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected GameObject toolTip;
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.SetActive(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }
}
