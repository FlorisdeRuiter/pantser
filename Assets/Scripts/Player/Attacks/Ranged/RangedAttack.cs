using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class RangedAttack : MonoBehaviour, IAbility
{
    public enum TargetType
    {
        closestTarget,
        randomTarget,
        randomDirection
    }

    [Header("Attack Configurations")]
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _baseAttackInterval;

    [Header("Enemy Target Type")]
    [SerializeField] private TargetType _targetType;

    [Header("Event")]
    [SerializeField] private UnityEvent _attackEvent;

    [Header("Object Pool")]
    private ObjectPool _projectilePool;
    private List<Projectile> _projectileList;

    [Header("Animation")]
    private Animator _anim;
    private readonly string _attackTrigger = "attack";

    [Header("Target Finder")]
    private NearbyEnemiesCheck _nearbyEnemiesChecker;

    private Player player;

    private void Awake()
    {
        player = Player.GetInstance();
        _nearbyEnemiesChecker = NearbyEnemiesCheck.GetInstance();
        _anim = GetComponent<Animator>();
        _projectilePool = GetComponentInChildren<ObjectPool>();
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
        _attackEvent?.Invoke();
    }

    /// <summary>
    /// Calculates what rotation projectile needs to move in to hit target.
    /// Activates projectile from pool in calculated rotation.
    /// </summary>
    public void ActivateProjectile()
    {
        Transform target = null;

        switch (_targetType)
        {
            case TargetType.closestTarget:
                target = _nearbyEnemiesChecker.GetNearestEnemy();
                break;
            case TargetType.randomTarget:
                target = _nearbyEnemiesChecker.GetRandomEnemy();
                break;
            case TargetType.randomDirection:
                target = null;
                break;
        }

        Vector3 spawnPos = transform.position;

        if (target != null)
        {
            Vector3 targetPos = target.position;

            // Checks what direction the target is in
            targetPos.x = targetPos.x - spawnPos.x;
            targetPos.y = targetPos.y - spawnPos.y;
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

    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(_baseAttackInterval * player.ConstantIntervalModifier);

        yield return wait;

        //_anim.SetTrigger(_attackTrigger);
        OnAttack();

        StartCoroutine(Attack());
    }
    #endregion

    public void UpdateAttackConfigs(float damageMod, float intervalMod)
    {
        _baseDamage = damageMod;
        foreach (Transform projectile in _projectilePool.transform)
        {
            projectile.GetComponent<Projectile>().Damage = _baseDamage * player.ConstantDamageModifier;
        }
        _baseAttackInterval = intervalMod;
    }

    public void SetConfig(Modification pModification)
    {
        _baseDamage = pModification.Damage;
        _baseAttackInterval = pModification.Interval;
    }
}
