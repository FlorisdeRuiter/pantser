using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BaseEnemy : PoolItem, IEnemy, IDamageable
{
    private Player player;
    [SerializeField] private EnemyScriptableStats scriptableStats;

    [Header("Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [Header("Damage")]
    [SerializeField] private float damage;

    [Header("Speed")]
    [SerializeField] private float chaseSpeed;

    private ObjectPool expPool;

    protected override void Activate()
    {
        SetEnemyStats();
        player = Player.GetInstance();
        currentHealth = maxHealth;
        expPool = _myPool.GetComponent<SetEnemyExpPool>().expPool;
    }

    private void Update()
    {
        MoveTowardsTarget();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    #region Move to Player
    public virtual void MoveTowardsTarget()
    {
        //Sets target
        Vector2 targetPos = player.GetTransform().position;

        //Moves enemy towards target
        transform.position = Vector2.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);
    }
    #endregion

    #region Damage Player 
    /// <summary>
    /// Damages player when enemy collides with it.
    /// Kills enemy when it collides with player.
    /// </summary>
    /// <param name="collision">The player's collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        if (collision.CompareTag(player.tag) && collision.gameObject.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.DoDamage(damage);
            DoDamage(currentHealth);
        }
    }
    #endregion

    #region Damage
    /// <summary>
    /// Damages enemy.
    /// </summary>
    /// <param name="damage">Value taken from the current health</param>
    public void DoDamage(float damage)
    {
        currentHealth -= damage;
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
        maxHealth = scriptableStats.maxHealth;
        damage = scriptableStats.damage;
        chaseSpeed = scriptableStats.chaseSpeed;
    }
    #endregion

    #region Returns Transform
    public Transform GetTransform()
    {
        return transform;
    }
    #endregion

    public void OnDropExp()
    {
        expPool.GetPooledObject(transform.position, transform.rotation, expPool.gameObject.transform); 
    }
}
