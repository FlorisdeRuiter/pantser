using UnityEngine;

public class SpawnEventManager : MonoBehaviour
{
    [SerializeField] private SpawnData _spawnData;

    [SerializeField] private EnemySpawner _enemySpawner;

    private GameManager _timer;
    private int _eventIndex;

    private void Awake()
    {
        _timer = GameManager.GetInstance();
    }

    private void Update()
    {
        if (_eventIndex >= _spawnData.SpawnEvents.Count)
            return;

        SpawnEvent spawnEvent = _spawnData.SpawnEvents[_eventIndex];
        if (_timer.GameTime > spawnEvent.Time)
        {
            foreach (EnemySpawnData spawnData in spawnEvent.EnemySpawnData)
            {
                for (int i = 0; i < spawnData.AmountToSpawn; i++)
                {
                    _enemySpawner.SpawnEnemy(spawnData.EnemyToSpawn);
                }
            }

            _eventIndex += 1;
        }
    }
}
