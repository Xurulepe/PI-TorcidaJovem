using UnityEngine;
using System.Collections;
using DG.Tweening;
public class InimigoMelee : InimigoDef
{
    [Header("Configuração InimigoM")]

    
    public float IntervaloAtaque = 3f;
    private float _clock = 2f;
    public bool _playerNaArea = false;
    private GameObject _player;

    [Header("configuração da detecção")]
    public float _raio = 3f;
    public float _tamanho = 3f;
    public LayerMask layermask;
    public Color gizmoColor = Color.cyan;

    [SerializeField] int Hurt = 0;

    //Tags
    

    private Vector3 direction;

    protected override void Start()
    {
        base.Start();
        _agent.speed = 10f;
    }
    
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
                //lembrete, para o inimigo aguentar mais fazer o check hit voltar a ser false após timming
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
            if (_playerNaArea == true)
            {
                Ataque();
                _clock = 0f;
            }
            /*if (other.CompareTag("EspadaHitBox"))
            {
                Vector3 knockDir = (transform.position - other.transform.position).normalized;
                ApplyKnockback(knockDir);
            }
            */
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

    protected virtual void Ataque()
    {
        Debug.Log("Atacou o player");
        
        PlayerHealthScript playerHealth = GetComponent<PlayerHealthScript>();
        playerHealth.DamagePlayer(Hurt, direction);

        StartCoroutine(CooldownAtk());
        //StartCoroutine(CooldownAtk());
    }

    

    protected override void LevarDano(int dano)
    {
        base.LevarDano(dano);
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

   
    IEnumerator CooldownAtk()
    {
        yield return new WaitForSeconds(1.25f);
       
    }
}
