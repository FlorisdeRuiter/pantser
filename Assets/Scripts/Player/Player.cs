using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Player : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    [Header("Ability")]
    public float constantDamageModifier = 0;
    public float constantIntervalModifier = 0;
    public float constantCooldownModifier = 0;

    [Header("Event")]
    public UnityEvent damageEvent;

    private static Player _instance;

    private void Start()
    {
        health = maxHealth;
    }

    #region Do Damage
    public void DoDamage(float damage)
    {
        health -= damage;
        damageEvent?.Invoke();
    }
    #endregion

    #region Returns Tag
    public string GetPlayerTag()
    {
        return gameObject.tag;
    }
    #endregion

    #region Returns Transform
    public Transform GetTransform()
    {
        return transform;
    }
    #endregion 

    public static Player GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<Player>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<Player>();
        }

        return _instance;
    }
}
