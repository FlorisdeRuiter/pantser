using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Shield : MonoBehaviour
{
    public int MaxHitsToBreak;
    public int HitsToBreak;

    private void OnEnable()
    {
        HitsToBreak = MaxHitsToBreak;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy;
        if (collision.TryGetComponent<IEnemy>(out enemy))
        {
            //Removes charge from shield
            HitsToBreak -= 1;
            if (HitsToBreak <= 0)
            {
                //Disables shield if no charges are left
                GetComponentInParent<Utility>().DisableChildren();
            }
        }
    }
}
