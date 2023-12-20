using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Damage : MonoBehaviour
{
    protected AAttack _damage;

    protected PlayerManager _player;

    [Header("Event")]
    [SerializeField] protected UnityEvent _damageEvent;

    private void Start()
    {
        _player = GameManager.GetInstance().Player;
        _damage = GetComponentInParent<AAttack>();
    }

    #region Damage Enemy on Collision
    /// <summary>
    /// Checks if the target that's been hit has an IDamageable script attached.
    /// </summary>
    /// <param name="collision">The collider the attack collided with</param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        //Checks if collided object has Damageable Interface
        if (collision.gameObject.TryGetComponent<IDamageable>(out damageable) && !collision.CompareTag(GameManager.GetInstance().Player.tag))
        {
            //Does Damage to damageable
            damageable.DoDamage(_damage.BaseDamage);

            _damageEvent?.Invoke();
        }
    }
    #endregion
}
