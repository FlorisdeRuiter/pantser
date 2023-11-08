using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void MoveTowardsTarget();
    Transform GetTransform();
    public void Die();
    public void OnDropExp();
}
