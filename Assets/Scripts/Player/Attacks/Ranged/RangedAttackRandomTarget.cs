using UnityEngine;

public class RangedAttackRandomTarget : ARangedAttack
{
    protected override void Attack()
    {
        _target = SetTarget();
        base.Attack();
    }

    private Transform SetTarget()
    {
        return _nearbyEnemiesChecker.GetRandomEnemy();
    }
}
