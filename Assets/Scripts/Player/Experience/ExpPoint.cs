using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExpPoint : PoolItem, IPickupable
{
    private Player player;
    private ExperienceManager experienceManager;

    [SerializeField] private int expValue;

    private void Start()
    {
        player = Player.GetInstance();
        experienceManager = ExperienceManager.GetInstance();
    }

    public void PickUp()
    {
        experienceManager.OnGainExp(expValue);
        ReturnToPool();
    }
}
