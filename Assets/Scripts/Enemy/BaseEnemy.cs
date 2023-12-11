using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BaseEnemy : PoolItem, IEnemy
{
    private PlayerManager _player;
    public EnemyScriptableData StatData;

    [Header("Damage")]
    [SerializeField] private float _damage;

    [Header("Speed")]
    [SerializeField] private float _chaseSpeed;

    [SerializeField] private ObjectPool _expPool;

    private void Start()
    {
        _expPool = SetEnemyExpPool.GetInstance().ExpPool;
        _player = GameManager.GetInstance().Player;
    }

    protected override void Activate()
    {
        SetEnemyStats();
        EnemySpawner.GetInstance().CurrentEnemyValue += StatData.Value;
    }

    private void Update()
    {
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

    #region Damage Player 
    /// <summary>
    /// Damages player when enemy collides with it.
    /// </summary>
    /// <param name="collision">The player's collider</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        if (collision.CompareTag(_player.gameObject.tag) && collision.gameObject.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.DoDamage(_damage);
        }
    }
    #endregion

    /// <summary>
    /// Sets enemy stats gained from scriptable object.
    /// </summary>
    private void SetEnemyStats()
    {
        GetComponent<EnemyHealth>().SetMaxHp(StatData.MaxHealth);
        _damage = StatData.Damage;
        _chaseSpeed = StatData.ChaseSpeed;
    }

    public void DropExp()
    {
        // Takes exp from the object pool and places it on the enemy's location
        _expPool.GetPooledObject(transform.position, transform.rotation, _expPool.gameObject.transform);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
