using UnityEditorInternal;
using Unity.AI;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using DG.Tweening;
using UnityEngine.AI;
using Unity.VisualScripting;

public class InimigoFreeze : MonoBehaviour
{
    [SerializeField] NavMeshAgent _Inimigo;
    [SerializeField] Transform Jogador;
    public float speed = 3.0f;
    public float _freezeDistance = 10f;
    public bool _IsFreeze = false;
    private void Start()
    {
        _Inimigo = GetComponent<NavMeshAgent>();
        GameObject Jogador = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Jogador.position);
        if (distanceToPlayer > _freezeDistance)
        {
            _IsFreeze = false;
            // Vector3 direction = (Jogador.position - transform.position).normalized;
            //transform.position += direction * speed * Time.deltaTime;
            if (Jogador != null && _Inimigo != null)
            {
                _Inimigo.SetDestination(Jogador.position);
            }
        }
        else
        {

            _IsFreeze = true;
            
        }
    }
}
