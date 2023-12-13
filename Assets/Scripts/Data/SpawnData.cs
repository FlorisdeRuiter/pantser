using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnEvent
{
    public float Time;
    public string Message;
    public List<EnemySpawnData> EnemySpawnData;
}

[Serializable]
public class EnemySpawnData
{
    public GameObject EnemyToSpawn;
    public int AmountToSpawn;
}

[CreateAssetMenu(fileName = "Spawn Configuration", menuName = "ScriptableObject/Spawn Configuration", order = 1)]
public class SpawnData : ScriptableObject
{
    public List<SpawnEvent> SpawnEvents;
}
