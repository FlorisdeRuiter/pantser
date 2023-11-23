using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public PlayerStatData BasePlayerStats;

    private Dictionary<EStatType, float> _baseStatValues;
    private Dictionary<EStatType, float> _modifiedStatValues;
    [SerializeField] private UnityEvent<float> _statUpdateEvent;

    private void Awake()
    {
        _baseStatValues = new Dictionary<EStatType, float>()
        {
        [EStatType.Damage] = BasePlayerStats.DamageModifier,
        [EStatType.MeleeDamage] = BasePlayerStats.MeleeDamageModifier,
        [EStatType.RangedDamage] = BasePlayerStats.RangedDamageModifier,
        [EStatType.AttackSpeed] = BasePlayerStats.AttackSpeedModifier,
        [EStatType.MovementSpeed] = BasePlayerStats.MovementSpeed,
        [EStatType.DamageReduction] = BasePlayerStats.DamageReduction,
        [EStatType.ProjectileSpeed] = BasePlayerStats.ProjectSpeedModifier,
        [EStatType.MaxHp] = BasePlayerStats.MaxHp,
        [EStatType.HpRegen] = BasePlayerStats.HpRegen,
        [EStatType.LifeSteal] = BasePlayerStats.LifeSteal,
        [EStatType.ExpGain] = BasePlayerStats.ExpGainModifier,
        [EStatType.AbilityCooldown] = BasePlayerStats.AbilityCooldownModifier,
        [EStatType.Evasion] = BasePlayerStats.EvasionRate,
        [EStatType.CriticalHit] = BasePlayerStats.CriticalHitRate,
        [EStatType.ItemPickup] = BasePlayerStats.ItemPickupRange
        };

        _modifiedStatValues = _baseStatValues;
    }

    public void SetStatModification(EStatType statType, bool isPercentChange, float statMod)
    {
        float baseStat = _baseStatValues[statType];
        float modStat = _modifiedStatValues[statType];
        if (isPercentChange && statMod >= 0)
        {
            _modifiedStatValues[statType] += statMod;
        }
        else
        {
            _modifiedStatValues[statType] += baseStat * (statMod / 100);
        }    
    }

    public float GetStatValue(EStatType statType)
    {
        return _baseStatValues[statType];
    }
}
