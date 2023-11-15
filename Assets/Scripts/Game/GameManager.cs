using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ObjectPool> EnemyPoolList;
    private EnemySpawner _spawner;

    private static GameManager _instance;

    private void Start()
    {
        _spawner = EnemySpawner.GetInstance();
        EnemyPoolList = _spawner.EnemyPoolsList;
    }

    public static GameManager GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<GameManager>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<GameManager>();
        }

        return _instance;
    }
}
