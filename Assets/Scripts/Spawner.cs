using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _spawnDelay = 2f;

    private Coroutine _spawnJob;

    private bool _isSpawn;

    private int _currentSpawnPoint = -1;

    private void Awake()
    {
        StartSpawn();
    }

    private void OnDestroy()
    {
        BreakSpawn();
    }

    private void StartSpawn()
    {
        BreakSpawn();

        _isSpawn = true;
        StartCoroutine(SpawnCoroutine(_spawnDelay));
    }

    private void BreakSpawn()
    {
        if (_spawnJob != null)
            StopCoroutine(_spawnJob);
    }

    private IEnumerator SpawnCoroutine(float spawnDelay)
    {
        var awaiter = new WaitForSeconds(spawnDelay);

        while(_isSpawn)
        {
            SpawnNextEnemy();

            yield return awaiter;
        }
    }

    private void StopSpawn()
    {
        _isSpawn = false;
    }

    private void SpawnNextEnemy()
    {
        if (_enemy == null)
            return;

        if (_spawnPoints.Length == 0)
            return;

        _currentSpawnPoint = (++_currentSpawnPoint) % _spawnPoints.Length;

        Transform point = _spawnPoints[_currentSpawnPoint];
        Vector3 spawnPosition = point.position;
        Quaternion spawnRotation = point.rotation;

        Instantiate(_enemy, spawnPosition, spawnRotation);
    }
}
