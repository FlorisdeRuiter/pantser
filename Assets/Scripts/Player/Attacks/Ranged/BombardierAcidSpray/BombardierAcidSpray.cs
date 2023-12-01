using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardierAcidSpray : Projectile
{
    [SerializeField] private GameObject _areaEffect;

    public override void Die()
    {
        Instantiate(_areaEffect, transform.position, Quaternion.identity);
        base.Die();
    }
}
