using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackController : MonoBehaviour
{
    [SerializeField] private Transform stackPoint;
    private Transform destinationTransform;
    private Stack<Collectable> collectedGems = new Stack<Collectable>();
    private float newGemYPos = 0;
    Collectable gem;

    private IEnumerator SellGems(Transform sellPointPosition = null)
    {
        while (collectedGems.Count > 0)
        {
            gem = collectedGems.Pop();
            gem.collectableData.IncreaseTotalSoldAmount();
            GameManager.Instance.SetTotalMoneyValue((int)(gem.collectableData.startingPrice + gem.transform.lossyScale.x * 100));
            gem.transform.parent = null;
            gem.CallCollectAnimaiton(sellPointPosition);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HarvestableSquare"))
        {
            var farmController = other.GetComponent<FarmSpawnController>();
            var collectable = farmController.HarvestGem();

            if (collectable == null)
            {
                return;
            }
            
            if (collectedGems.Count <= 0)
            {
                newGemYPos = 0;
                collectable.transform.parent = stackPoint;
                collectedGems.Push(collectable);
                collectable.CallCollectAnimaiton();
            }
            else
            {
                var lastGem = collectedGems.Pop();
                var lastGemTransform = lastGem.transform;
                collectedGems.Push(lastGem);
                var collectableTransform = collectable.transform;
                
                newGemYPos += lastGemTransform.localScale.y + collectableTransform.lossyScale.y / 2;
                
                collectableTransform.parent = stackPoint;
                collectedGems.Push(collectable);
                collectable.CallCollectAnimaiton(null, newGemYPos);
            }
        }
        
        if (other.CompareTag("SellPoint"))
        {
            
            if (collectedGems.Count < 0)
                return;

            StartCoroutine(SellGems(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SellPoint"))
        {
            if (collectedGems.Count < 0)
                return;

            StopCoroutine(SellGems());
        }
    }
}
