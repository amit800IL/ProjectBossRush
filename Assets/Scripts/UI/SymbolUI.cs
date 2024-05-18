using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SymbolUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] symbolText;

    public void UpdateUI(string text)
    {
        for (int i = 0; i < SymbolTable.SYMBOL_TYPE_COUNT; i++)
        {
            symbolText[i].text = text[i].ToString();
        }
    }
}
