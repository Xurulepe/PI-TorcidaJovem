using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int currentWaveId = 0;
    [SerializeField] private List<GameObject> waveEnemies = new List<GameObject>();

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        int deadEnemies = 0;

        foreach (var enemy in waveEnemies)
        {
            if (enemy.activeInHierarchy)
            {
                deadEnemies++;
            }
        }

        if (deadEnemies >= waveEnemies.Count)
        {
            StartNextWave();
        }
    }

    public void StartNextWave()
    {
        waveEnemies.Clear();
        currentWaveId++;
    }

    public void AddNewEnemy(GameObject enemy)
    {
        if (!waveEnemies.Contains(enemy))
        {
            waveEnemies.Add(enemy);
        }
    }
}
