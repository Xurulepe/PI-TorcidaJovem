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
    public Animator _attackAnim;
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

    [Header("Camera")]
    private Camera cam;

    void Start()
    {
        _attackAnim = GetComponent<Animator>();
        cam = Camera.main;
        if (Controller == null)
            Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Gravity();
        Move();
        //if (Controller.velocity.x + Controller.velocity.z <= 0)
    
        RotateTowardsMouse();
        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.L))
        {
            _attackAnim.SetBool("Atack", _isAttacking);
            AttackRaycast();
        }
    }

    public void Move()
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

    // recebe entrada do Input System
    public void PlayerMove(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector3>();
    }

    public void Gravity()
    {
        if (Controller.isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -1f;

        _playerVelocity.y += _gravityFloat * Time.deltaTime;
    }

    void RotateTowardsMouse()
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

    void Shoot()
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
        if (!_readytoAttack || _isAttacking) return;
        _readytoAttack = false;
        _isAttacking = true;
        Invoke(nameof(ResetAttack), _attackSpeed);
        Invoke(nameof(AttackRaycast), _attackDelay);
    }

    void ResetAttack()
    {
        _readytoAttack = true;
        _isAttacking = false;
    }

    void AttackRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.transform.position, attackPoint.transform.forward, out hit, _attackDistance, _attackLayer))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("HitTarget");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Debug.Log("You hit the enemy!");
    }
}