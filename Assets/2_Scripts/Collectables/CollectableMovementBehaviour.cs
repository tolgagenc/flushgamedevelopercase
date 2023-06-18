using UnityEngine;

public abstract class CollectableMovementBehaviour : MonoBehaviour
{
    public abstract void MoveBehaviour(Collectable collectable, Transform collectPoint = null, float ?yPosOfCollectable = null);
}