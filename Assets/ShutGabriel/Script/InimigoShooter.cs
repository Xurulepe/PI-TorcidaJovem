using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;
using System;

public class InimigoShooter : InimigoDef
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;

  
    public float _intervaloTiro = 2f;
    public float _distanciaMin= 7f;
    public float _distanciaSeg = 6f; 
    public float _recuoDist = 5f;
    public float _clock = 3f;


    [Header("configuração da capsula")]
    public float _raio = 3f;
    public float _tamanho = 3f;
    public LayerMask layermask;
    public Color gizmoColor = Color.cyan;

 

    

    protected override void Start()
    {

        base.Start();

        if (_agent != null) _agent.speed = 10.5f;
        
    }
    protected override void Update()
    {
        base.Update();
        
        if (_alvo == null || _agent == null) return;
        float distancia = Vector3.Distance(transform.position, _alvo.position);

        if(distancia > _distanciaMin)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_alvo.position);
        }
        else if (distancia < _distanciaSeg)
        {
            _agent.isStopped = false;
            Vector3 direcaoFuga = (transform.position -_alvo.position).normalized;
            Vector3 destinoFuga = transform.position + direcaoFuga * _recuoDist;
            _agent.SetDestination(destinoFuga);
        }
        else
        {
            if(_StopTiro == true)
            {
                _clock = 0f;
            }
            _agent.isStopped = true;
            _clock += Time.deltaTime;
            if(_clock >= _intervaloTiro)
            {
                _StopTiro = false;
                Atirar();
                _clock = 0f;
            }
        }
        if (!_checkMorte)
        {
            Vector3 point1, point2;
            GetCapsulePoints(out point1, out point2);
            _isHIT = Physics.CheckCapsule(point1, point2, _raio, layermask);
            if (_isHIT && !_checkHIT)
            {
                _checkHIT = true;
                Debug.Log("Alvo Colidiu na Capsula");

                StartCoroutine(HitTime());

            }
        }


    }
    protected override void LevarDano(int dano)
    {
        base.LevarDano(dano);
    }
    private void Atirar()
    {
        if(_StopTiro == false)
        {
            if (projectilePrefab == null || spawnPoint == null || _alvo == null) return;
            Vector3 dir = (_alvo.position - spawnPoint.position).normalized;
            Quaternion rot = Quaternion.LookRotation(dir);

            GameObject proj = Instantiate(projectilePrefab, spawnPoint.position, rot);
            Projetil p = proj.GetComponent<Projetil>();
        }
    }


    private void OnDrawGizmos()
    {
        Vector3 point1, point2;
        GetCapsulePoints(out point1, out point2);

        Gizmos.color = _isHIT ? Color.red : gizmoColor;
        Gizmos.DrawWireSphere(point1, _raio);
        Gizmos.DrawWireSphere(point2, _raio);

        Gizmos.DrawLine(point1 + transform.right * _raio, point2 + transform.right * _raio);

        Gizmos.DrawLine(point1 - transform.right * _raio, point2 - transform.right * _raio);

        Gizmos.DrawLine(point1 + transform.forward * _raio, point2 + transform.forward * _raio);

        Gizmos.DrawLine(point1 - transform.forward * _raio, point2 - transform.forward * _raio);

    }
    private void GetCapsulePoints(out Vector3 point1, out Vector3 point2)
    {
        Vector3 center = transform.position;
        float halfheight = Mathf.Max(_tamanho * 0.5f - _raio, 0f);
        Vector3 Up = transform.up * halfheight;
        point1 = center + Up;
        point2 = center - Up;
    }
    

}
