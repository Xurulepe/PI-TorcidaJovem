using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.XR;

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
    public CharacterController Controller;
    public Vector3 moveInput; // agora usamos Vector2 (x,y)
    public float _rotationSpeed = 10f;

    // vetores fixos para projeção isométrica
    private Vector3 isoForward = new Vector3(1, 0, 1).normalized;
    private Vector3 isoRight = new Vector3(1, 0, -1).normalized;

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

    [Header("Animation")]
    public Animator _Anim;

    [Header("ScriptCalling")]
    public Dashing _dashScript;
    void Start()
    {
        _dashScript = GetComponent<Dashing>();
        _Anim = GetComponent<Animator>();
        cam = Camera.main;
        if (Controller == null)
            Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Gravity();
        Move();
        InputAttack();
        //if (Controller.velocity.x + Controller.velocity.z <= 0)

        RotateTowardsMouse();
        
           
           

        
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            LockFunction();
        }
    }

    public void InputAttack() 
    {
        if (_lockMove == false) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                _Anim.SetTrigger("Shoot");

            }
            if (Input.GetMouseButton(1))
            {
                _Anim.SetTrigger("Attack");

            }
        }  
    }

    public void LockFunction()
    { 
        _lockMove = true;
        _dashScript.DashMove();
        AttackRaycast();
        InputAttack();
        Move();
        Shoot();
        RotateTowardsMouse();
        Gravity();
        Melee();
        Shootmanage();
    }

    public void Move()
    {
        if (_lockMove == false ) 
        {

            float h = moveInput.x; // A/D ou ← →
            float v = moveInput.z; // W/S ou ↑ ↓

            // converte entrada para movimento no plano isométrico
            Vector3 move = (isoForward * v + isoRight * h).normalized;

            // junta movimento horizontal + gravidade
            Vector3 finalMove = move * moveSpeed + Vector3.up * _playerVelocity.y;

            if (move.sqrMagnitude > 0.001f)
            {
                Controller.Move(finalMove * Time.deltaTime);

                // rotaciona suavemente para direção do movimento
                Quaternion targetRot = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
            }
            else
            {
                // se parado, só aplica gravidade
                Controller.Move(Vector3.up * _playerVelocity.y * Time.deltaTime);
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
            if (Controller.isGrounded && _playerVelocity.y < 0)
                _playerVelocity.y = -1f;

            _playerVelocity.y += _gravityFloat * Time.deltaTime;



        }


       
    }

    void RotateTowardsMouse()
    {
        if (_lockMove == false) 
        {

            if (cam == null) return;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);
            }

        }


        
    }

    void Shoot()
    {
        if (_lockMove == false) 
        {
            _Anim.SetTrigger("Shoot");
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyMelee"))
            Debug.Log("You hit the enemy!");
    }
}