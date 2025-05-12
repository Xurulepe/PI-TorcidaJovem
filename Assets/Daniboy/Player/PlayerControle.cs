using UnityEngine;

public class PlayerControle : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 5f; // Velocidade do jogador

    [Header("Tiro")]
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform shootPoint;        // Ponto de disparo
    public float projectileSpeed = 10f; // Velocidade do projétil

    private Rigidbody rb;
    private Camera cam;

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

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        rb.linearVelocity = moveDirection * moveSpeed;
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
}
