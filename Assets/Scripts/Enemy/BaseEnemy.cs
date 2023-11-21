using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BaseEnemy : PoolItem, IEnemy, IDamageable
{
    private Player _player;
    [SerializeField] private EnemyScriptableData _scriptableStats;

    [Header("Health")]
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    [Header("Damage")]
    [SerializeField] private float _damage;

    [Header("Speed")]
    [SerializeField] private float _chaseSpeed;

    private ObjectPool _expPool;

    protected override void Activate()
    {
        SetEnemyStats();
        _player = Player.GetInstance();
        _currentHealth = _maxHealth;
        _expPool = _myPool.GetComponent<SetEnemyExpPool>().expPool;
    }

    private void Update()
    {
        MoveTowardsTarget();
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    #region Move to Player
    public virtual void MoveTowardsTarget()
    {
        //Sets target
        Vector2 targetPos = _player.GetTransform().position;

        //Moves enemy towards target
        transform.position = Vector2.MoveTowards(transform.position, targetPos, _chaseSpeed * Time.deltaTime);
    }
    #endregion

    #region Damage Player 
    /// <summary>
    /// Damages player when enemy collides with it.
    /// </summary>
    /// <param name="collision">The player's collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        if (collision.CompareTag(_player.tag) && collision.gameObject.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.DoDamage(_damage);
        }
    }
    #endregion

    #region IDamageable
    /// <summary>
    /// Damages enemy.
    /// </summary>
    /// <param name="damage">Value taken from the current health</param>
    public void DoDamage(float damage)
    {
        _currentHealth -= damage;
    }

    public Transform GetTransform()
    {
        return transform;
    }
    #endregion

    #region Death
    /// <summary>
    /// Returns enemy to it's pool
    /// </summary>
    public void Die()
    {
        ReturnToPool();
        OnDropExp();
    }
    #endregion

    #region Set Stats
    /// <summary>
    /// Sets enemy stats gained from scriptable object.
    /// </summary>
    private void SetEnemyStats()
    {
        _maxHealth = _scriptableStats.MaxHealth;
        _damage = _scriptableStats.Damage;
        _chaseSpeed = _scriptableStats.ChaseSpeed;
    }
    #endregion

    public void OnDropExp()
    {
        // Takes exp from the object pool and places it on the enemy's location
        _expPool.GetPooledObject(transform.position, transform.rotation, _expPool.gameObject.transform); 
    }
}
