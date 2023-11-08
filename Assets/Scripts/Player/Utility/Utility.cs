using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Utility : Ability, IAbility
{
    public enum DisableType
    {
        time,
        damage
    }

    [Header("Timers")]
    [Tooltip("Time until object disables in seconds")]
    [SerializeField] private float upTime;
    [Tooltip("Time object will be disabled for in seconds")]
    [SerializeField] private float downTime;

    [Header("Disable Type")]
    [SerializeField] private DisableType disableType;

    [Header("Event")]
    [SerializeField] private UnityEvent utilityEvent;

    private readonly string summonTrigger = "summon";

    private Animator anim;

    private Shield shield;

    #region Give Object Disable Type and Start Timer
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        switch (disableType)
        {
            case DisableType.time:
                StartCoroutine(UptimeTimer());
                break;
            case DisableType.damage:
                shield = GetComponentInChildren<Shield>();
                EnableChildren();
                break;
            default:
                break;
        }
        utilityEvent?.Invoke();
    }
    #endregion

    #region Up and Down Timers
    /// <summary>
    /// Timer for how long utility is active
    /// </summary>
    private IEnumerator UptimeTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(upTime);

        yield return wait;

        DisableChildren();
    }

    /// <summary>
    /// Timer for how long utility is inactive
    /// </summary>
    private IEnumerator DowntimeTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(downTime);

        yield return wait;

        EnableChildren();
    }
    #endregion

    #region Activate and Disactivate Utility
    /// <summary>
    /// Enables children and activates utility
    /// Triggers summon animation
    /// </summary>
    public override void EnableChildren()
    {
        base.EnableChildren();

        anim.SetTrigger(summonTrigger);

        switch (disableType)
        {
            case DisableType.time:
                StartCoroutine(UptimeTimer());
                break;
            case DisableType.damage:
                shield.hitsToBreak = shield.maxHitsToBreak;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Disables children and disactivates utility
    /// </summary>
    public override void DisableChildren()
    {
        base.DisableChildren();

        StartCoroutine(DowntimeTimer());
    }
    #endregion

    public void UpdateUtilityConfigs(float uptimeMod, float downtimeMod)
    {
        upTime = uptimeMod;
        downTime = downtimeMod;
    }

    public void SetConfig(Modification pModification)
    {
        downTime = pModification.Downtime;
        upTime = pModification.Uptime;
    }
}
