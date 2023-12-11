using UnityEngine;

public class RangedAttackClosestTarget : ARangedAttack
{
    protected override void Attack()
    {
        _target = SetTarget();
        base.Attack();
    }

    private Transform SetTarget()
    {
        return _nearbyEnemiesChecker.GetNearestEnemy();
    }
}