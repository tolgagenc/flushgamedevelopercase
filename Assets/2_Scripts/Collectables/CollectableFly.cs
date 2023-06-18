using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectableFly : CollectableMovementBehaviour
{
    private Collectable _collectable;
    private Transform _target;
    private bool _canMoveTowards = false;

    private void Update()
    {
        if (_canMoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, 75 * Time.deltaTime);
        }
    }

    public override void MoveBehaviour(Collectable collectable, Transform collectPoint = null,
        float? yPosOfCollectable = null)
    {
        _target = collectPoint;
        _collectable = collectable;


        if (_target == null)
        {
            if (yPosOfCollectable != null)
                transform.DOLocalMove(new Vector3(0, (float) yPosOfCollectable, 0), Random.Range(0.2f, 0.4f))
                    .SetEase(Ease.Linear);
            else
                transform.DOLocalMove(Vector3.zero, Random.Range(0.2f, 0.4f))
                    .SetEase(Ease.Linear);
        }
        else
        {
            transform.DOLocalMove(_target.position, Random.Range(0.2f, 0.4f)).SetEase(Ease.Linear)
                .OnComplete(() => { _collectable.Despawn(); });
        }
    }
}