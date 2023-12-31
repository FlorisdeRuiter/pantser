using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BaseEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private PlayerManager _player;
    public EnemyScriptableData StatData;

    [Header("Damage")]
    [SerializeField] private float _damage;
    [SerializeField] private float _attackInterval;
    [SerializeField] private float _timeUntilAttack;

    [Header("Speed")]
    [SerializeField] private float _chaseSpeed;

    private ObjectPool _expPool;
    public EnemyHealth Health;

    private void Start()
    {
        Health = GetComponent<EnemyHealth>();
        _expPool = SetEnemyExpPool.GetInstance().ExpPool;
        _player = GameManager.GetInstance().Player;
        SetEnemyStats();
    }

    private void Update()
    {
        _timeUntilAttack -= Time.deltaTime;
        MoveTowardsTarget();
    }

    #region Move to Player
    public virtual void MoveTowardsTarget()
    {
        //Sets target
        Vector2 targetPos = _player.transform.position;

        //Moves enemy towards target
        transform.position = Vector2.MoveTowards(transform.position, targetPos, _chaseSpeed * Time.deltaTime);
    }
    #endregion

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_timeUntilAttack > 0)
            return;

        IDamageable damageable;
        if (collision.collider.CompareTag(_player.gameObject.tag) && collision.gameObject.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.DoDamage(_damage);
        }

        _timeUntilAttack = _attackInterval;
    }

    /// <summary>
    /// Sets enemy stats gained from scriptable object.
    /// </summary>
    private void SetEnemyStats()
    {
        GetComponent<EnemyHealth>().SetMaxHp(StatData.MaxHealth);
        _damage = StatData.Damage;
        _chaseSpeed = StatData.ChaseSpeed;
        _attackInterval = StatData.AttackInterval;
    }

    public void DropExp()
    {
        // Takes exp from the object pool and places it on the enemy's location
        GameObject exp = _expPool.GetPooledObject(transform.position, transform.rotation, _expPool.gameObject.transform) as GameObject;
        exp.GetComponent<ExpPoint>().ExpValue = StatData.Value;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
