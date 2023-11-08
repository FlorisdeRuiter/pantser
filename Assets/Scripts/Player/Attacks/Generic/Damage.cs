using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Damage : MonoBehaviour
{
    public float damage;

    private Player player;

    [Header("Event")]
    [SerializeField] protected UnityEvent damageEvent;

    private void Awake()
    {
        player = Player.GetInstance();
    }

    #region Damage Enemy on Collision
    /// <summary>
    /// Checks if the target that's been hit has an IDamageable script attached.
    /// If it does, damage the target.
    /// </summary>
    /// <param name="collision">The collider the attack collided with</param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        //Checks if collided object has Damageable Interface
        if (collision.gameObject.TryGetComponent<IDamageable>(out damageable) && !collision.CompareTag(Player.GetInstance().tag))
        {
            //Does Damage to damageable
            damageable.DoDamage(damage * player.constantDamageModifier);

            damageEvent?.Invoke();
        }
    }
    #endregion
}
