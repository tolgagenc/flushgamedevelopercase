using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GamePanelCanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    public void SetMoneyValueText(int value)
    {
        moneyText.text = value.ToString();
    }
}
