using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class MeleeAttack : MonoBehaviour, IAbility
{
    [Header("Attack Configurations")]
    public float baseDamage;
    [SerializeField] private float baseAttackInterval;

    [Header("Event")]
    [SerializeField] private UnityEvent attackEvent;

    [Header("Animation")]
    private Animator anim;
    private readonly string attackTrigger = "attack";

    public Damage Damage;

    private Player player;

    private void Awake()
    {
        player = Player.GetInstance();
        anim = GetComponent<Animator>();
        Damage = GetComponentInChildren<Damage>();
        Damage.damage = baseDamage;
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }

    #region Attack
    /// <summary>
    /// What event does is decided in the inspector.
    /// This event is meant for inplementing all effects besides damage.
    /// </summary>
    public void OnAttack()
    {
        attackEvent?.Invoke();
    }

    /// <summary>
    /// Activates the attack's animation.
    /// Triggers attackEvent.
    /// </summary>
    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(baseAttackInterval * player.constantIntervalModifier);

        yield return wait;

        anim.SetTrigger(attackTrigger);
        OnAttack();

        StartCoroutine(Attack());
    }
    #endregion

    #region Collider toggle
    /// <summary>
    /// Toggles the collider on or off depending on it's current state.
    /// This function is called at the start and end of the attack's animation.
    /// </summary>
    private void ToggleCollider()
    {
        BoxCollider2D collider = GetComponentInChildren<BoxCollider2D>();
        collider.enabled = !collider.enabled;
    }
    #endregion

    public void SetConfig(Modification pModification)
    {
        baseDamage = pModification.Damage;
        baseAttackInterval = pModification.Interval;
    }
}
