using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickupable pickupable;
        if (collision.TryGetComponent<IPickupable>(out pickupable))
        {
            pickupable.PickUp();
        }
    }
}
