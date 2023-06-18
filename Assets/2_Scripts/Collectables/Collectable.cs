using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Collectable : MonoBehaviour
{
    public CollectableFly CollectableFly { get; set; }
    public DOTweenAnimation scaleAnim { get; private set; }
    
    public CollectableScriptable collectableData;
    
    [SerializeField] private Pools.Types poolType;

    public bool isCollected;
    
    private void Awake()
    {
        CollectableFly = GetComponent<CollectableFly>();
        scaleAnim = GetComponent<DOTweenAnimation>();
    }

    public void CallCollectAnimaiton(Transform collectPoint = null, float ?yPosOfCollectable = null)
    {
        isCollected = true;
        CollectableFly.MoveBehaviour(this, collectPoint, yPosOfCollectable);
    }
    
    public void Despawn()
    {
        isCollected = false;
        PoolManager.Instance.Despawn(poolType, gameObject);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            collectableData.SaveSoldAmount();
        }
    }
}
