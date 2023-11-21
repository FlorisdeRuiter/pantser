using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration", order = 1)]
[Serializable]
public class EnemyScriptableData : ScriptableObject
{
    public int MaxHealth;
    public int Damage;

    public float ChaseSpeed;
}
