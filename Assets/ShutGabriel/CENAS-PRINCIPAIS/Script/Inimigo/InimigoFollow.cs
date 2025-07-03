using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class InimigoFollow : MonoBehaviour
{
    public GameObject _spriteVirus;
    public Transform cameraTrans;
    public Transform shadowPosition;
    public Transform _player;

    [SerializeField] NavMeshAgent _agent;
    public float _distanciaATK = 2f;
    public float _TempoEntreATKS = 2;
    public int _dano = 1;

    private float _tempoProximoATK = 1f;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, _player.position);
        if (distancia > _distanciaATK)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.position);
        }
        else
        {
            _agent.isStopped = true;
            Vector3 direcao = (_player.position.normalized - transform.position).normalized;
            //Quaternion rotacao = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * 5f);
            if(Time.time >= _tempoProximoATK)
            {
                Atacar();
                _tempoProximoATK = Time.time + _TempoEntreATKS;
            }
        }
    }
    void LateUpdate()
    {
        _spriteVirus.transform.LookAt(_spriteVirus.transform.localPosition + cameraTrans.position);
        shadowPosition.transform.LookAt(shadowPosition.transform.localPosition + cameraTrans.position);
    }
    void Atacar()
    {
        _player.GetComponent<PlayerDano>()?.LevarDano();
         Debug.Log("-1 HP");
    }
}
