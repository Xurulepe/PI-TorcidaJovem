using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int currentWaveId = 1;
    [SerializeField] private List<GameObject> waveEnemies = new List<GameObject>();
    [SerializeField] private int deadEnemiesCount = 0;
    [SerializeField] private bool isWaveActive = false;

    public UnityEvent OnAllEnemiesDead;

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!isWaveActive || waveEnemies.Count <= 0)
        {
            return;
        }

        if (deadEnemiesCount >= waveEnemies.Count)
        {
            OnAllEnemiesDead?.Invoke();
            StartNextWave();
        }
    }

    public void StartWaves()
    {
        deadEnemiesCount = 0;
        isWaveActive = true;
    }

    public void StartNextWave()
    {
        waveEnemies.Clear();
        deadEnemiesCount = 0;
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

    public int GetCurrentWaveId()
    {
        return currentWaveId;
    }
}
