using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class InimigoFollow : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    public Transform _player;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_player.position);
    }
}
