using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CollectableDataManager : MonoBehaviour
{
    public List<CollectableScriptable> collectableDataHolders;

    private void Awake()
    {
        foreach (var item in collectableDataHolders)
        {
            if (!PlayerPrefs.HasKey("Total" + item.gemName + "SoldAmount"))
                PlayerPrefs.SetInt("Total" + item.gemName + "SoldAmount", item.GetTotalSoldAmount());
            else
                item.SetTotalSoldAmount(PlayerPrefs.GetInt("Total" + item.gemName + "SoldAmount"));
            
            Debug.Log(item.GetTotalSoldAmount());
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            foreach (var item in collectableDataHolders)
            {
                item.SaveSoldAmount();
            }
        }
    }
}
