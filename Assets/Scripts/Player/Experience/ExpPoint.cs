using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExpPoint : PoolItem
{
    private Player player;
    private ExperienceManager experienceManager;

    [SerializeField] private int expValue;

    private void Start()
    {
        player = Player.GetInstance();
        experienceManager = ExperienceManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player.GetPlayerTag()))
        {
            experienceManager.OnGainExp(expValue);
            ReturnToPool();
        }
    }
}
