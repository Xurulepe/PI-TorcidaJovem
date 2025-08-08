using UnityEngine;
using UnityEngine.AI;

public class InimigoDefault : MonoBehaviour
{

    protected GameObject _spriteVírus;
    protected Transform _cameraTransf;
    protected Transform _shadowPosition;
    protected Transform _player;
    protected Animator _anim;

    protected float _distanciaATK = 2f;
    protected float _TempoEntreATKS = 2;
    protected int _dano = 1;
    
    [SerializeField] bool _PlayerInRange;

    [SerializeField] protected NavMeshAgent _Agent;
    [SerializeField] protected GameObject _Player;



    protected float stoppingDistance = 10f;
    protected GameObject projectilePrefab;
    protected Transform ShootPoint;

    protected float _shootInterval = 2f;
    protected float _projectileSpeed = 10f;
    protected float _shootTimer;

    protected virtual void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }
    protected virtual void Atacar()
    {
        _player.GetComponent<PlayerDano>().LevarDano();
        Debug.Log("-1 HP");
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _PlayerInRange = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _PlayerInRange = false;
        }
    }

    protected virtual void LateUpdate()
    {
        _spriteVírus.transform.LookAt(_spriteVírus.transform.localPosition + _cameraTransf.position);
        _shadowPosition.transform.LookAt(_shadowPosition.transform.localPosition + _cameraTransf.position);
    }

    protected virtual void Shoot()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer <= 0f)
        {
            GameObject projectile = Instantiate(projectilePrefab, ShootPoint.position, ShootPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = ShootPoint.forward * _projectileSpeed;
            }
            _shootTimer = _shootInterval;
        }
    }
}
