using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackDamageToolTip : ToolTip
{
    [SerializeField] private Boss boss;
    [SerializeField] private HeroesManager heroesManager;
    [SerializeField] private TextMeshProUGUI heroDamageText;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        int damage = 0;

        foreach (Hero hero in heroesManager.heroList)
        {
            if (hero.HeroIsAlive && hero.AttackPosCondition(hero.CurrentTile))
            {
                damage += hero.SymbolTable.GetDamage();
            }
        }

        heroDamageText.text = "Damage: " + damage.ToString();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }
}
