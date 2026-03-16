using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int currentWaveId = 1;
    [SerializeField] private List<GameObject> waveEnemies = new List<GameObject>();
    [SerializeField] private int deadEnemiesCount = 0;
    [SerializeField] private bool isWaveActive = false;

    [Header("Spawners")]
    [SerializeField] private List<GameObject> tutorialSpawners;
    [SerializeField] private List<GameObject> wave1Spawners;
    [SerializeField] private List<GameObject> wave2Spawners;
    [SerializeField] private List<GameObject> wave3Spawners;
    [SerializeField] private List<List<GameObject>> waveSpawnersList = new List<List<GameObject>>();

    // eventos
    public event Action OnNewWaveStarted;
    public event Action OnWaveCompleted;
    public event Action OnAllWavesCompleted;

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        waveSpawnersList = new List<List<GameObject>>()
        {
            wave1Spawners,
            wave2Spawners,
            wave3Spawners
        };
    }

    private void Update()
    {
        if (!isWaveActive || waveEnemies.Count <= 0)
        {
            return;
        }

        if (deadEnemiesCount >= waveEnemies.Count)
        {
            OnWaveCompleted?.Invoke();
            HandleWaveCompleted();
        }
    }

    public void StartWaves()
    {
        currentWaveId = 1;
        deadEnemiesCount = 0;
        isWaveActive = true;

        ControlSpawners(tutorialSpawners, false);
        ControlSpawners(wave1Spawners, true);
        OnNewWaveStarted?.Invoke();
    }

    public void HandleWaveCompleted()
    {
        waveEnemies.Clear();
        deadEnemiesCount = 0;

        if (currentWaveId > waveSpawnersList.Count - 1)
        {
            OnAllWavesCompleted?.Invoke();
        }
        else
        {
            ControlSpawners(waveSpawnersList[currentWaveId], true);
            OnNewWaveStarted?.Invoke();
        }

        currentWaveId++;
    }

    public void AddNewEnemy(GameObject enemy)
    {
        if (isWaveActive && !waveEnemies.Contains(enemy))
        {
            waveEnemies.Add(enemy);
        }
    }

    public void IncreaseDeadEnemiesCount()
    {
        if (isWaveActive)
        {
            deadEnemiesCount++;
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
        tutorialSpawners[index].SetActive(active);
    }

    public int GetCurrentWaveId()
    {
        return currentWaveId;
    }

    public int GetDeadEnemiesCount()
    {
        return deadEnemiesCount;
    }
}
