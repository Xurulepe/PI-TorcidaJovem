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

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Debug.Log("You hit the enemy!");        
        }
    }

}
