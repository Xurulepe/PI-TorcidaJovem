using UnityEngine;
using System.Collections;
using DG.Tweening;
public class InimigoMelee : InimigoDef
{
    public int dano = 1;
    public float IntervaloAtaque = 3f;
    private float tempo = 0f;
    private bool _playerNaArea = false;
    private GameObject _player;

    [Header("configuração da capsula")]
    public float _raio = 3f;
    public float _tamanho = 3f;
    public LayerMask layermask;
    public Color gizmoColor = Color.cyan;

    private bool _isHIT;
    bool _checkHIT;
    bool _checkMorte;

    [SerializeField] MeshRenderer[] _renderer;
    [SerializeField] ParticleSystem[] _part;
    [SerializeField] Collider[] _CL;




    protected override void Update()
    {
        base.Update();
        if (_alvo != null)
        {

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"));
        {
            //Debug.Log("Hit");
            _playerNaArea = true;
            _player = other.gameObject;
            if (_playerNaArea = true)
            {
                Ataque();
                tempo = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerNaArea = false;
            _player = null;
        }
    }

    private void Ataque()
    {
        Debug.Log("Atacou o player");
        StartCoroutine(CooldownAtk());
    }

    protected override void Start()
    {
        base.Start();
        _agent.speed = 3.5f;
    }
    
    public void LevarDano(int dano)
    {
        vida -= dano;
        if (vida >= 0)
        {
            Morrer();
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

    IEnumerator HitTime()
    {
        _checkMorte = true;

        for(int i = 0; i <  _renderer.Length ; i++)
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
    }
    IEnumerator CooldownAtk()
    {
        yield return new WaitForSeconds(0.25f);

        yield return new WaitForSeconds(0.25f);

        yield return new WaitForSeconds(0.25f);
    }
}
