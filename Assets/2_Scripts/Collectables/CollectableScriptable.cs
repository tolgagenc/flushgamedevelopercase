using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GemData", menuName = "GemData/GemDataHolder")]
public class CollectableScriptable : ScriptableObject
{
    public string gemName;
    public int startingPrice;
    public Sprite gemIcon;
    public GameObject gemModel;
    private int totalSoldAmount = 0;

    public void SetTotalSoldAmount(int value)
    {
        totalSoldAmount = value;
    }

    public void IncreaseTotalSoldAmount()
    {
        totalSoldAmount++;
    }

    public int GetTotalSoldAmount()
    {
        return totalSoldAmount;
    }

    public void SaveSoldAmount()
    {
        PlayerPrefs.SetInt("Total" + gemName + "SoldAmount", totalSoldAmount);
    }
}
