using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration", order = 0)]
public class EnemyScriptableStats : ScriptableObject
{
    public int MaxHealth;
    public int Damage;

    public float ChaseSpeed;
}
