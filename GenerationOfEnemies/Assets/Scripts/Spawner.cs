using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy; 
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private float _spawnCooldown;

    private Transform[] _spawnPoints;

    private IEnumerator SpawnEnemies()
    {
        var endOfSeconds = new WaitForSeconds(_spawnCooldown);
        foreach (var spawnPoint in _spawnPoints)
        {
            Instantiate(_enemy, spawnPoint.position, Quaternion.identity);
            yield return endOfSeconds;
        }
        Debug.Log("Все враги созданы!");
    }

    private void Awake()
    {
        _spawnPoints = new Transform[_spawnPointsParent.childCount];
        for (int i = 0; i < _spawnPointsParent.childCount; i++)
        {
            _spawnPoints[i] = _spawnPointsParent.GetChild(i);
        }
        ShufflePointsArray();
        StartCoroutine(SpawnEnemies());
    }

    private void ShufflePointsArray()
    {
        int numberToSwap;
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            numberToSwap = Random.Range(i, _spawnPoints.Length);
            var temp = _spawnPoints[numberToSwap];
            _spawnPoints[numberToSwap] = _spawnPoints[i];
            _spawnPoints[i] = temp;
        }
    }
}
