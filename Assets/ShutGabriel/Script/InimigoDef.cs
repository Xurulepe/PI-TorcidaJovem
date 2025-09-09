using UnityEngine;
using UnityEngine.AI;

public class InimigoDef : MonoBehaviour
{
  
    protected NavMeshAgent _agent;
    protected Transform _alvo;
    public int vida = 5;
    [HideInInspector] public Spawner spawner;
    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _alvo = player.transform;
        }
    }
    protected virtual void Update()
    {
        if (_alvo != null && _agent != null)
        {
            _agent.SetDestination(_alvo.position);
        }
    }
    public virtual void LevarDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Morrer();
        }
    }
    public virtual void Morrer()
    {
        Destroy(gameObject);
    }


}
