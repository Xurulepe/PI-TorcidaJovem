using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
public class PlayerControle : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 5f; // Velocidade do jogador
    public float _dashSpeed;
    public bool _dashing;
    public Vector3 moveDirection;
    private Rigidbody rb;
    [Header("Tiro")]
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform shootPoint;        // Ponto de disparo
    public float projectileSpeed = 10f; // Velocidade do projétil

    [Header("Camera")]
    private Camera cam;

    [Header("Variables")]
    public StateSET _state; //referencia a função enumerator

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        RotateTowardsMouse();
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo para atirar
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {      
        rb.linearVelocity = new Vector3 (moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed) ;
    }

    public void PlayerMove(InputAction.CallbackContext value) 
    { 
        moveDirection = value.ReadValue<Vector3>();
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

    public enum StateSET //enumerator das ações 
    { 
        dashing, 
    }

    private void stateHandle() 
    {
        if (_dashing) 
        { 
            _state = StateSET.dashing;
            moveSpeed = _dashSpeed;        
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
