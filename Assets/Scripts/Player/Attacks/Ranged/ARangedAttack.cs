using System.Collections.Generic;
using UnityEngine;

public abstract class ARangedAttack : AAttack
{
    [Header("Object Pool")]
    [SerializeField] protected ObjectPool _projectilePool;

    [Header("Target Finder")]
    protected NearbyEnemiesCheck _nearbyEnemiesChecker;

    protected Transform _target;

    public virtual void Start()
    {
        _nearbyEnemiesChecker = NearbyEnemiesCheck.GetInstance();
        _projectilePool = GetComponentInChildren<ObjectPool>();
    }

    private void Update()
    {
        _timeUntilAttack -= Time.deltaTime;

        if (_timeUntilAttack > 0)
            return;

        Attack();
        _timeUntilAttack = _baseAttackInterval;
    }

    protected override void Attack()
    {
        OnAttack();
    }

    /// <summary>
    /// Calculates what rotation projectile needs to move in to hit target.
    /// Activates projectile from pool in calculated rotation.
    /// </summary>
    public void ActivateProjectile()
    {
        Vector3 spawnPos = _player.transform.position;

        if (_target != null)
        {
            Vector3 targetPos = _target.position;

            // Checks what direction the target is in
            targetPos -= spawnPos;
            targetPos.z = 0;

            // Calculates projectile's angle to target the enemy
            float angle = Mathf.Atan2(-targetPos.x, targetPos.y) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));

            GameObject e = _projectilePool.GetPooledObject(transform.position, transform.rotation) as GameObject;
            e.transform.position = spawnPos;
            e.transform.rotation = rot;
        }
        else
        {
            // Shoots projectile in a random direction if there were no available enemies
            float angle = Mathf.Atan2(Random.Range(-1f,1f), Random.Range(-1f, 1f)) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));

            GameObject e = _projectilePool.GetPooledObject(transform.position, transform.rotation) as GameObject;
            e.transform.position = spawnPos;
            e.transform.rotation = rot;
        }
    }

    public override void SetConfig(Modification pModification)
    {
        base.SetConfig(pModification);
    }
}