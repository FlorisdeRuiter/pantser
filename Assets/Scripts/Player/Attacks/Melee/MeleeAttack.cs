using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class MeleeAttack : Ability, IAbility
{
    private enum EIntervalType
    {
        Interval,
        ConditionalInterval
    }

    [Header("Attack Configurations")]
    public float BaseDamage;
    [SerializeField] private float _baseAttackInterval;
    private float _timeSinceAttack;

    [Header("Event")]
    [SerializeField] private UnityEvent _attackEvent;

    [Header("Animation")]
    private Animator _anim;
    private readonly string _attackTrigger = "attack";

    private EIntervalType _intervalType;

    [HideInInspector]
    public Damage Damage;

    private Player _player;

    private void Awake()
    {
        _player = Player.GetInstance();
        _anim = GetComponent<Animator>();
        Damage = GetComponentInChildren<Damage>();
        Damage.DamageAmount = BaseDamage;
        _timeSinceAttack = _baseAttackInterval;
    }

    private void Update()
    {
        _timeSinceAttack -= Time.deltaTime;
        if (_timeSinceAttack > 0)
            return;

        AttackOnInterval();
    }

    #region Attack
    public void OnAttack()
    {
        _attackEvent?.Invoke();
    }

    private void AttackOnInterval()
    {
        // Plays attack animation
        _anim.SetTrigger(_attackTrigger);
        OnAttack();

        _timeSinceAttack = _baseAttackInterval;
    }
    #endregion

    #region Collider toggle
    private void ToggleCollider()
    {
        Collider2D collider = GetComponentInChildren<Collider2D>();
        collider.enabled = !collider.enabled;
    }
    #endregion

    public void SetConfig(Modification pModification)
    {
        BaseDamage = pModification.Damage;
        _baseAttackInterval = pModification.Interval;
    }
}
