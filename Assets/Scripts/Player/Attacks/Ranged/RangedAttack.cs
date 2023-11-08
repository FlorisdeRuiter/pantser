using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class RangedAttack : MonoBehaviour
{
    public enum TargetType
    {
        closestTarget,
        randomTarget,
        randomDirection
    }

    [Header("Attack Configurations")]
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseAttackInterval;

    [Header("Enemy Target Type")]
    [SerializeField] TargetType targetType;

    [Header("Event")]
    [SerializeField] private UnityEvent attackEvent;

    [Header("Object Pool")]
    private ObjectPool projectilePool;
    private List<Projectile> projectileList;

    [Header("Animation")]
    private Animator anim;
    private readonly string attackTrigger = "attack";

    [Header("Target Finder")]
    private NearbyEnemiesCheck nearbyEnemies;

    private Player player;

    private void Awake()
    {
        player = Player.GetInstance();
        nearbyEnemies = NearbyEnemiesCheck.GetInstance();
        anim = GetComponent<Animator>();
        projectilePool = GetComponentInChildren<ObjectPool>();
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }

    #region Attack
    /// <summary>
    /// What event does is decided in the inspector
    /// This event is meant for inplementing all effects beside damage
    /// </summary>
    public void OnAttack()
    {
        attackEvent?.Invoke();
    }

    /// <summary>
    /// Calculates what rotation projectile needs to move in to hit target.
    /// Activates projectile from pool in calculated rotation.
    /// </summary>
    public void ActivateProjectile()
    {
        Transform target = null;

        switch (targetType)
        {
            case TargetType.closestTarget:
                target = nearbyEnemies.GetNearestEnemy();
                break;
            case TargetType.randomTarget:
                target = nearbyEnemies.GetRandomEnemy();
                break;
            case TargetType.randomDirection:
                target = null;
                break;
        }

        Vector3 spawnPos = transform.position;

        if (target != null)
        {
            Vector3 targetPos = target.position;

            //Checks what direction the target is in
            targetPos.x = targetPos.x - spawnPos.x;
            targetPos.y = targetPos.y - spawnPos.y;
            targetPos.z = 0;

            //Calculates projectile's angle to target the enemy
            float angle = Mathf.Atan2(-targetPos.x, targetPos.y) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));

            GameObject e = projectilePool.GetPooledObject(transform.position, transform.rotation) as GameObject;
            e.transform.position = spawnPos;
            e.transform.rotation = rot;
        }
        else
        {
            float angle = Mathf.Atan2(Random.Range(-1f,1f), Random.Range(-1f, 1f)) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));

            GameObject e = projectilePool.GetPooledObject(transform.position, transform.rotation) as GameObject;
            e.transform.position = spawnPos;
            e.transform.rotation = rot;
        }
    }

    /// <summary>
    /// Activates the attack's animation
    /// Triggers attackEvent
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(baseAttackInterval * player.constantIntervalModifier);

        yield return wait;

        anim.SetTrigger(attackTrigger);
        OnAttack();

        StartCoroutine(Attack());
    }
    #endregion

    public void UpdateAttackConfigs(float damageMod, float intervalMod)
    {
        baseDamage = damageMod;
        foreach (Transform projectile in projectilePool.transform)
        {
            projectile.GetComponent<Projectile>().damage = baseDamage * player.constantDamageModifier;
        }
        baseAttackInterval = intervalMod;
    }
}
