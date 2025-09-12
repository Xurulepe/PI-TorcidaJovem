using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
public class InimigoShooter : InimigoDef
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;

    public float _intervaloTiro = 2f;
    public float _distanciaMin= 7f;
    public float _distanciaSeg = 4f; 
    public float _recuoDist = 5f;
    public float _tempo = 0f;

    protected override void Start()
    {
        base.Start();
        if (_agent != null) _agent.speed = 3.5f;
    }
    protected override void Update()
    {
        base.Update();
        if(_alvo == null || _agent == null) return;
        float distancia = Vector3.Distance(transform.position, _alvo.position);

        if(distancia > _distanciaMin)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_alvo.position);
        }
        else if (distancia < _distanciaSeg)
        {
            _agent.isStopped = false;
            Vector3 direcaoFuga = (transform.position-_alvo.position).normalized;
            Vector3 destinoFuga = transform.position + direcaoFuga * _recuoDist;
            _agent.SetDestination(destinoFuga);
        }
        else
        {
            _agent.isStopped = true;
            _tempo += Time.deltaTime;
            if(_tempo >= _intervaloTiro)
            {
                Atirar();
                _tempo = 0f;
            }
        }
    }
    private void Atirar()
    {
        if (projectilePrefab == null || spawnPoint == null || _alvo == null) return;
        Vector3 dir = (_alvo.position- spawnPoint.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);

        GameObject proj = Instantiate(projectilePrefab,spawnPoint.position, rot);
        Projetil p = proj.GetComponent<Projetil>();
    }
}
