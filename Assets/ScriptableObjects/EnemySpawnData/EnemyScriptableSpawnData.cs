using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Spawn Configuration", menuName = "ScriptableObject/Enemy Spawn Configuration", order = 0)]
public class EnemyScriptableSpawnData : ScriptableObject
{
    public EnemyScriptableData EnemyType;

    public float SpawnTime;
    public float TimeUntilAddedToSpawnPool;
}
