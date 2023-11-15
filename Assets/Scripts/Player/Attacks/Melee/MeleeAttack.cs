using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class MeleeAttack : MonoBehaviour, IAbility
{
    [Header("Attack Configurations")]
    public float BaseDamage;
    [SerializeField] private float _baseAttackInterval;

    [Header("Event")]
    [SerializeField] private UnityEvent _attackEvent;

    [Header("Animation")]
    private Animator _anim;
    private readonly string _attackTrigger = "attack";

    public Damage Damage;

    private Player _player;

    private void Awake()
    {
        _player = Player.GetInstance();
        _anim = GetComponent<Animator>();
        Damage = GetComponentInChildren<Damage>();
        Damage.DamageAmount = BaseDamage;
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }

    #region Attack
    public void OnAttack()
    {
        _attackEvent?.Invoke();
    }

    private IEnumerator Attack()
    {
        // Sets wait to the attack interval
        WaitForSeconds wait = new WaitForSeconds(_baseAttackInterval * _player.ConstantIntervalModifier);

        yield return wait;

        // Plays attack animation
        _anim.SetTrigger(_attackTrigger);
        OnAttack();

        StartCoroutine(Attack());
    }
    #endregion

    #region Collider toggle
    private void ToggleCollider()
    {
        BoxCollider2D collider = GetComponentInChildren<BoxCollider2D>();
        collider.enabled = !collider.enabled;
    }
    #endregion

    public void SetConfig(Modification pModification)
    {
        BaseDamage = pModification.Damage;
        _baseAttackInterval = pModification.Interval;
    }
}
