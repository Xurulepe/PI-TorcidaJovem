using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
        Move();
        RotateTowardsMouse();
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo para atirar
        {
            Shoot();
        }
    }

    public void Move()
    {
      
        rb.linearVelocity = new Vector3 (moveDirection.x , rb.linearVelocity.y, moveDirection.z) * moveSpeed;
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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 shootDirection = (hit.point - shootPoint.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rbProjectile = projectile.GetComponent<Rigidbody>();
            rbProjectile.linearVelocity = shootDirection * projectileSpeed;
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

}
