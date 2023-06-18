using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private int totalMoney = 0;

    private void Awake()
    {
        Instance = this;
        
        if (!PlayerPrefs.HasKey("TotalMoney"))
        {
            PlayerPrefs.SetInt("TotalMoney", totalMoney);
        }
        else
        {
            totalMoney = PlayerPrefs.GetInt("TotalMoney", 0);
        }
    }

    private void Start()
    {
        UIManager.Instance.gamePanelCanvas.SetMoneyValueText(totalMoney);
    }

    public void SetTotalMoneyValue(int value)
    {
        totalMoney += value;
        UIManager.Instance.gamePanelCanvas.SetMoneyValueText(totalMoney);
    }

    public int GetTotalMoneyValue()
    {
        return totalMoney;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            PlayerPrefs.SetInt("TotalMoney", totalMoney);
        }
    }
}
