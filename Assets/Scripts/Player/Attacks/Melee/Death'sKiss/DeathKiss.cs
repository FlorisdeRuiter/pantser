using UnityEngine;

public class DeathKiss : MeleeDamage
{
    #region Damage Enemy on Collision
    /// <summary>
    /// Checks if the target that's been hit has an IDamageable script attached.
    /// </summary>
    /// <param name="collision">The collider the attack collided with</param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        BaseEnemy enemy;
        //Checks if collided object has Damageable Interface
        if (collision.gameObject.TryGetComponent<IDamageable>(out damageable) && !collision.CompareTag(Player.GetInstance().tag) && collision.gameObject.TryGetComponent<BaseEnemy>(out enemy))
        {
            //Does Damage to damageable
            damageable.DoDamage(DamageAmount * _player.ConstantDamageModifier);
            DrainHp(enemy);

            _damageEvent?.Invoke();
        }
    }
    #endregion

    public void DrainHp(BaseEnemy enemy)
    {
        _player.Health = enemy.CurrentHealth / 2;
    }
}
