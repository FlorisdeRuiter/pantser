using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration", order = 0)]
public class EnemyScriptableStats : ScriptableObject
{
    public int maxHealth;
    public int damage;

    public float chaseSpeed;
}
