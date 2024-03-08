using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI ActionPointsNumber;

    private void Start()
    {
        PlayerResourceManager.OnAPChanged += ApTextChange;
    }

    private void ApTextChange(int obj)
    {
       ActionPointsNumber.text = "AP : " + obj.ToString();
    }
}
