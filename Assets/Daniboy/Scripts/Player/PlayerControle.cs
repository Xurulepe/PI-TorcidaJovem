using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.Rendering.DebugUI;


public class PlayerControle : MonoBehaviour
{
    [Header("Melee")]
    public float _attackDistance = 3f;
    public float _attackDelay = 0.4f;
    public float _attackSpeed = 1f;
    public float _attackDamage = 1f;
    public LayerMask _attackLayer;

    public bool _isAttacking;
    public bool _readytoAttack;
    public int attackCount;
    [SerializeField] public Transform attackPoint;

    [Header("PlayerInfo")]
    public float _lifepoints;

    [Header("Movimentação")]
    public float moveSpeed = 5f;
    public float _dashSpeed;
    public bool _dashing;
    public Transform Orientation;
    public CharacterController Controller;
    public Vector3 moveInput; // agora usamos Vector2 (x,y)
    public float _rotationSpeed = 10f;

    // vetores fixos para projeção isométrica
    private Vector3 isoForward = new Vector3(1, 0, 1).normalized;
    private Vector3 isoRight = new Vector3(1, 0, -1).normalized;

    [Header("Particle")]
    [SerializeField] private ParticleSystem _muzzleFlash;

    [Header("Gravidade")]
    [SerializeField] Vector3 _playerVelocity;
    public float _gravityFloat = -9.81f;

    [Header("Tiro")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;
    public float _shootDelay = 1f;
    public bool _readytoShoot;
    public bool _isShooting;
    [Header("Camera")]
    private Camera cam;

    [Header("Manage")]
    public bool _lockMove;
    public bool _Death;

    [Header("Animation")]
   // public Animator _Anim;

    [Header("ScriptCalling")]
    public Dashing _dashScript;

    [Header("animaPlayer_ruan")]
    public  AnimaControleRobo_ruan anima_robo_ruan;
    public Animator _anima_Robo;
    public bool shootExec;

    void Start()
    {
        _dashScript = GetComponent<Dashing>();
        //_Anim = GetComponent<Animator>();
        cam = Camera.main;
        if (Controller == null)
            Controller = GetComponent<CharacterController>();
    }




    void Update()
    {
        Gravity();
        Move();

        RotateTowardsMouse();
        //Shootmanage();


        if (Input.GetKeyDown(KeyCode.R))
        {
            LockFunction();
        }
    }

    public void AttackShoot(InputAction.CallbackContext value)
    {
        if (_lockMove == false && _Death == false && shootExec == false)
        {

            //_Anim.SetTrigger("Shoot");
            _anima_Robo.SetTrigger("Shot");
            shootExec = true;

        }
    }

    public void AttackMeele(InputAction.CallbackContext value)
    {
        if (_lockMove == false && _Death == false)
        {
            if (value.performed)
            {
                anima_robo_ruan.quantAtk++;                
            }

        }
    }

    public void Muzzleflash()
    {

        //_muzzleFlash.Play();

    }

    public void LockFunction()
    {
        _lockMove = true;
        _dashScript.DashMove();
        AttackRaycast();

        Move();
       // Shoot();
       // RotateTowardsMouse();
        Gravity();
        Melee();
    }

    public void Move()
    {
        if (_lockMove == false && _Death == false)
        {
            _anima_Robo.SetInteger("quantAtk", anima_robo_ruan.quantAtk);
            Vector3 move = new Vector3(moveInput.x, 0f, moveInput.z);

            if (move.sqrMagnitude > 0.001f)
            {
                Controller.Move(move.normalized * moveSpeed * Time.deltaTime);
                // _Anim.SetBool("Walk", true);
                _anima_Robo.SetBool("walk", true);
            }
            else
            {
                // _Anim.SetBool("Walk", false);
                _anima_Robo.SetBool("walk", false);
            }
        }
    }

    // recebe entrada do Input System
    public void PlayerMove(InputAction.CallbackContext value)
    {

        moveInput = value.ReadValue<Vector3>();
    }

    public void Gravity()
    {
        if (_lockMove == false)
        {
            _playerVelocity.y += _gravityFloat * Time.deltaTime;
            Controller.Move(_playerVelocity * Time.deltaTime);
        }

    }


    void RotateTowardsMouse()
    {
        if (_lockMove == false && _Death == false)
        {

            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 lookPos = hit.point;
                lookPos.y = transform.position.y;

                Vector3 dir = lookPos - transform.position;
                if (dir.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(dir);
            }
        }

    }

    public void Shoot()
    {
        GameObject bullet = BulletPooling.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.transform.position;
            bullet.transform.rotation = shootPoint.transform.rotation;
            Projectile proj = bullet.GetComponent<Projectile>();
            proj.speedProjectile = projectileSpeed;
            proj.shootPoint = shootPoint;
            bullet.SetActive(true);
        }
    }

    void Melee()
    {
        if (_lockMove == false) 
        {
            if (!_readytoAttack || _isAttacking) return;
            _readytoAttack = false;
            _isAttacking = true;
            Invoke(nameof(ResetAttack), _attackSpeed);
            Invoke(nameof(AttackRaycast), _attackDelay);

        }
    }

    void Shootmanage()
    {
        if (_lockMove == false)
        {
            if (!_readytoShoot || _isShooting) return;
            _readytoShoot = false;
            _isShooting = true;
            Invoke(nameof(ResetShoot), projectileSpeed);
            Invoke(nameof(Shoot), _shootDelay);

        }

    }

    void ResetShoot() 
    {

        _readytoShoot = true;
        _isShooting = false;
    }

    void ResetAttack()
    {
        _readytoAttack = true;
        _isAttacking = false;
    }

    void AttackRaycast()
    {
        if (_lockMove == false) 
        {
           
            RaycastHit hit;
            if (Physics.Raycast(attackPoint.transform.position, attackPoint.transform.forward, out hit, _attackDistance, _attackLayer))
            {
             
                Debug.Log(hit.transform.name);
                Debug.Log("HitTarget");
            }

        }
   
    }

    public void CallHelp()
    {
        _anima_Robo.SetTrigger("CallHelp");
    }

    public void CallShieldUp()
    {
        _anima_Robo.SetTrigger("ShieldUp");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyMelee"))
            Debug.Log("You hit the enemy!");
    }
}