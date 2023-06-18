using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FarmSpawnController : MonoBehaviour
{
    private GameObject gem;
    private Transform gemTransform;
    
    private Vector3 initialScale;
    
    private Collectable collectable;

    void Start()
    {
        gem = PoolManager.Instance.Spawn((Pools.Types) Random.Range(0, 3), transform);
        
        collectable = gem.GetComponent<Collectable>();
        gemTransform = gem.transform;
        initialScale = gemTransform.localScale;
        
        gemTransform.localPosition = new Vector3(0, 10, 0);
        gemTransform.localScale = Vector3.zero;

        collectable.scaleAnim.endValueV3 = initialScale;
        collectable.scaleAnim.DOPlay();
    }

    void FixedUpdate()
    {
        if (collectable.isCollected)
        {
            collectable.isCollected = false;
            SpawnNewGem();
        }
    }


    private void SpawnNewGem()
    {
        gem = PoolManager.Instance.Spawn((Pools.Types) Random.Range(0, 3), transform);
        collectable = gem.GetComponent<Collectable>();
        gemTransform = gem.transform;
        initialScale = gemTransform.localScale;
        gemTransform.localPosition = new Vector3(0, 10, 0);
        gemTransform.localScale = Vector3.zero;
        
        collectable.scaleAnim.endValueV3 = initialScale;
        collectable.scaleAnim.DOPlay();
    }

    public Collectable HarvestGem()
    {
        if (gemTransform.lossyScale.x >= 0.25)
        {
            collectable.scaleAnim.DOPause();
            return collectable;
        }

        return null;
    } 
}
