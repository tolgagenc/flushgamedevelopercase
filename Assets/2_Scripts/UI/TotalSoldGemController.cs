using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalSoldGemController : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private CollectableDataManager collectableDataManager;
    [SerializeField] private GameObject totalSoldElement;

    private bool isFirstOpen = true;

    public void OnPanelOpen()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
            
        foreach (var item in collectableDataManager.collectableDataHolders)
        {
            var element = Instantiate(totalSoldElement, content);
            var elementController = element.GetComponent<TotalSoldGemElementController>();

            elementController.gemIcon.sprite = item.gemIcon;
            elementController.gemType.text = "Gem Type: " + item.gemName;
            elementController.totalCount.text = "Total Count: " + item.GetTotalSoldAmount();
        }
    }
}
