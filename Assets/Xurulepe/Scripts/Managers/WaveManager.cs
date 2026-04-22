using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int _currentWaveId = 1;
    [SerializeField] private int _deadEnemiesCount = 0;
    [SerializeField] private bool _isWaveActive = false;

    [Header("Wave Enemies")]
    [SerializeField] private List<GameObject> _waveEnemiesList = new List<GameObject>();

    [Header("Spawners")]
    [SerializeField] private List<GameObject> _tutorialSpawners;
    [SerializeField] private List<GameObject> _wave1Spawners;
    [SerializeField] private List<GameObject> _wave2Spawners;
    [SerializeField] private List<GameObject> _wave3Spawners;
    
    private List<List<GameObject>> _waveSpawnersList = new List<List<GameObject>>();

    // eventos
    public event Action OnNewWaveStarted;
    public event Action OnWaveCompleted;
    public event Action OnAllWavesCompleted;

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        _waveSpawnersList = new List<List<GameObject>>()
        {
            _wave1Spawners,
            _wave2Spawners,
            _wave3Spawners
        };
    }

    private void Update()
    {
        if (!_isWaveActive || _waveEnemiesList.Count <= 0)
        {
            return;
        }

        if (_deadEnemiesCount >= _waveEnemiesList.Count)
        {
            OnWaveCompleted?.Invoke();

            HandleWaveCompleted();
        }
    }

    public void StartWaves()
    {
        _currentWaveId = 1;
        _deadEnemiesCount = 0;
        _isWaveActive = true;

        ControlSpawners(_tutorialSpawners, false);
        ControlSpawners(_wave1Spawners, true);

        OnNewWaveStarted?.Invoke();
    }

    public void HandleWaveCompleted()
    {
        _waveEnemiesList.Clear();
        _deadEnemiesCount = 0;

        if (_currentWaveId > _waveSpawnersList.Count - 1)
        {
            OnAllWavesCompleted?.Invoke();
        }
        else
        {
            ControlSpawners(_waveSpawnersList[_currentWaveId], true);

            OnNewWaveStarted?.Invoke();
        }

        _currentWaveId++;
    }

    public void AddNewEnemy(GameObject enemy)
    {
        if (_isWaveActive)
        {
            _waveEnemiesList.Add(enemy);
        }
    }

    public void IncreaseDeadEnemiesCount()
    {
        if (_isWaveActive)
        {
            _deadEnemiesCount++;
        }
    }

    private void ControlSpawners(List<GameObject> spawnerList, bool active)
    {
        foreach (GameObject spawner in spawnerList)
        {
            spawner.SetActive(active);
        }
    }

    public void ControlSingleTutorialSpawner(int index, bool active)
    {
        _tutorialSpawners[index].SetActive(active);
    }

    public int GetCurrentWaveId()
    {
        return _currentWaveId;
    }

    public int GetDeadEnemiesCount()
    {
        return _deadEnemiesCount;
    }
}
