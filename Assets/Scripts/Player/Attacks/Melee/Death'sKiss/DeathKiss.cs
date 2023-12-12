using UnityEngine;

public class DeathKiss : AMeleeAttack
{
    #region Damage Enemy on Collision
    /// <summary>
    /// Checks if the target that's been hit has an IDamageable script attached.
    /// </summary>
    /// <param name="collision">The collider the attack collided with</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_timeUntilAttack > 0)
            return;

        IDamageable damageable;
        EnemyHealth enemy;
        //Checks if collided object has Damageable Interface
        if (collision.gameObject.TryGetComponent<IDamageable>(out damageable) && collision.gameObject.TryGetComponent<EnemyHealth>(out enemy))
        {
            DrainHp(enemy);
            damageable.DoDamage(BaseDamage);

            Attack();
        }
    }
    #endregion

    public void DrainHp(EnemyHealth enemy)
    {
        GameManager.GetInstance().Player.PlayerHealth.Heal(enemy.CurrentHealth / 2);
    }
}
