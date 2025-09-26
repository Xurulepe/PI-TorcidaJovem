using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;

public class InimigoDef : MonoBehaviour
{
  
    protected NavMeshAgent _agent;
    protected Transform _alvo;
    public int vida = 1;

    [SerializeField] protected int dano = 1;
    [SerializeField]  MeshRenderer[] _renderer;
    [SerializeField] protected ParticleSystem[] _part;
    [SerializeField] protected Collider[] _CL;
    [SerializeField] protected bool _checkHIT;
    [SerializeField] protected bool _checkMorte;
    [SerializeField] protected bool _isHIT;

    [SerializeField] protected Vector3 ScaleStart;
    protected float _tempo = 0f;
    bool _startV;
    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _alvo = player.transform;
        }

        if (!_startV)
        {
            ScaleStart = transform.localScale;
            _startV = true;
        }

    }
    protected virtual void Update()
    {

        if (_alvo != null && _agent != null)
        {
            _agent.SetDestination(_alvo.position);
        }
    }
    protected virtual void LevarDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Morrer();
        }
    }
    protected virtual void Morrer()
    {
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
        }
        gameObject.SetActive(false);
    }

    protected virtual IEnumerator HitTime()
    {
        _checkMorte = true;

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].transform.DOScale(2, .25f);
        }
        for (int i = 0; i < _CL.Length; i++)
        {
            _CL[i].enabled = false;
        }
        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < _part.Length; i++)
        {
            _part[i].Play();
        }

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
        }

        yield return new WaitForSeconds(0.25f);

        yield return new WaitForSeconds(0.25f);

        _checkHIT = false;
        LevarDano(dano);
    }


    public virtual void Vida()
    {
        vida = 1;
        _tempo = 0f;
        _checkMorte = false;
        _isHIT = false;
        
        if (_startV)
        {
            transform.localScale = ScaleStart;
        }

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = true;
        }
    }


}
