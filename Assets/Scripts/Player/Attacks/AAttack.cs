using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public abstract class AAttack : Ability, IAbility
{
    [Header("Attack Configurations")]
    [SerializeField] public float BaseDamage;
    [SerializeField] protected float _baseAttackInterval;
    [SerializeField] protected float _timeUntilAttack;

    [Header("Animation")]
    protected Animator _anim;
    protected readonly string _attackTrigger = "attack";

    [SerializeField] protected UnityEvent _attackEvent;
    protected PlayerManager _player;

    private void Awake()
    {
        _player = GameManager.GetInstance().Player;
        _anim = GetComponent<Animator>();
        _timeUntilAttack = _baseAttackInterval;
    }

    protected abstract void Attack();

    protected void OnAttack()
    {
        _attackEvent?.Invoke();
    }

    public virtual void SetConfig(Modification pModification)
    {
        BaseDamage = pModification.Damage;
        _baseAttackInterval = pModification.Interval;
    }
}
