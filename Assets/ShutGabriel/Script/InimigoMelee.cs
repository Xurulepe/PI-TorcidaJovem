using UnityEngine;
using System.Collections;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class InimigoMelee : InimigoDef
{
    [Header("Configuração InimigoM")]


    public float IntervaloAtaque = 1f;

    public bool _playerNaArea = false;
    private GameObject _player;

    [Header("configuração da detecção")]
    public float _raio = 3f;
    public float _tamanho = 3f;
    public LayerMask layermask;
    public Color gizmoColor = Color.cyan;


    //KnockBeck
    public float _knockbackForce = 40f;
    public float knockbackDuration = 0.3f;
    public float gravity = -9.81f;


    [SerializeField] public CharacterController controller;
    private Vector3 EnemyVelocity;
    private Vector3 knockbackVelocity;
    private float knockbackTimer;
    public bool PlayerHitBox;
    [SerializeField] Transform _enemy;
    private Vector3 direction;
    bool _field = false;

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
        if (controller.isGrounded && EnemyVelocity.y < 0)
            EnemyVelocity.y = -2f;
        else
            EnemyVelocity.y += gravity * Time.deltaTime;

        if (knockbackTimer > 0)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackTimer -= Time.deltaTime;
        }
        IntervaloAtaque -= Time.deltaTime;
    }
    public void ApplyKnockback(Vector3 direction, float force = -1)
    {
        if (force < 0) force = _knockbackForce;

        knockbackVelocity = direction * force;
        knockbackTimer = knockbackDuration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HitPlayer"))
        {
            //Debug.Log("Hit");
            _playerNaArea = true;
            _player = other.gameObject;

            if (_playerNaArea == true)
            {
                if (IntervaloAtaque <= 0)
                {
                    Ataque();
                    IntervaloAtaque = 2f;
                }

            }

            if (other.CompareTag("Espadão"))
            {
                Debug.Log(" knockback melle " + other.gameObject.name);
                Vector3 knockDir = (_enemy.transform.position - other.transform.position).normalized;
                ApplyKnockback(knockDir);
            }
        }

        if (other.gameObject.CompareTag("Forcefield") && !_field)
        {

            _field = true;
            ApplyKnockback(transform.position);
            Invoke(nameof(FieldResp), 1);

        }


    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Espadão"))
        {
            Vector3 knockDir = (transform.position - hit.point).normalized;
            ApplyKnockback(knockDir);
        }


    }

    void FieldResp()
    {

        _field = false;

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HitPlayer"))
        {
            _playerNaArea = false;
            _player = null;
        }
    }

    protected void Ataque()
    {
        Debug.Log("Atacou o player");

        PlayerHit();

        StartCoroutine(CooldownAtk());
    }

    protected void PlayerHit()
    {
        PlayerHealthScript PlayerHealth = _alvo.gameObject.GetComponent<PlayerHealthScript>();
        PlayerHealth.DamagePlayer(dano, direction);
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