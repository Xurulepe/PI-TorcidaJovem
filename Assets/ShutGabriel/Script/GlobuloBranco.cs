using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GlobuloBranco : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Transform _currentTarget;
    [SerializeField] List<Transform> enemies = new List<Transform>();
    public float _updateRate = 0.5f;
    void Start()
    {
        if (_agent == null)
            _agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(UpdateTarget), 0f, _updateRate);
    }
    void UpdateTarget()
    {
        UpdateEnemyList();
        _currentTarget = FindClosestEnemy();

        if (_currentTarget != null)
        {
            _agent.SetDestination(_currentTarget.position);
        }
    }
    void UpdateEnemyList()
    {
        enemies.Clear();

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyObjects)
        {
            enemies.Add(enemy.transform);
        }
    }
    Transform FindClosestEnemy()
    {
        if (enemies.Count == 0)
            return null;

        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform enemy in enemies)
        {
            float distance = (enemy.position - currentPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
