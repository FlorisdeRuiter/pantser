using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : PoolItem
{
    public enum ProjectileBreakType
    {
        hit,
        time
    }

    [Header("Stats")]
    [SerializeField] private float _speed;
    public float Damage;

    [Header("Life Timer")]
    [SerializeField] private float _lifeTime;

    [Header("Break Type")]
    public ProjectileBreakType BreakType;

    private void OnEnable()
    {
        StartCoroutine(DestroyProjectileTimer());
    }

    private void Update()
    {
        MoveProjectile(); 
    }

    #region Projectile Life Timer
    /// <summary>
    /// Destroys projectile after period of time
    /// </summary>
    private IEnumerator DestroyProjectileTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(_lifeTime);

        yield return wait;

        Die();
    }
    #endregion

    #region Moves Projectile
    /// <summary>
    /// Moves projectile based on it's speed and rotation.
    /// </summary>
    private void MoveProjectile()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }
    #endregion

    #region Destroy Projectile
    /// <summary>
    /// Checks when projectile collides with enemy.
    /// </summary>
    /// <param name="collision">The collider the projectile collides with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy;
        if (collision.TryGetComponent<IEnemy>(out enemy) && BreakType == ProjectileBreakType.hit)
        {
            Die();
        }
    }

    /// <summary>
    /// Returns projectile to pool.
    /// </summary>
    public void Die()
    {
        ReturnToPool();
    }
    #endregion
}
