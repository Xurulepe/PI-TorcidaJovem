using UnityEngine;
using UnityEngine.AI;

public class InimigoV2 : InimigoDefault
{
 
    protected override void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _Agent.stoppingDistance = stoppingDistance;
        _shootTimer = _shootInterval;
    }
    protected override void Update()
    {

        if (_player != null)
        {
            float distance = Vector3.Distance(transform.position, _player.position);
            if (distance > stoppingDistance)
            {
                _Agent.isStopped = false;
                _Agent.SetDestination(_player.position);
            }
            else
            {
                _Agent.isStopped = true;
                transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
                Shoot();
            }
        }
    }

    


}
