using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SymbolUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI symbolText;

    public void UpdateUI(string text)
    {
        symbolText.text = text;
    }
}
