using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;
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
    public Transform attackPoint;
    [Header("PlayerInfo")]
    public float _lifepoints;
   
    [Header("Movimentação")]
    public float moveSpeed = 5f; // Velocidade do jogador
    public float _dashSpeed;
    public bool _dashing;
    public CharacterController Controller;
    public Vector3 moveDirection;
    [Header("Gravidade")]
    [SerializeField] Vector3 _playerVelocity;
    public float _gravityFloat = -9.81f;
    [Header("Tiro")]
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform shootPoint;        // Ponto de disparo
    public float projectileSpeed = 10f; // Velocidade do projétil

    [Header("Camera")]
    private Camera cam;

  

    void Start()
    {
      
        cam = Camera.main;
    }

    void Update()
    {
        Move();
        RotateTowardsMouse();
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo para atirar
        {
            Shoot();
        }
        if (Input.GetMouseButtonDown(1)) // Botão direito para atacar
        {
            Melee();
        }
    }

    public void Move()
    {
        Controller.Move(new Vector3(moveDirection.x * moveSpeed, Controller.velocity.y, moveDirection.z * moveSpeed) * Time.deltaTime);
    }

    public void PlayerMove(InputAction.CallbackContext value) 
    { 
        moveDirection = value.ReadValue<Vector3>();

        Vector3 m = value.ReadValue<Vector3>();


        moveDirection.x = m.x;
        moveDirection.y = m.y;
    }

    public void Gravity()
    {

        _playerVelocity.y += _gravityFloat * Time.deltaTime;

        Controller.Move(_playerVelocity * Time.deltaTime);


    }






    void RotateTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Mantém a rotação no plano horizontal
            transform.LookAt(targetPosition); 
        }
    }

    void Shoot()
    {
        GameObject bullet = BulletPooling.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.transform.position;
            bullet.transform.rotation = shootPoint.transform.rotation; // Rotaciona a bala corretamente
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
    
    if (Physics.Raycast(attackPoint.transform.position, attackPoint.transform.forward, out RaycastHit hit, _attackDistance, _attackLayer)) 
    { 
        Debug.Log("HitTarget");
        
        
        
        
    }
    
    
    
    
    
    
    }

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Debug.Log("You hit the enemy!");        
        }
    }

}
