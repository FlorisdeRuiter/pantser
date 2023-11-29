using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetEnemyExpPool : MonoBehaviour
{
    private static SetEnemyExpPool _instance; 
    public ObjectPool ExpPool;

    public static SetEnemyExpPool GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<SetEnemyExpPool>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("EnemyExpPool").AddComponent<SetEnemyExpPool>();
        }

        return _instance;
    }
}
