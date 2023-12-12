using System.Collections;
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
    [SerializeField] private float _upTime;
    [Tooltip("Time object will be disabled for in seconds")]
    [SerializeField] private float _downTime;

    [Header("Disable Type")]
    [SerializeField] private DisableType _disableType;

    [Header("Event")]
    [SerializeField] private UnityEvent _utilityEvent;

    private readonly string _summonTrigger = "summon";

    private Animator anim;

    private Shield shield;

    private PlayerManager _player;

    public override void Start()
    {
        base.Start();
        _player = GameManager.GetInstance().Player;
    }

    private void Update()
    {
        transform.position = _player.transform.position;
    }

    #region Give Object Disable Type and Start Timer
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        switch (_disableType)
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
        _utilityEvent?.Invoke();
    }
    #endregion

    #region Up and Down Timers
    /// <summary>
    /// Timer for how long utility is active
    /// </summary>
    private IEnumerator UptimeTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(_upTime);

        yield return wait;

        DisableChildren();
    }

    /// <summary>
    /// Timer for how long utility is inactive
    /// </summary>
    private IEnumerator DowntimeTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(_downTime);

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

        anim.SetTrigger(_summonTrigger);

        switch (_disableType)
        {
            case DisableType.time:
                StartCoroutine(UptimeTimer());
                break;
            case DisableType.damage:
                shield.HitsToBreak = shield.MaxHitsToBreak;
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
        _upTime = uptimeMod;
        _downTime = downtimeMod;
    }

    public void SetConfig(Modification pModification)
    {
        _downTime = pModification.Downtime;
        _upTime = pModification.Uptime;
    }
}
