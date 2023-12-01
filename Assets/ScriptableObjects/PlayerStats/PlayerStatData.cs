using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Configuration", menuName = "ScriptableObject/Player Configuration", order = 2)]
[Serializable]
public class PlayerStatData : ScriptableObject
{
    public float DamageModifier;
    public float MeleeDamageModifier;
    public float RangedDamageModifier;
    public float AttackSpeedModifier;
    public float MovementSpeed;
    public float DamageReduction;
    public float ProjectSpeedModifier;
    public float MaxHp;
    public float HpRegen;
    public float LifeSteal;
    public float ExpGainModifier;
    public float AbilityCooldownModifier;

    public float EvasionRate;
    public float CriticalHitRate;
    public float ItemPickupRange;
}
