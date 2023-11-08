using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Shield : MonoBehaviour
{
    public int maxHitsToBreak;
    public int hitsToBreak;

    private void OnEnable()
    {
        hitsToBreak = maxHitsToBreak;
    }

    /// <summary>
    /// Removes charge from shield
    /// Disables Utility if no charges are left
    /// Kills enemy
    /// </summary>
    /// <param name="collision">The collider the shield collides with</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy;
        if (collision.TryGetComponent<IEnemy>(out enemy))
        {
            hitsToBreak -= 1;
            if (hitsToBreak <= 0)
            {
                GetComponentInParent<Utility>().DisableChildren();
            }

            enemy.Die();
        }
    }
}
