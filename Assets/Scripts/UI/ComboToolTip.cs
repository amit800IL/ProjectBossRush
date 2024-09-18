using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComboToolTip : ToolTip
{
    [SerializeField] protected PlayerResourceManager resourceManager;
    [SerializeField] protected TechniqueDataSO techniqueDataSO;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        resourceManager.ShowApUse(techniqueDataSO.APCost);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        resourceManager.StopShowApUse(); 
    }
}
