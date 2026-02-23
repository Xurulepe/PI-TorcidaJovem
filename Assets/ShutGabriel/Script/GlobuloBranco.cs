using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GlobuloBranco : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] GameObject _currentTarget;
    [SerializeField] List<Transform> transforms = new List<Transform>();
    void start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }
    void UpdateTarget()
    {
        _currentTarget = FindClosestEnemy();
        if (_currentTarget != null )
        {
            _agent.SetDestination(_currentTarget.transform.position);
        }
    }
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach ( GameObject enemy in enemies )
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }
        return closest;
    }
}
