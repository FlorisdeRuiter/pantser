using UnityEngine;

public interface IEnemy
{
    public void MoveTowardsTarget();
    public Transform GetTransform();
}
