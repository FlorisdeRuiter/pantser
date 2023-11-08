using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDamage : Damage
{
    private Projectile projectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
        damage = projectile.damage;
    }
}
